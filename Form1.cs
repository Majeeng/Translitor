using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace translitor
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            notifyIcon.Visible = true;

            // регистрация глобальной комбинации клавиш
            FormClosing += (s, e) => { UnregisterHotKey(Handle, HOTKEY_ID); };
            RegisterHotKey(Handle, HOTKEY_ID, MOD_ALT | MOD_SHIFT, (int)Keys.P);
        }

        #region комбинации клавиш
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int MOD_ALT = 0x0001;
        private const int MOD_SHIFT = 0x0004;

        private const int WM_HOTKEY = 0x0312;

        private const int HOTKEY_ID = 1;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();

                if (id == HOTKEY_ID)
                {
                    //perform hotkey action
                    Thread.Sleep(300);
                    translite();
                }
            }

            base.WndProc(ref m);
        }
        #endregion

        SoundPlayer sp = new SoundPlayer(Properties.Resources.empty);

        #region перевод
        #region получение выделенного текста
        #region импорт функций
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();
        #endregion

        private const uint CF_TEXT = 1;

        public static string GetSelectedText()
        {
            SendKeys.SendWait("^c");
            Thread.Sleep(100);
            if (OpenClipboard(IntPtr.Zero))
            {
                // Получаем данные из буфера обмена
                IntPtr hClipboardData = GetClipboardData(CF_TEXT);
                if (hClipboardData != IntPtr.Zero)
                {
                    string selectedText = Marshal.PtrToStringAnsi(hClipboardData);
                    CloseClipboard();
                    return selectedText;
                }
                CloseClipboard();
            }
            return string.Empty;
        }
        #endregion
        private string CorrectText(string text)
        {
            // Пример словаря для замены
            var dictionary = new Dictionary<char, char>
        {
            {'q','й'}, {'w','ц'}, {'e','у'}, {'r','к'}, {'t','е'}, {'y','н'}, {'u','г'}, {'i','ш'}, {'o','щ'}, {'p','з'}, {'[','х'},{']','ъ'},
            {'a','ф'}, {'s','ы'}, {'d','в'}, {'f','а'}, {'g','п'}, {'h','р'}, {'j','о'}, {'k','л'}, {'l','д'}, {';','ж'}, {'\'','э'},
            {'z','я'}, {'x','ч'}, {'c','с'}, {'v','м'}, {'b','и'}, {'n','т'}, {'m','ь'}, {',','б'}, {'.','ю'}, {'/','.'}, {'?',','},
            {'@','"'}, {'#','№'}, {'$',';'}, {'^',':'}, {'&','?'}, {' ',' '},
            {'Q','Й'}, {'W','Ц'}, {'E','У'}, {'R','К'}, {'T','Е'}, {'Y','Н'}, {'U','Г'}, {'I','Ш'}, {'O','Щ'}, {'P','З'}, {'{','Х'}, {'}','Ъ'},
            {'A','Ф'}, {'S','Ы'}, {'D','В'}, {'F','А'}, {'G','П'}, {'H','Р'}, {'J','О'}, {'K','Л'}, {'L','Д'}, {':','Ж'}, {'"','Э'}, 
            {'Z','Я'}, {'X','Ч'}, {'C','С'}, {'V','М'}, {'B','И'}, {'N','Т'}, {'M','Ь'}, {'<','Б'}, {'>','Ю'}
        };

            char[] correctedChars = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                correctedChars[i] = dictionary.ContainsKey(text[i]) ? dictionary[text[i]] : text[i];
            }

            return new string(correctedChars);
        }
        private void translite()
        {
            string text = GetSelectedText();
            if (text != string.Empty)
            {
                text = CorrectText(text);
                tbConclusion.Text = text;
                Clipboard.SetText(text);
                SendKeys.SendWait("^v");
            }
            else
            {
                tbConclusion.Text = "Выделенный текст не найден";
                sp.Play();
            }
        }
        #endregion
        #region функционал
        // трей
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Передвижение окна (пока что не работает)
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        // кнопка свернуть
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
