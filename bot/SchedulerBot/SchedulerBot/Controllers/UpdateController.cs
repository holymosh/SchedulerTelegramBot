using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using SchedulerBot.FrontController;
using SchedulerBot.Proxies;

namespace SchedulerBot.Controllers
{
    public class UpdateController : Controller
    {
        private RequestLogger _logger;
        private ITelegramFrontController _frontController;
        private ScheduleContext _context;
        private DatabaseContextProxy _contextProxy;

        public UpdateController(RequestLogger Logger, 
                                ITelegramFrontController telegramFrontController, 
                                ScheduleContext context,
                                DatabaseContextProxy contextProxy)
        {
            _logger = Logger;
            _frontController = telegramFrontController;
            _context = context;
            _contextProxy = contextProxy;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            //var lessons = new SortedSet<Course>(){new Course(){Name = "TSP",
            //    Location = "B-902",StartHour = 9,EndHour = 11,StartMinute = 0,EndMinute = 0,
            //    Teacher = new Teacher(){Name = "gop",FatherName = "israeli", Surname = "shiheeva"} }};
            //var days = new SortedSet<Day>() { new Day() { Name = "monday", Lessons = lessons } };
            //var students = new SortedSet<Student>() { new Student() { FirstName = "holy", LastName = "mosh", IsAdmin = true, Id = "123123112" } };
            //var schedule = new Schedule() { Days = days };
            //var group = new Group() { Name = "MM-15-2", Schedule = schedule, Students = students };
            //_context.SaveGroup(group);
            //var result = _context.GetGroupsByName(group.Name);
            //return Ok(result);
            return Ok(_context.Groups.First());
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
            _contextProxy.SetContext();
            _frontController.DoAction(update);
            return Ok();
        }
    }
}