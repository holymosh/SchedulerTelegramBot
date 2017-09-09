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
        [Route("update/{action}/{index}")]
        public IActionResult ActionResult(LoggerActions actions , int index)
        {
            switch (actions)
            {
                case LoggerActions.delete:
                    _logger.Updates.RemoveAt(index);
                    return Ok("removed");
                case LoggerActions.get:
                    return Ok(_logger.Updates[index]);
                default:
                    return BadRequest();
            }
        }


        [HttpPost]
        [Route("update")]
        public IActionResult TestPost([FromBody] Update update)
        {
            _logger.Updates.Add(update);
            TelegramApiProxy apiProxy = new TelegramApiProxy();
            var message = JsonConvert.SerializeObject(new SendMessage(update.message.chat.id.ToString(), "флуд"));
            apiProxy.SendMessage(message);
            
            return Ok();
        }
    }
}