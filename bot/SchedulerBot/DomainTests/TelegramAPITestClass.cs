using System;
using System.Net.Http;
using Domain;
using Domain.TelegramEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DomainTests
{
    [TestClass]
    public class TelegramAPITestClass
    {
        [TestMethod]
        public void SendMessageToTelegram()
        {
            BotConfig config = new BotConfig();
            Assert.IsFalse(BotConfig.SendMessage.Equals(String.Empty));
            var request = new HttpClient();
            var task = request.GetAsync("https://schedulerbot20170906115714.azurewebsites.net/obj");
            task.Wait();
            var secondTask = task.Result.Content.ReadAsStringAsync();
            secondTask.Wait();
            var responseString = secondTask.Result;
            var update = JsonConvert.DeserializeObject<Update>(responseString);
            var apiProxy = new TelegramApiProxy();
            var json = JsonConvert.SerializeObject(new SendMessage("136329961", "test"));
            var response = apiProxy.SendMessage(json);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
