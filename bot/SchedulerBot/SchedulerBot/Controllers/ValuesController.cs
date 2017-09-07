using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace SchedulerBot.Controllers
{
    public class ValuesController : Controller
    {
        private RequestLogger _logger;

        public ValuesController(RequestLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("obj")]
        public IActionResult last()
        {
            return Ok(_logger.lastJson);
        }


        [HttpPost]
        [Route("update")]
        public IActionResult TestPost([FromBody] Update update)
        {
            _logger.lastJson = update;
            return Ok();
        }
    }
}