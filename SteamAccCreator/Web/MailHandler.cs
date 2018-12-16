using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace SteamAccCreator.Web
{
    public class MailHandler
    {
        private readonly RestClient _client = new RestClient();
        private readonly RestRequest _request = new RestRequest();

        public static string Provider = "@xitroo.com";

        private static readonly Uri MailboxUri = new Uri("https://api.xitroo.com/v1/mails");
        private static readonly Uri MailUri = new Uri("https://api.xitroo.com/v1/mail");
        private static readonly Uri SteamUri = new Uri("https://store.steampowered.com/account/newaccountverification?");

        //private static readonly Regex FromRegex = new Regex(@".Steam.*");
        private static readonly Regex ConfirmMailRegex = new Regex("stoken=([^&]+).*creationid=([^\"]+)");

        public void ConfirmMail(string address)
        {
            _client.BaseUrl = MailboxUri;
            _request.Method = Method.GET;
            dynamic jsonResponse;

            _request.AddParameter("locale", "en");
            _request.AddParameter("mailAddress", address);
            _request.AddParameter("mailsPerPage", "25");
            _request.AddParameter("minTimestamp", "0");

            do
            {
                _request.AddOrUpdateParameter("maxTimestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                var response = _client.Execute(_request);
                jsonResponse = JsonConvert.DeserializeObject(response.Content);
            } while (jsonResponse.totalMails < 1);

            _request.Parameters.Clear();
            try
            {
                foreach (var mail in jsonResponse.mails)
                {
                    //if (FromRegex.IsMatch(mail.from.Value))
                    if (mail.from.Value.Contains("noreply@steampowered.com"))
                    {
                        var mailText = ReadMail(mail._id.Value);
                        var confirmUri = GetConfirmUri(mailText);
                        ConfirmSteamAccount(confirmUri);
                    }
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private Uri GetConfirmUri(string base64Text)
        {
            var data = Convert.FromBase64String(base64Text);
            var decodedString = Encoding.UTF8.GetString(data);
            var matches = ConfirmMailRegex.Matches(decodedString);
            var token1 = matches[0].Groups[1].Value;
            var token2 = matches[0].Groups[2].Value;
            var tokenUri = "stoken=" + token1 + "&creationid=" + token2;

            return new Uri(SteamUri + tokenUri);
        }

        private string ReadMail(string mailId)
        {
            _client.BaseUrl = MailUri;
            _request.Method = Method.GET;
            _request.AddParameter("locale", "en");
            _request.AddParameter("id", mailId);
            var response = _client.Execute(_request);
            _request.Parameters.Clear();

            dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);

            return jsonResponse.bodyHtmlStrict;
        }

        private void ConfirmSteamAccount(Uri uri)
        {
            _client.BaseUrl = uri;
            _request.Method = Method.GET;
            _client.Execute(_request);
            _request.Parameters.Clear();
        }
    }
}