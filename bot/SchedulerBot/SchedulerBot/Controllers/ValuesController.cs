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
        [Route("test")]
        public IActionResult TestMethod()
        {
            return Ok(_logger.Data);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult TestPost([FromBody] Object update)
        {
            _logger.Data.Add(update.ToString());
            return Ok();
        }
    }
}