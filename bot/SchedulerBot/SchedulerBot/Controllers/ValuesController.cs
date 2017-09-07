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

        [HttpGet]
        [Route("upd")]
        public IActionResult TestMethod()
        {
            return Ok(_logger.Data[_logger.Data.Count]);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult TestPost([FromBody] Object update)
        {
            _logger.lastJson = update;
            return Ok();
        }
    }
}