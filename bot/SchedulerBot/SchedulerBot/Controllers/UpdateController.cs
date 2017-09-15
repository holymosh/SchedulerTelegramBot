using System.Linq;
using Domain;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using SchedulerBot.Facade;

namespace SchedulerBot.Controllers
{
    public class UpdateController : Controller
    {
        private RequestLogger _logger;
        private ActionFacade _facade;
        private ScheduleContext _context;

        public UpdateController(RequestLogger Logger, ActionFacade ActionFacade, ScheduleContext context)
        {
            _logger = Logger;
            _facade = ActionFacade;
            _context = context;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var teacher = new Teacher();
            teacher.FatherName = "popengauz";
            teacher.Name = "kekauz";
            teacher.Surname = "huyauz";
            _context.Teachers.Add(teacher);
            return Ok(_context.Teachers.SingleOrDefault(teacher1 => teacher.FatherName.Equals("popengauz")));
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