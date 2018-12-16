using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RestSharp;
using Image = System.Drawing.Image;

namespace SteamAccCreator.Web
{
    public class HttpHandler
    {
        private readonly RestClient _client = new RestClient();
        private readonly RestRequest _request = new RestRequest();

        private string _captchaGid = string.Empty;
        private string _sessionId = string.Empty;

        private static readonly Uri JoinUri = new Uri("https://store.steampowered.com/join/");
        private static readonly Uri CaptchaUri = new Uri("https://store.steampowered.com/login/rendercaptcha?gid=");
        private static readonly Uri VerifyCaptchaUri = new Uri("https://store.steampowered.com/join/verifycaptcha/");
        private static readonly Uri AjaxVerifyCaptchaUri = new Uri("https://store.steampowered.com/join/ajaxverifyemail");
        private static readonly Uri AjaxCheckEmailVerifiedUri = new Uri("https://store.steampowered.com/join/ajaxcheckemailverified");
        private static readonly Uri CheckAvailUri = new Uri("https://store.steampowered.com/join/checkavail/");
        private static readonly Uri CheckPasswordAvailUri = new Uri("https://store.steampowered.com/join/checkpasswordavail/");
        private static readonly Uri CreateAccountUri = new Uri("https://store.steampowered.com/join/createaccount/");

        private static readonly Regex CaptchaRegex = new Regex(@"\/rendercaptcha\?gid=([0-9]+)\D");
        private static readonly Regex BoolRegex = new Regex(@"(true|false)");

        public Image GetCaptchaImage()
        {
            //load Steam page
            _client.BaseUrl = JoinUri;
            _request.Method = Method.GET;
            var response = _client.Execute(_request);
            
            //Store captcha ID
            _captchaGid = CaptchaRegex.Matches(response.Content)[0].Groups[1].Value;

            //download and return captcha image
            _client.BaseUrl = new Uri(CaptchaUri + _captchaGid);
            var captchaResponse = _client.DownloadData(_request);
            using (var ms = new MemoryStream(captchaResponse))
            {
                return Image.FromStream(ms);
            }
        }

        public bool CreateAccount(string email, string captchaText, ref string status)
        {
            _client.BaseUrl = VerifyCaptchaUri;
            _request.Method = Method.POST;
            _request.AddParameter("captchagid", _captchaGid);
            _request.AddParameter("captcha_text", captchaText);
            _request.AddParameter("email", email);
            _request.AddParameter("count", "1");

            var response = _client.Execute(_request);
            _request.Parameters.Clear();

            if (!response.IsSuccessful)
            {
                status = "HTTP Request failed";
                return false;
            }

            var matches = BoolRegex.Matches(response.Content);
            var bCaptchaMatches = bool.Parse(matches[0].Value);
            var bEmailAvail = bool.Parse(matches[1].Value);

            if (!bCaptchaMatches)
            {
                status = "Captcha wrong";
                return false;
            }

            if (!bEmailAvail)
            {
                //seems to always return true even if email is already in use
                status = "Email error";
                return false;
            }

            //Send request again
            _client.BaseUrl = AjaxVerifyCaptchaUri;
            _request.AddParameter("captchagid", _captchaGid);
            _request.AddParameter("captcha_text", captchaText);
            _request.AddParameter("email", email);

            response = _client.Execute(_request);
            _request.Parameters.Clear();

            dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
            if (jsonResponse.success != 1)
            {
                switch (jsonResponse.success)
                {
                    case 62:
                        status = "This e-mail address must be different from your own.";
                        break;
                    case 13:
                        status = "Please enter a valid email address.";
                        break;
                    case 17:
                        status = "It appears you've entered a disposable email address, or are using an email provider that cannot be used on Steam. Please provide a different email address.";
                        break;
                    default:
                        status = "Unknown error @ CreateAccount()";
                        break;
                }
                return false;
            }

            _sessionId = jsonResponse.sessionid;
            status = "Waiting for email to be verified";

            return true;
        }

        public bool CheckEmailVerified(ref string status)
        {
            _client.BaseUrl = AjaxCheckEmailVerifiedUri;
            _request.Method = Method.POST;
            _request.AddParameter("creationid", _sessionId);

            var response = _client.Execute(_request);
            _request.Parameters.Clear();

            switch (response.Content)
            {
                case "1":
                    status = "Email confirmed";
                    return true;

                case "42":
                case "29":
                    status = "There was an error with your registration, please try again.";
                    break;

                case "27":
                    status = "You've waited too long to verify your email. Please try creating your account and verifying your email again.";
                    break;

                case "36":
                case "10":
                    status = "Mail not verified";
                    break;

                default:
                    status = "Unknown error @ CheckEmailVerified()";
                    break;
            }
            return false;
        }

        public bool CompleteSignup(string alias, string password, ref string status)
        {
            /*  not needed
            _client.BaseUrl = new Uri(CompleteSignupUri + _sessionId);
            _request.Method = Method.GET;

            var response = _client.Execute(_request);
            */

            if (!CheckAlias(alias, ref status))
                return false;
            if (!CheckPassword(password, alias, ref status))
                return false;

            _client.BaseUrl = CreateAccountUri;
            _request.Method = Method.POST;
            _request.AddParameter("accountname", alias);
            _request.AddParameter("password", password);
            _request.AddParameter("creation_sessionid", _sessionId);
            _request.AddParameter("count", "1");
            _request.AddParameter("lt", "0");

            var response = _client.Execute(_request);
            _request.Parameters.Clear();

            dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
            if (jsonResponse.bSuccess == "true")
            {
                status = "Account created";
                return true;
            }
            status = jsonResponse.details;
            return false;
        }

        private static bool CheckAlias(string alias, ref string status)
        {
            var tempClient = new RestClient(CheckAvailUri);
            var tempRequest = new RestRequest(Method.POST);
            tempRequest.AddParameter("accountname", alias);
            tempRequest.AddParameter("count", "1");

            var response = tempClient.Execute(tempRequest);
            dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);

            if (jsonResponse.bAvailable == "true")
                return true;
            status = "Alias already in use";
            return false;
        }

        private static bool CheckPassword(string password, string alias, ref string status)
        {
            var tempClient = new RestClient(CheckPasswordAvailUri);
            var tempRequest = new RestRequest(Method.POST);
            tempRequest.AddParameter("password", password);
            tempRequest.AddParameter("accountname", alias);
            tempRequest.AddParameter("count", "1");

            var response = tempClient.Execute(tempRequest);
            dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);

            if (jsonResponse.bAvailable == "true")
                return true;
            status = "Password not safe enough";
            return false;
        }


    }
}