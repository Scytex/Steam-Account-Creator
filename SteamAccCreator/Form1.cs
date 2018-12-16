using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
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

        public Form1()
        {
            InitializeComponent();
        }

        private readonly HttpHandler _httpHandler = new HttpHandler();
        private readonly FileManager _fileManager = new FileManager();

        private async void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            _alias = txtAlias.Text;
            _pass = txtPass.Text;
            _mail = txtEmail.Text;
            _status = "Creating account...";
            UpdateStatus();


            //Create account using mail
            bool success;

            do
            {
                ShowCaptchaDialog();
                success = _httpHandler.CreateAccount(_mail, _captcha, ref _status);
                UpdateStatus();

                if (_status == Error.EMAIL_ERROR)
                {
                    return;
                }
            } while (!success);


            //TODO Verify mail

            //Check if mail is verified
            while (!_httpHandler.CheckEmailVerified(ref _status))
            {
                UpdateStatus();
                await Task.Delay(1000);
            }
            UpdateStatus();

            //Finish creation with alias & password
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
            UpdateStatus();

            //Write account into file
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
