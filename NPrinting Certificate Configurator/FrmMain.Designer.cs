namespace NPrinting_Certificate_Configurator
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRestoreBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisableConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRestartService = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFaq = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConfigure = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkPassword = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tasksToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(401, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(92, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRestoreBackup,
            this.mnuDisableConfig,
            this.toolStripSeparator1,
            this.mnuRestartService});
            this.tasksToolStripMenuItem.Enabled = global::NPrinting_Certificate_Configurator.Properties.Settings.Default.NotProcessing;
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            this.tasksToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.tasksToolStripMenuItem.Text = "Tasks";
            // 
            // mnuRestoreBackup
            // 
            this.mnuRestoreBackup.Name = "mnuRestoreBackup";
            this.mnuRestoreBackup.Size = new System.Drawing.Size(232, 22);
            this.mnuRestoreBackup.Text = "Restore Configuration Backup";
            this.mnuRestoreBackup.Click += new System.EventHandler(this.MnuRestoreBackup_Click);
            // 
            // mnuDisableConfig
            // 
            this.mnuDisableConfig.Name = "mnuDisableConfig";
            this.mnuDisableConfig.Size = new System.Drawing.Size(232, 22);
            this.mnuDisableConfig.Text = "Disable Configuration";
            this.mnuDisableConfig.Click += new System.EventHandler(this.MnuDisableConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // mnuRestartService
            // 
            this.mnuRestartService.Name = "mnuRestartService";
            this.mnuRestartService.Size = new System.Drawing.Size(232, 22);
            this.mnuRestartService.Text = "Restart Web Engine Service";
            this.mnuRestartService.Click += new System.EventHandler(this.MnuRestartService_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoc,
            this.toolStripSeparator2,
            this.mnuGitHub,
            this.mnuFaq,
            this.mnuCheckUpdates,
            this.toolStripSeparator3,
            this.mnuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mnuDoc
            // 
            this.mnuDoc.Name = "mnuDoc";
            this.mnuDoc.Size = new System.Drawing.Size(230, 22);
            this.mnuDoc.Text = "Qlik Documentation...";
            this.mnuDoc.Click += new System.EventHandler(this.MnuDoc_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuGitHub
            // 
            this.mnuGitHub.Name = "mnuGitHub";
            this.mnuGitHub.Size = new System.Drawing.Size(230, 22);
            this.mnuGitHub.Text = "GitHub Repository...";
            this.mnuGitHub.Click += new System.EventHandler(this.MnuGitHub_Click);
            // 
            // mnuFaq
            // 
            this.mnuFaq.Name = "mnuFaq";
            this.mnuFaq.Size = new System.Drawing.Size(230, 22);
            this.mnuFaq.Text = "Frequently Asked Questions...";
            this.mnuFaq.Click += new System.EventHandler(this.MnuFaq_Click);
            // 
            // mnuCheckUpdates
            // 
            this.mnuCheckUpdates.Name = "mnuCheckUpdates";
            this.mnuCheckUpdates.Size = new System.Drawing.Size(230, 22);
            this.mnuCheckUpdates.Text = "Check for Updates...";
            this.mnuCheckUpdates.Click += new System.EventHandler(this.MnuCheckUpdates_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(230, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
            // 
            // btnConfigure
            // 
            this.btnConfigure.Enabled = false;
            this.btnConfigure.Location = new System.Drawing.Point(88, 184);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(224, 48);
            this.btnConfigure.TabIndex = 3;
            this.btnConfigure.Text = "EZ-Configure";
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.BtnConfigure_Click);
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.Location = new System.Drawing.Point(8, 48);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(384, 20);
            this.txtFile.TabIndex = 2;
            this.txtFile.TabStop = false;
            this.txtFile.TextChanged += new System.EventHandler(this.TxtFile_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(8, 128);
            this.txtPassword.MaxLength = 256;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = 'x';
            this.txtPassword.Size = new System.Drawing.Size(384, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // btnBrowse
            // 
            this.btnBrowse.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::NPrinting_Certificate_Configurator.Properties.Settings.Default, "NotProcessing", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnBrowse.Enabled = global::NPrinting_Certificate_Configurator.Properties.Settings.Default.NotProcessing;
            this.btnBrowse.Location = new System.Drawing.Point(264, 72);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(129, 32);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select certificate in PFX format:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Checked = true;
            this.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackup.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::NPrinting_Certificate_Configurator.Properties.Settings.Default, "NotProcessing", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkBackup.Enabled = global::NPrinting_Certificate_Configurator.Properties.Settings.Default.NotProcessing;
            this.chkBackup.Location = new System.Drawing.Point(119, 161);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(166, 17);
            this.chkBackup.TabIndex = 2;
            this.chkBackup.Text = "Create configuration backup?";
            this.chkBackup.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripServiceStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 242);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(401, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripServiceStatus
            // 
            this.toolStripServiceStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripServiceStatus.Name = "toolStripServiceStatus";
            this.toolStripServiceStatus.Size = new System.Drawing.Size(386, 17);
            this.toolStripServiceStatus.Spring = true;
            this.toolStripServiceStatus.Text = "Idle";
            this.toolStripServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPassword
            // 
            this.chkPassword.AutoSize = true;
            this.chkPassword.Checked = true;
            this.chkPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPassword.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::NPrinting_Certificate_Configurator.Properties.Settings.Default, "NotProcessing", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkPassword.Enabled = global::NPrinting_Certificate_Configurator.Properties.Settings.Default.NotProcessing;
            this.chkPassword.Location = new System.Drawing.Point(8, 111);
            this.chkPassword.Name = "chkPassword";
            this.chkPassword.Size = new System.Drawing.Size(124, 17);
            this.chkPassword.TabIndex = 9;
            this.chkPassword.Text = "Certificate password:";
            this.chkPassword.UseVisualStyleBackColor = true;
            this.chkPassword.CheckedChanged += new System.EventHandler(this.ChkPassword_CheckedChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(401, 264);
            this.Controls.Add(this.chkPassword);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnConfigure);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPrinting Certificate Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button btnConfigure;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRestoreBackup;
        private System.Windows.Forms.ToolStripMenuItem mnuDisableConfig;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripServiceStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRestartService;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuGitHub;
        private System.Windows.Forms.ToolStripMenuItem mnuFaq;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckUpdates;
        private System.Windows.Forms.CheckBox chkPassword;
    }
}

