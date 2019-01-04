using System;
using System.Windows.Forms;
using SteamAccCreator.File;
using SteamAccCreator.Web;
using System.Threading.Tasks;

namespace SteamAccCreator.Gui
{
    public class AccountCreator
    {
        private static readonly Random Random = new Random();

        private string _status;

        private readonly HttpHandler _httpHandler = new HttpHandler();
        private readonly FileManager _fileManager = new FileManager();
        private readonly MailHandler _mailHandler = new MailHandler();
        private readonly MainForm _mainForm;

        private string _alias, _pass, _mail, _captcha = string.Empty;
        private readonly string _enteredAlias;
        private readonly int _index;

        public AccountCreator(MainForm mainForm, string mail, string alias, string pass, int index)
        {
            _mainForm = mainForm;
            _mail = mail;
            _alias = alias;
            _enteredAlias = alias;
            _pass = pass;
            _index = index;
        }

        public async void Run()
        {

            if (_mainForm.RandomAlias)
                _alias = GetRandomString(12);
            else
                _alias = _enteredAlias + _index;
            if (_mainForm.RandomPass)
                _pass = GetRandomString(24);
            if (_mainForm.RandomMail)
                _mail = GetRandomString(12) + MailHandler.Provider;

            _mainForm.AddToTable(_mail, _alias, _pass);
            _status = "Creating account...";

            StartCreation();

            bool verified;
            do
            {
                VerifyMail();
                verified = CheckIfMailIsVerified();
                await Task.Delay(2000);
            } while (!verified);

            FinishCreation();

            WriteAccountIntoFile();
            _status = "Finished";
            UpdateStatus();
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

        private void StartCreation()
        {
            bool success;

            do
            {
                //Ask for captcha
                _captcha = ShowCaptchaDialog(_httpHandler);
                success = _httpHandler.CreateAccount(_mail, _captcha, ref _status);

                if (_status == Error.EMAIL_ERROR)
                {
                    return;
                }
            } while (!success);
        }

        private void VerifyMail()
        {
            if (_mainForm.AutoMailVerify)
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
                switch (_status)
                {
                    case Error.PASSWORD_UNSAFE:
                        _pass = ShowUpdateInfoBox(_status);
                        break;
                    case Error.ALIAS_UNAVAILABLE:
                        _alias = ShowUpdateInfoBox(_status);
                        break;
                    default:
                        return;
                }
            }
        }   

        private void WriteAccountIntoFile()
        {
            if (_mainForm.WriteIntoFile)
            {          
                _fileManager.WriteIntoFile(_mail, _mainForm.istrue, _alias, _pass, _mainForm.Path, _mainForm.original);
            }
        }

        private void UpdateStatus()
        {
            _mainForm.UpdateStatus(_index, _status);
        }

        private string ShowUpdateInfoBox(string status)
        {
            var inputDialog = new InputDialog(status);
            var update = string.Empty;

            if (inputDialog.ShowDialog() == DialogResult.OK)
            {
                update = inputDialog.txtInfo.Text;
            }
            inputDialog.Dispose();
            return update;
        }

        private string ShowCaptchaDialog(HttpHandler httpHandler)
        {
            var captchaDialog = new CaptchaDialog(httpHandler);
            var captcha = string.Empty;

            if (captchaDialog.ShowDialog() == DialogResult.OK)
            {
                captcha = captchaDialog.txtCaptcha.Text;
            }
            captchaDialog.Dispose();
            return captcha;
        }
    }
}
