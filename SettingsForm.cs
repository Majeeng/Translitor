using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace translitor
{
    public partial class SettingsForm: Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public void SaveHotkeySettings(HotkeySettings settings)
        {
            Properties.Settings.Default.Modifiers = settings.Modifiers;
            Properties.Settings.Default.Key = (int)settings.Key;
            Properties.Settings.Default.Save();
        }
        public event Action SettingsChanged;
        private Keys selectedKey;

        private void btnSave_Click(object sender, EventArgs e)
        {
            int modifiers = 0;

            if (cbShift.Checked)
                modifiers |= 0x0001;
            if (cbCtrl.Checked)
                modifiers |= 0x0002;
            if (cbAlt.Checked)
                modifiers |= 0x0004;

            // Сохраняем настройки
            Properties.Settings.Default.Modifiers = modifiers;
            Properties.Settings.Default.Key = (int)selectedKey;
            Properties.Settings.Default.Save();

            SettingsChanged?.Invoke();
            MessageBox.Show("Настройки сохранены!");
        }

        private bool waitingForKey = false;
        private void btnChangeLetter_Click(object sender, EventArgs e)
        {
            waitingForKey = true;
            this.Text = "Нажмите клавишу...";
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            int modifiers = Properties.Settings.Default.Modifiers;
            selectedKey = (Keys)Properties.Settings.Default.Key;

            cbShift.Checked = (modifiers & 0x0001) != 0;
            cbCtrl.Checked = (modifiers & 0x0002) != 0;
            cbAlt.Checked = (modifiers & 0x0004) != 0;

            label1.Text = $"Кнопка: {selectedKey}";
            this.KeyPreview = true;
        }

        private void btnSbros_Click(object sender, EventArgs e)
        {
            int modifiers = Properties.Settings.Default.Modifiers;
            selectedKey = Keys.P;

            cbShift.Checked = true;
            cbAlt.Checked = true;

            label1.Text = $"Кнопка: {selectedKey}";
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (waitingForKey)
            {
                selectedKey = e.KeyCode;
                label1.Text = $"Кнопка: {selectedKey}";
                this.Text = "Settings";
                waitingForKey = false;
                e.Handled = true;
            }
        }
    }
}
