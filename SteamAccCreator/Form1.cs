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
using SteamAccCreator.Web;

namespace SteamAccCreator
{
    public partial class Form1 : Form
    {
        private string _status = string.Empty;
        private string _alias = string.Empty;
        private string _password = string.Empty;

        public Form1()
        {
            InitializeComponent();
            LoadSteam();
        }

         HttpHandler _httpHandler = new HttpHandler();

        private void BtnLoadSteam_Click(object sender, EventArgs e)
        {
            LoadSteam();
        }

        private async void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            _alias = txtAlias.Text;
            _password = txtPass.Text;
            _status = "Creating account...";
            UpdateStatus();

            var success = _httpHandler.CreateAccount(txtEmail.Text, txtCaptcha.Text, ref _status);
            UpdateStatus();
            if (!success)
                return;

            //TODO Verify mail

            while (!_httpHandler.CheckEmailVerified(ref _status))
            {
                UpdateStatus();
                await Task.Delay(5000);
            }
            UpdateStatus();

            while (!_httpHandler.CompleteSignup(_alias, _password, ref _status))
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
        }



        private void LoadSteam()
        {
            captchaBox.Image = _httpHandler.GetCaptchaImage();
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
                    _password = inputDialog.txtInfo.Text;
                else
                    _alias = inputDialog.txtInfo.Text;
            }
            inputDialog.Dispose();
        }
    }
}
