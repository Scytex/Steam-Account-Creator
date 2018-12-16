using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAccCreator.File;
using SteamAccCreator.Web;

namespace SteamAccCreator
{
    public partial class Form1 : Form
    {
        private string _status = string.Empty;
        private string _alias = string.Empty;
        private string _pass = string.Empty;
        private string _mail = string.Empty;
        private string _captcha = string.Empty;
        private static readonly Random Random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private readonly HttpHandler _httpHandler = new HttpHandler();
        private readonly FileManager _fileManager = new FileManager();
        private readonly MailHandler _mailHandler = new MailHandler();

        private async void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            _alias = txtAlias.Text;
            _pass = txtPass.Text;
            _mail = txtEmail.Text;
            if (chkRandomMail.Checked)
                _mail = GetRandomString(12) + MailHandler.Provider;
            if (chkRandomAlias.Checked)
                _alias = GetRandomString(12);
            if (chkRandomPass.Checked)
                _pass = System.Web.Security.Membership.GeneratePassword(12, 4);

            for (var i = 0; i < nmbrAmountAccounts.Value; i++)
            {
                
            }

            _status = "Creating account...";
            UpdateStatus();

            CreateAccount();

            var verified = false;
            do
            {
                VerifyMail();
                verified = CheckIfMailIsVerified();
                UpdateStatus();
                await Task.Delay(2000);
            } while (!verified);
            UpdateStatus();

            FinishCreation();
            UpdateStatus();

            WriteAccountIntoFile();
        }

        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];

            for (var i = 0; i < length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        private void CreateAccount()
        {
            bool success;

            do
            {
                //Ask for captcha
                ShowCaptchaDialog();
                success = _httpHandler.CreateAccount(_mail, _captcha, ref _status);
                UpdateStatus();

                if (_status == Error.EMAIL_ERROR)
                {
                    return;
                }
            } while (!success);
        }

        private void VerifyMail()
        {
            if (chkAutoVerifyMail.Checked)
            {
                _mailHandler.ConfirmMail(_mail);
            }
            else
            {
                Clipboard.SetText(_mail);
            }
        }

        private bool CheckIfMailIsVerified()
        {
            return _httpHandler.CheckEmailVerified(ref _status);
        }

        private void FinishCreation()
        {
            while (!_httpHandler.CompleteSignup(_alias, _pass, ref _status))
            {
                UpdateStatus();
                if (_status == "Password not safe enough")
                    ShowUpdateInfoBox(true);
                else if (_status == "Alias already in use")
                    ShowUpdateInfoBox(false);
                else
                    return;
            }
        }

        private void WriteAccountIntoFile()
        {
            if (chkWriteIntoFile.Checked)
            {
                _fileManager.WriteIntoFile(_mail, _alias, _pass);
            }
        }

        private void UpdateStatus()
        {
            lblStatusInfo.Text = _status;
        }

        private void ShowUpdateInfoBox(bool updatePass)
        {
            var inputDialog = new InputDialog(_status);

            if (inputDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (updatePass)
                    _pass = inputDialog.txtInfo.Text;
                else
                    _alias = inputDialog.txtInfo.Text;
            }
            inputDialog.Dispose();
        }

        private void ShowCaptchaDialog()
        {
            var captchaDialog = new CaptchaDialog(_httpHandler);

            if (captchaDialog.ShowDialog(this) == DialogResult.OK)
            {
                _captcha = captchaDialog.txtCaptcha.Text;
            }
        }

        private void ChkRandomMail_CheckedChanged(object sender, EventArgs e)
        {
            chkAutoVerifyMail.Enabled = chkRandomMail.Checked;
            chkAutoVerifyMail.Checked = chkRandomMail.Checked;
            txtEmail.Enabled = !chkRandomMail.Checked;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.Enabled = !chkRandomPass.Checked;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomAlias_CheckedChanged(object sender, EventArgs e)
        {
            txtAlias.Enabled = !chkRandomAlias.Checked;
            ToggleForceWriteIntoFile();
        }

        private void ToggleForceWriteIntoFile()
        {
            var shouldForce = chkRandomMail.Checked || chkRandomPass.Checked || chkRandomAlias.Checked;
                chkWriteIntoFile.Checked = shouldForce;
                chkWriteIntoFile.Enabled = !shouldForce;
        }
    }
}
