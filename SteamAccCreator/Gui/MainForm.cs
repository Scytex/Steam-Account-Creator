using System;
using System.Windows.Forms;
using SteamAccCreator.Web;

namespace SteamAccCreator.Gui
{
    public partial class MainForm : Form
    {
        private readonly AccountCreator _accountCreator;

        public bool RandomMail { get; private set; }
        public bool RandomAlias { get; private set; }
        public bool RandomPass { get; private set; }
        public bool WriteIntoFile { get; private set; }
        public bool UseProxy { get; private set; }
        public bool AutoMailVerify { get; private set; }
        public bool UseCaptchaService { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < nmbrAmountAccounts.Value; i++)
            {
                new AccountCreator(this, txtEmail.Text, txtAlias.Text, txtPass.Text, i).Run();
            }
        }

        public void AddToTable(string mail, string alias, string pass)
        {
            dataAccounts.Rows.Add(new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell {Value = mail},
                    new DataGridViewTextBoxCell {Value = alias},
                    new DataGridViewTextBoxCell {Value = pass},
                    new DataGridViewTextBoxCell {Value = "Ready"}
                }
            });
        }

        public void UpdateStatus(int i, string status)
        {
            dataAccounts.Rows[i].Cells[3].Value = status;
        }

        public string ShowUpdateInfoBox(string status)
        {
            var inputDialog = new InputDialog(status);
            var update = string.Empty;

            if (inputDialog.ShowDialog(this) == DialogResult.OK)
            {
                update = inputDialog.txtInfo.Text;
            }
            inputDialog.Dispose();
            return update;
        }

        public string ShowCaptchaDialog(HttpHandler httpHandler)
        {
            var captchaDialog = new CaptchaDialog(httpHandler);
            var captcha = string.Empty;

            if (captchaDialog.ShowDialog(this) == DialogResult.OK)
            {
                captcha = captchaDialog.txtCaptcha.Text;
            }
            captchaDialog.Dispose();
            return captcha;
        }

        private void ChkAutoVerifyMail_CheckedChanged(object sender, EventArgs e)
        {
            AutoMailVerify = chkAutoVerifyMail.Checked;
        }

        private void ChkWriteIntoFile_CheckedChanged(object sender, EventArgs e)
        {
            WriteIntoFile = chkWriteIntoFile.Checked;
        }

        private void ChkAutoCaptcha_CheckedChanged(object sender, EventArgs e)
        {
            UseCaptchaService = chkAutoCaptcha.Checked;
        }

        private void ChkRandomMail_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomMail.Checked;
            chkAutoVerifyMail.Enabled = state;
            chkAutoVerifyMail.Checked = state;
            RandomMail = state;
            txtEmail.Enabled = !state;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomPass_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomPass.Checked;
            txtPass.Enabled = !state;
            RandomPass = state;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomAlias_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomAlias.Checked;
            txtAlias.Enabled = !state;
            RandomAlias = state;
            ToggleForceWriteIntoFile();
        }
        private void ChkProxy_CheckedChanged(object sender, EventArgs e)
        {
            UseProxy = chkProxy.Checked;
        }

        private void ToggleForceWriteIntoFile()
        {
            var shouldForce = chkRandomMail.Checked || chkRandomPass.Checked || chkRandomAlias.Checked;
            chkWriteIntoFile.Checked = shouldForce;
            chkWriteIntoFile.Enabled = !shouldForce;
        }
    }
}
