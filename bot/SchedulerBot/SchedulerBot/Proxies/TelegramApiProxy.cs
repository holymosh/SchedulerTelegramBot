using System.Net.Http;
using System.Text;
using Domain;
using Domain.TelegramEntities;
using Newtonsoft.Json;

namespace SchedulerBot.Proxies
{
    public class TelegramApiProxy : ITelegramApiProxy
    {
        private string _apiUrl;

        private RequestLogger _logger;

        public TelegramApiProxy(RequestLogger logger)
        {
            _logger = logger;
            _apiUrl = BotConfig.Url + BotConfig.Token;
        }

        public HttpResponseMessage SendMessage(SendMessage message)
        {
            var client = new HttpClient();
            var serializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            var jsonData = JsonConvert.SerializeObject(message,serializerSettings);
            var content = new StringContent(jsonData,Encoding.UTF8, "application/json");
            return client.PostAsync(_apiUrl + BotConfig.SendMessage, content).Result;
        }

        public HttpResponseMessage SendInlineAnswer(AnswerInlineQuery answerInlineQuery)
        {
            _logger.AnswersList.Add(answerInlineQuery);
            var client = new HttpClient();
            var serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var jsonData = JsonConvert.SerializeObject(answerInlineQuery, serializerSettings);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response =  client.PostAsync(_apiUrl + BotConfig.AnswerInlineQuery, content).Result;
            _logger.O = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            return response;
        }

    }
}