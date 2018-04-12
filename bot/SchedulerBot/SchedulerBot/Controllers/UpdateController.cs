using System.Collections.Generic;
using Domain;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using SchedulerBot.FrontController.Interfaces;
using SchedulerBot.Proxies;

namespace SchedulerBot.Controllers
{
    public class UpdateController : Controller
    {
        private RequestLogger _logger;
        private readonly ITelegramFrontController _frontController;
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
        [Route("saveschedule")]
        public IActionResult Test()
        {
            var gopengauz = new Teacher()
            {
                Name = "Владимир",
                FatherName = "Израилевич",
                Surname = "Гопенгауз",
            };
            var chislennieMetodyLecture = new Course()
            {
                Name = "Численные методы",
                Teacher = gopengauz,
                Location = "A-524",
                StartHour = 14,
                StartMinute = 30,
                EndHour = 16,
                EndMinute = 5,
                LessonType = LessonType.Lecture,
                WeekType = WeekType.All
            };
            var defaultTeacher = new Teacher()
            {
                Name = "Ведет",
                Surname = "Какой-то",
                FatherName = "Препод"
            };
            var englishMonday = new Course()
            {
                Name = "Английский",
                Location = "где-то на 10 или в розовом",
                StartHour = 16,
                StartMinute = 20,
                EndHour = 17,
                EndMinute = 55,
                LessonType = LessonType.Practice,
                WeekType = WeekType.All,
                Teacher = defaultTeacher
                
            };
            var monday = new Day()
            {
                Name = "Понедельник",
                Lessons = new HashSet<Course>()
                {
                    englishMonday , chislennieMetodyLecture
                }
            };
            var fizra1 = new Course()
            {
                Name = "Физра",
                Location = "Горный или Беляево",
                LessonType = LessonType.Practice,
                Teacher = defaultTeacher,
                StartHour = 10,
                StartMinute = 10,
                EndHour = 11,
                EndMinute = 40,
                WeekType = WeekType.All
            }; 
            var kimTnya = new Teacher()
            {
                Name = "Луиза",
                Surname = "Ким-Тян",
                FatherName = "Ревмировна"
            };
            var urchpLecture = new Course()
            {
                Teacher = kimTnya,
                Name = "Уравнения в частных производных",
                Location = "А-304",
                LessonType = LessonType.Lecture,
                WeekType = WeekType.All,
                StartHour = 12,
                StartMinute = 40,
                EndHour = 14,
                EndMinute = 15,
            };
            var krapuhina = new Teacher()
            {
                Name = "Нина",
                FatherName = "Владимировна",
                Surname = "Крапухина",
                
            };
            var matmodKrapuhinaLecture = new Course()
            {
                Teacher = krapuhina,
                Name = "Математическое моделирование",
                LessonType = LessonType.Lecture,
                Location = "Б-907",
                StartHour = 14,
                StartMinute = 30,
                EndHour = 16,
                EndMinute = 5,
                WeekType = WeekType.All
            };
            var matmodKrapuhinaPractise = new Course()
            {
                Teacher = krapuhina,
                StartHour = 16,
                EndHour = 17,
                StartMinute = 20,
                EndMinute = 55,
                Location = "Б-904А",
                LessonType = LessonType.Practice,
                WeekType = WeekType.All,
                Name = "Математическое моделирование",
            };
            var thuesday = new Day()
            {
                Name = "Вторник",
                Lessons = new HashSet<Course>()
                {
                    urchpLecture,matmodKrapuhinaPractise,matmodKrapuhinaLecture,fizra1
                }
            };
            var shiheeva = new Teacher()
            {
                Name = "Валерия",
                Surname = "Шихеева",
                FatherName = "Владимировна",
            };
            var tspSredaPractise = new Course()
            {
                Teacher = shiheeva,
                Name = "Теория случайных процессов",
                Location = "Кафедра",
                WeekType = WeekType.All,
                StartHour = 9,
                StartMinute = 0,
                EndHour = 10,
                EndMinute = 35,
                LessonType = LessonType.Practice
            };
            var akvsntv = new Teacher()
            {
                Name = "Наталья",
                Surname = "Авксентьева",
                FatherName = "Николаевна",
            };
            var urchpPratice = new Course()
            {
                Teacher = akvsntv,
                Name = "Уравнения в частных производных",
                StartHour = 10,
                EndHour = 12,
                StartMinute = 50,
                EndMinute = 25,
                Location = "A-422",
                LessonType = LessonType.Practice,
                WeekType = WeekType.All,
            };
            var chislMetodySreda = new Course()
            {
                Teacher = gopengauz,
                StartMinute = 40,
                StartHour = 12,
                EndHour = 14,
                EndMinute = 15,
                Name = "Численные методы",
                LessonType = LessonType.Laboratory,
                WeekType = WeekType.All,
                Location = "Б-808"
            };
            var kapalin = new Teacher()
            {
                Name = "Иван",
                FatherName = "Владимирович",
                Surname = "Капалин",
            };
            var tauLecture = new Course()
            {
                Teacher = kapalin,
                Name = "Основы теории автоматического управления",
                Location = "A-524",
                StartHour = 14,
                StartMinute = 30,
                EndHour = 16,
                EndMinute = 5,
                LessonType = LessonType.Lecture,
                WeekType = WeekType.All
            };
            var anglSreda = new Course()
            {
                Teacher = defaultTeacher,
                Name = "Английский",
                StartMinute = 20,
                StartHour = 16,
                EndHour = 17,
                EndMinute = 55,
                LessonType = LessonType.Practice,
                Location = "10 этаж или розовый корпус",
                WeekType = WeekType.All,
            };
            var wednesday = new Day()
            {
                Name = "Среда",
                Lessons = new HashSet<Course>()
                {
                    tspSredaPractise,urchpPratice,tauLecture,chislMetodySreda,anglSreda
                }
            };
            var kurenok = new Teacher()
            {
                Name = "Владимир",
                Surname = "Куренков",
                FatherName = "Вячеславович",
            };
            var kurenki = new Course()
            {
                Teacher = kurenok,
                Name = "Комбинаторика и теория графов",
                StartMinute = 0,
                StartHour = 9,
                EndMinute = 35,
                EndHour = 10,
                LessonType = LessonType.Lecture,
                WeekType = WeekType.All,
                Location = "A-303",
            };
            var fizra2 = new Course()
            {
                Teacher = defaultTeacher,
                Name = "Физра",
                Location = "Беляево или горный",
                WeekType = WeekType.All,
                LessonType = LessonType.Practice,
                StartHour = 11,
                EndHour = 13,
                StartMinute = 50,
                EndMinute = 20,
            };
            var kurenki2 = new Course()
            {
                Teacher = kurenok,
                Name = "Комбинаторика и теория графов",
                StartHour = 14,
                EndHour = 16,
                StartMinute = 30,
                EndMinute = 5,
                LessonType = LessonType.Practice,
                WeekType = WeekType.All,
                Location = "Кафедра"
            };
            var thursday = new Day()
            {
                Name = "Четверг",
                Lessons = new HashSet<Course>()
                {
                    kurenki2,kurenki,fizra2
                }
            };
            var tauPractice = new Course()
            {
                Teacher = kapalin,
                Name = "Основы теории автоматического управления",
                WeekType = WeekType.Even,
                StartHour = 10,
                StartMinute = 50,
                EndHour = 12,
                EndMinute = 25,
                LessonType = LessonType.Practice,
                Location = "Кафедра"
            };
            var tspLectureFriday1 = new Course()
            {
                Teacher = shiheeva,
                Name = "Теория случайных процессов",
                WeekType = WeekType.Uneven,
                Location = "K-513",
                StartHour = 12,
                StartMinute = 40,
                EndHour = 14,
                EndMinute = 15,
                LessonType = LessonType.Lecture
            };
            var tspLectureFriday2 = new Course()
            {
                Teacher = shiheeva,
                Name = "Теория случайных процессов",
                WeekType = WeekType.Even,
                Location = "K-513",
                StartHour = 14,
                StartMinute = 30,
                EndHour = 16,
                EndMinute = 5,
                LessonType = LessonType.Lecture
            };
            var englishriday = new Course()
            {
                Teacher = defaultTeacher,
                Name = "Английский",
                WeekType = WeekType.Even,
                Location = "Б 10 этаж или розовый корпус",
                StartHour = 12,
                StartMinute = 40,
                EndHour = 14,
                EndMinute = 15,
                LessonType = LessonType.Practice
            };
            var tspPracticeFriday1 = new Course()
            {
                Teacher = shiheeva,
                Name = "Теория случайных процессов",
                WeekType = WeekType.Uneven,
                Location = "K-513",
                StartHour = 14,
                StartMinute = 30,
                EndHour = 16,
                EndMinute = 5,
                LessonType = LessonType.Practice
            };
            var tspPracticeFriday2 = new Course()
            {
                Teacher = shiheeva,
                Name = "Теория случайных процессов",
                WeekType = WeekType.Even,
                Location = "K-513",
                StartHour = 16,
                StartMinute = 20,
                EndHour = 17,
                EndMinute = 55,
                LessonType = LessonType.Practice
            };
            var friday = new Day()
            {
                Name = "Пятница",
                Lessons = new HashSet<Course>()
                {
                    tauPractice,tspLectureFriday1,tspPracticeFriday2,tspLectureFriday2,englishriday,tspPracticeFriday1
                }
            };
            var schedule = new Schedule()
            {
                Days = new HashSet<Day>()
                {
                    monday,thuesday,thursday,wednesday,friday
                }
            };
            var group = new Group();
            group.Name = "MM-15-2";
            group.Schedule = schedule;
            group.Students = new HashSet<Student>()
            {
                new Student()
                {
                    Id = "136329961",
                    FirstName = "Dmitry",
                    LastName = "Holymosh",
                    IsAdmin = true,
                    GroupId = group.Id
                }
            };
            group.Schedule.Group = group;
            _context.Groups.Add(group);
            _context.SaveChanges();
            return Ok();
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

        [HttpGet]
        [Route("answers/{index}")]
        public IActionResult ActionResult(int index)
        {
            return Ok(_logger.AnswersList[index]);
        }

        [HttpGet]
        [Route("dynamic")]
        public IActionResult ActionResult()
        {
            return Ok(_logger.O);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult DoAction([FromBody] Update update)
        {
            _logger.Updates.Add(update);
            _contextProxy.SetContext();
            _frontController.DoAction(update);
            return Ok();
        }
    }
}