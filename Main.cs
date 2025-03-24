using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
            SetDefaultHotkeySettings();
            LoadHotkeySettings();
        }
        private void SetDefaultHotkeySettings()
        {
            if (Properties.Settings.Default.Modifiers == 0 && Properties.Settings.Default.Key == 0)
            {
                Properties.Settings.Default.Modifiers = (0x0001 | 0x0002);
                Properties.Settings.Default.Key = (int)Keys.P;
                Properties.Settings.Default.Save();
            }
        }

        #region комбинации клавиш
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 1;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();

                if (id == HOTKEY_ID)
                {
                    Thread.Sleep(300);
                    translite();
                }
            }

            base.WndProc(ref m);
        }
        private void LoadHotkeySettings()
        {
            int modifiers = Properties.Settings.Default.Modifiers;
            Keys key = (Keys)Properties.Settings.Default.Key;
            string modifiersNames = GetModifierNames(modifiers);

            label1.Text = $"1. Выделите нужный текст 2. Нажмите: {modifiersNames}+{key}";

            RegisterHotKey(Handle, HOTKEY_ID, modifiers, (int)key);
        }
        private string GetModifierNames(int modifiers)
        {
            List<string> modifierNames = new List<string>();

            if ((modifiers & 0x0001) != 0)
                modifierNames.Add("Alt");
            if ((modifiers & 0x0002) != 0)
                modifierNames.Add("Ctrl");
            if ((modifiers & 0x0004) != 0)
                modifierNames.Add("Shift");

            return string.Join(" + ", modifierNames);
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
            try
            {
                SendKeys.SendWait("^c");
                Thread.Sleep(100);
                if (OpenClipboard(IntPtr.Zero))
                {
                    IntPtr hClipboardData = GetClipboardData(CF_TEXT);
                    if (hClipboardData != IntPtr.Zero)
                    {
                        string selectedText = Marshal.PtrToStringAnsi(hClipboardData);
                        return selectedText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при getselectedtext:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
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
            {'@','"'}, {'#','№'}, {'$',';'}, {'^',':'}, {'&','?'}, {' ',' '}, {'`','ё'}, {'~','Ё'},
            {'Q','Й'}, {'W','Ц'}, {'E','У'}, {'R','К'}, {'T','Е'}, {'Y','Н'}, {'U','Г'}, {'I','Ш'}, {'O','Щ'}, {'P','З'}, {'{','Х'}, {'}','Ъ'},
            {'A','Ф'}, {'S','Ы'}, {'D','В'}, {'F','А'}, {'G','П'}, {'H','Р'}, {'J','О'}, {'K','Л'}, {'L','Д'}, {':','Ж'}, {'"','Э'}, 
            {'Z','Я'}, {'X','Ч'}, {'C','С'}, {'V','М'}, {'B','И'}, {'N','Т'}, {'M','Ь'}, {'<','Б'}, {'>','Ю'}
        };

            StringBuilder correctedText = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                correctedText.Append(dictionary.TryGetValue(c, out char correctedChar) ? correctedChar : c);
            }

            return correctedText.ToString();
        }
        private void translite()
        {
            string text = GetSelectedText();
            if (!string.IsNullOrEmpty(text))
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

        #region Tray
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Close_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Settings_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.SettingsChanged += LoadHotkeySettings;
            settingsForm.ShowDialog();
        }
    }
}
