using System.Collections.Generic;
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
            var lessons = new SortedSet<Course>(){new Course(){Name = "TSP",
                Location = "B-902",StartHour = 9,EndHour = 11,StartMinute = 0,EndMinute = 0,
                Teacher = new Teacher(){Name = "gop",FatherName = "israeli", Surname = "shiheeva"} }};
            var days = new SortedSet<Day>() { new Day() { Name = "monday", Lessons = lessons } };
            var students = new SortedSet<Student>() { new Student() { FirstName = "holy", LastName = "mosh", IsAdmin = true, Id = "123123112" } };
            var schedule = new Schedule() { Days = days };
            var group = new Group() { Name = "MM-15-2", Schedule = schedule, Students = students };
            _context.SaveGroup(group);
            var result = _context.GetGroupsByName(group.Name);
            return Ok(result);
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