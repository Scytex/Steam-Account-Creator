using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAccCreator.Web;

namespace SteamAccCreator
{
    public partial class CaptchaDialog : Form
    {
        private readonly HttpHandler _httpHandler;
        public CaptchaDialog(HttpHandler httpHandler)
        {
            InitializeComponent();
            _httpHandler = httpHandler;
            LoadCaptcha();
        }

        private void LoadCaptcha()
        {
            boxCaptcha.Image = _httpHandler.GetCaptchaImage();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TxtCaptcha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnConfirm_Click(sender, e);
        }
    }
}
