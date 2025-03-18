namespace translitor
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cbShift = new System.Windows.Forms.CheckBox();
            this.cbAlt = new System.Windows.Forms.CheckBox();
            this.cbCtrl = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSbros = new System.Windows.Forms.Button();
            this.btnChangeLetter = new System.Windows.Forms.Button();
            this.tbWarn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbShift
            // 
            this.cbShift.AutoSize = true;
            this.cbShift.Location = new System.Drawing.Point(12, 12);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(47, 17);
            this.cbShift.TabIndex = 0;
            this.cbShift.Text = "Shift";
            this.cbShift.UseVisualStyleBackColor = true;
            // 
            // cbAlt
            // 
            this.cbAlt.AutoSize = true;
            this.cbAlt.Location = new System.Drawing.Point(12, 35);
            this.cbAlt.Name = "cbAlt";
            this.cbAlt.Size = new System.Drawing.Size(38, 17);
            this.cbAlt.TabIndex = 0;
            this.cbAlt.Text = "Alt";
            this.cbAlt.UseVisualStyleBackColor = true;
            // 
            // cbCtrl
            // 
            this.cbCtrl.AutoSize = true;
            this.cbCtrl.Location = new System.Drawing.Point(12, 58);
            this.cbCtrl.Name = "cbCtrl";
            this.cbCtrl.Size = new System.Drawing.Size(41, 17);
            this.cbCtrl.TabIndex = 0;
            this.cbCtrl.Text = "Ctrl";
            this.cbCtrl.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Кнопка: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(107, 128);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSbros
            // 
            this.btnSbros.Location = new System.Drawing.Point(12, 137);
            this.btnSbros.Name = "btnSbros";
            this.btnSbros.Size = new System.Drawing.Size(75, 23);
            this.btnSbros.TabIndex = 2;
            this.btnSbros.Text = "Сбросить";
            this.btnSbros.UseVisualStyleBackColor = true;
            this.btnSbros.Click += new System.EventHandler(this.btnSbros_Click);
            // 
            // btnChangeLetter
            // 
            this.btnChangeLetter.Location = new System.Drawing.Point(85, 91);
            this.btnChangeLetter.Name = "btnChangeLetter";
            this.btnChangeLetter.Size = new System.Drawing.Size(93, 20);
            this.btnChangeLetter.TabIndex = 3;
            this.btnChangeLetter.Text = "сменить";
            this.btnChangeLetter.UseVisualStyleBackColor = true;
            this.btnChangeLetter.Click += new System.EventHandler(this.btnChangeLetter_Click);
            // 
            // tbWarn
            // 
            this.tbWarn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbWarn.Enabled = false;
            this.tbWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbWarn.Location = new System.Drawing.Point(94, 12);
            this.tbWarn.Multiline = true;
            this.tbWarn.Name = "tbWarn";
            this.tbWarn.Size = new System.Drawing.Size(103, 63);
            this.tbWarn.TabIndex = 4;
            this.tbWarn.TabStop = false;
            this.tbWarn.Text = "Осторожно! Некоторые комбинации могут не сработать.";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 172);
            this.Controls.Add(this.tbWarn);
            this.Controls.Add(this.btnChangeLetter);
            this.Controls.Add(this.btnSbros);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCtrl);
            this.Controls.Add(this.cbAlt);
            this.Controls.Add(this.cbShift);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbShift;
        private System.Windows.Forms.CheckBox cbAlt;
        private System.Windows.Forms.CheckBox cbCtrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSbros;
        private System.Windows.Forms.Button btnChangeLetter;
        private System.Windows.Forms.TextBox tbWarn;
    }
}