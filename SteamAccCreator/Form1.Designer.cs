namespace SteamAccCreator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblAlias = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.nmbrAmountAccounts = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.chkRandomMail = new System.Windows.Forms.CheckBox();
            this.pnlSettings = new System.Windows.Forms.GroupBox();
            this.chkWriteIntoFile = new System.Windows.Forms.CheckBox();
            this.chkAutoCaptcha = new System.Windows.Forms.CheckBox();
            this.chkAutoVerifyMail = new System.Windows.Forms.CheckBox();
            this.chkRandomAlias = new System.Windows.Forms.CheckBox();
            this.chkRandomPass = new System.Windows.Forms.CheckBox();
            this.pnlCreation = new System.Windows.Forms.GroupBox();
            this.chkProxy = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmbrAmountAccounts)).BeginInit();
            this.pnlSettings.SuspendLayout();
            this.pnlCreation.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(94, 19);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(206, 20);
            this.txtEmail.TabIndex = 1;
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.Location = new System.Drawing.Point(94, 97);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(206, 23);
            this.btnCreateAccount.TabIndex = 5;
            this.btnCreateAccount.Text = "Create Account";
            this.btnCreateAccount.UseVisualStyleBackColor = true;
            this.btnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(34, 22);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(96, 123);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.Location = new System.Drawing.Point(142, 123);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(38, 13);
            this.lblStatusInfo.TabIndex = 9;
            this.lblStatusInfo.Text = "Ready";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(94, 45);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(206, 20);
            this.txtAlias.TabIndex = 2;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(94, 71);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '•';
            this.txtPass.Size = new System.Drawing.Size(206, 20);
            this.txtPass.TabIndex = 3;
            // 
            // lblAlias
            // 
            this.lblAlias.AutoSize = true;
            this.lblAlias.Location = new System.Drawing.Point(34, 48);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(32, 13);
            this.lblAlias.TabIndex = 12;
            this.lblAlias.Text = "Alias:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(34, 74);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(33, 13);
            this.lblPass.TabIndex = 13;
            this.lblPass.Text = "Pass:";
            // 
            // nmbrAmountAccounts
            // 
            this.nmbrAmountAccounts.Location = new System.Drawing.Point(53, 19);
            this.nmbrAmountAccounts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbrAmountAccounts.Name = "nmbrAmountAccounts";
            this.nmbrAmountAccounts.Size = new System.Drawing.Size(41, 20);
            this.nmbrAmountAccounts.TabIndex = 14;
            this.nmbrAmountAccounts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(6, 21);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(41, 13);
            this.lblAmount.TabIndex = 15;
            this.lblAmount.Text = "Create:";
            // 
            // chkRandomMail
            // 
            this.chkRandomMail.AutoSize = true;
            this.chkRandomMail.Location = new System.Drawing.Point(9, 45);
            this.chkRandomMail.Name = "chkRandomMail";
            this.chkRandomMail.Size = new System.Drawing.Size(88, 17);
            this.chkRandomMail.TabIndex = 16;
            this.chkRandomMail.Text = "Random Mail";
            this.chkRandomMail.UseVisualStyleBackColor = true;
            this.chkRandomMail.CheckedChanged += new System.EventHandler(this.ChkRandomMail_CheckedChanged);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.chkProxy);
            this.pnlSettings.Controls.Add(this.chkWriteIntoFile);
            this.pnlSettings.Controls.Add(this.chkAutoCaptcha);
            this.pnlSettings.Controls.Add(this.chkAutoVerifyMail);
            this.pnlSettings.Controls.Add(this.chkRandomAlias);
            this.pnlSettings.Controls.Add(this.chkRandomPass);
            this.pnlSettings.Controls.Add(this.nmbrAmountAccounts);
            this.pnlSettings.Controls.Add(this.chkRandomMail);
            this.pnlSettings.Controls.Add(this.lblAmount);
            this.pnlSettings.Location = new System.Drawing.Point(15, 12);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(332, 96);
            this.pnlSettings.TabIndex = 17;
            this.pnlSettings.TabStop = false;
            this.pnlSettings.Text = "Settings";
            // 
            // chkWriteIntoFile
            // 
            this.chkWriteIntoFile.AutoSize = true;
            this.chkWriteIntoFile.Location = new System.Drawing.Point(200, 68);
            this.chkWriteIntoFile.Name = "chkWriteIntoFile";
            this.chkWriteIntoFile.Size = new System.Drawing.Size(117, 17);
            this.chkWriteIntoFile.TabIndex = 20;
            this.chkWriteIntoFile.Text = "Write Data Into File";
            this.chkWriteIntoFile.UseVisualStyleBackColor = true;
            // 
            // chkAutoCaptcha
            // 
            this.chkAutoCaptcha.AutoSize = true;
            this.chkAutoCaptcha.Enabled = false;
            this.chkAutoCaptcha.Location = new System.Drawing.Point(200, 45);
            this.chkAutoCaptcha.Name = "chkAutoCaptcha";
            this.chkAutoCaptcha.Size = new System.Drawing.Size(127, 17);
            this.chkAutoCaptcha.TabIndex = 19;
            this.chkAutoCaptcha.Text = "Use Captcha Service";
            this.chkAutoCaptcha.UseVisualStyleBackColor = true;
            // 
            // chkAutoVerifyMail
            // 
            this.chkAutoVerifyMail.AutoSize = true;
            this.chkAutoVerifyMail.Enabled = false;
            this.chkAutoVerifyMail.Location = new System.Drawing.Point(103, 45);
            this.chkAutoVerifyMail.Name = "chkAutoVerifyMail";
            this.chkAutoVerifyMail.Size = new System.Drawing.Size(99, 17);
            this.chkAutoVerifyMail.TabIndex = 19;
            this.chkAutoVerifyMail.Text = "Auto Mail Verify";
            this.chkAutoVerifyMail.UseVisualStyleBackColor = true;
            // 
            // chkRandomAlias
            // 
            this.chkRandomAlias.AutoSize = true;
            this.chkRandomAlias.Location = new System.Drawing.Point(103, 68);
            this.chkRandomAlias.Name = "chkRandomAlias";
            this.chkRandomAlias.Size = new System.Drawing.Size(91, 17);
            this.chkRandomAlias.TabIndex = 18;
            this.chkRandomAlias.Text = "Random Alias";
            this.chkRandomAlias.UseVisualStyleBackColor = true;
            this.chkRandomAlias.CheckedChanged += new System.EventHandler(this.ChkRandomAlias_CheckedChanged);
            // 
            // chkRandomPass
            // 
            this.chkRandomPass.AutoSize = true;
            this.chkRandomPass.Location = new System.Drawing.Point(9, 68);
            this.chkRandomPass.Name = "chkRandomPass";
            this.chkRandomPass.Size = new System.Drawing.Size(92, 17);
            this.chkRandomPass.TabIndex = 18;
            this.chkRandomPass.Text = "Random Pass";
            this.chkRandomPass.UseVisualStyleBackColor = true;
            this.chkRandomPass.CheckedChanged += new System.EventHandler(this.ChkRandomPass_CheckedChanged);
            // 
            // pnlCreation
            // 
            this.pnlCreation.Controls.Add(this.btnCreateAccount);
            this.pnlCreation.Controls.Add(this.lblPass);
            this.pnlCreation.Controls.Add(this.lblAlias);
            this.pnlCreation.Controls.Add(this.txtPass);
            this.pnlCreation.Controls.Add(this.txtEmail);
            this.pnlCreation.Controls.Add(this.txtAlias);
            this.pnlCreation.Controls.Add(this.lblEmail);
            this.pnlCreation.Controls.Add(this.lblStatusInfo);
            this.pnlCreation.Controls.Add(this.lblStatus);
            this.pnlCreation.Location = new System.Drawing.Point(15, 114);
            this.pnlCreation.Name = "pnlCreation";
            this.pnlCreation.Size = new System.Drawing.Size(332, 147);
            this.pnlCreation.TabIndex = 18;
            this.pnlCreation.TabStop = false;
            // 
            // chkProxy
            // 
            this.chkProxy.AutoSize = true;
            this.chkProxy.Location = new System.Drawing.Point(200, 21);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Size = new System.Drawing.Size(100, 17);
            this.chkProxy.TabIndex = 21;
            this.chkProxy.Text = "Use Proxylist.txt";
            this.chkProxy.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 268);
            this.Controls.Add(this.pnlCreation);
            this.Controls.Add(this.pnlSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Steam Account Creator";
            ((System.ComponentModel.ISupportInitialize)(this.nmbrAmountAccounts)).EndInit();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlCreation.ResumeLayout(false);
            this.pnlCreation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnCreateAccount;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusInfo;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.NumericUpDown nmbrAmountAccounts;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.CheckBox chkRandomMail;
        private System.Windows.Forms.GroupBox pnlSettings;
        private System.Windows.Forms.CheckBox chkWriteIntoFile;
        private System.Windows.Forms.CheckBox chkAutoCaptcha;
        private System.Windows.Forms.CheckBox chkAutoVerifyMail;
        private System.Windows.Forms.CheckBox chkRandomAlias;
        private System.Windows.Forms.CheckBox chkRandomPass;
        private System.Windows.Forms.GroupBox pnlCreation;
        private System.Windows.Forms.CheckBox chkProxy;
    }
}

