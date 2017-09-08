using System;
using System.Net;
using System.Net.Http;

namespace Domain
{
    public class TelegramApiProxy
    {
        private string _apiUrl;

        public void SendMessage(Update update)
        {
            var request = WebRequest.Create(_apiUrl + "sendMessage");
        }

        public TelegramApiProxy()
        {
            _apiUrl = BotConfig.Url + BotConfig.Token+'/';
        }
    }
}