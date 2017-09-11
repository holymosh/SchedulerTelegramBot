using Domain;
using Microsoft.AspNetCore.Mvc;
using SchedulerBot.Facade;

namespace SchedulerBot.Controllers
{
    public class UpdateController : Controller
    {
        private RequestLogger _logger;
        private ActionFacade _facade;

        public UpdateController(RequestLogger Logger, ActionFacade ActionFacade)
        {
            _logger = Logger;
            _facade = ActionFacade;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("tested");
        }

        [HttpGet]
        [Route("update/{actions}/{index}")]
        public IActionResult ActionResult(LoggerActions actions, int index)
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
            _facade.DoAction(update);
            return Ok();
        }
    }
}