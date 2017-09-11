using System.Net.Http;
using System.Text;
using Domain;
using Domain.TelegramEntities;
using Newtonsoft.Json;

namespace SchedulerBot
{
    public class TelegramApiProxy
    {
        private string _apiUrl;

        public HttpResponseMessage SendMessage(SendMessage message)
        {
            var client = new HttpClient();
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            var jsonData = JsonConvert.SerializeObject(message,serializerSettings);
            var content = new StringContent(jsonData,Encoding.UTF8, "application/json");
            return client.PostAsync(_apiUrl + BotConfig.SendMessage, content).Result;
        }

        public TelegramApiProxy()
        {
            _apiUrl = BotConfig.Url + BotConfig.Token;
        }
    }
}