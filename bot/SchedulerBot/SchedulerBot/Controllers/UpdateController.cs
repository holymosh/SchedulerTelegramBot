using System;
using Domain;
using Domain.TelegramEntities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SchedulerBot.Controllers
{
    public class UpdateController : Controller
    {
        private RequestLogger _logger;

        public UpdateController(RequestLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("logger/{}")]
        public IActionResult last()
        {
            return Ok(_logger.lastJson);
        }


        [HttpPost]
        [Route("update")]
        public IActionResult TestPost([FromBody] Update update)
        {
            _logger.lastJson =  update;
            TelegramApiProxy apiProxy = new TelegramApiProxy();
            var message = JsonConvert.SerializeObject(new SendMessage(update.message.chat.id.ToString(), "флуд"));
            apiProxy.SendMessage(message);
            
            return Ok();
        }
    }
}