using System;
using System.Net;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders;

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
        public void TestPost([FromBody] Update update)
        {
            _logger.lastJson = update;
            WebRequest request = WebRequest.Create("");
        }
    }
}