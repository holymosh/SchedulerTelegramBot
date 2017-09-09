using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Domain
{
    public class TelegramApiProxy
    {
        private string _apiUrl;

        public HttpResponseMessage SendMessage(string message)
        {
            var client = new HttpClient();
            //var b = client.DefaultRequestHeaders.
            //client.DefaultRequestHeaders.Add("Content-Type","application/json");
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var content = new StringContent(message,Encoding.UTF8, "application/json");
            return client.PostAsync(_apiUrl + BotConfig.SendMessage, content).Result;
        }

        public TelegramApiProxy()
        {
            _apiUrl = BotConfig.Url + BotConfig.Token;
        }
    }
}