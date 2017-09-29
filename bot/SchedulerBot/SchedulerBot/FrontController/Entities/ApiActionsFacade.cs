using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.TelegramEntities;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using SchedulerBot.FrontController.Interfaces;
using SchedulerBot.Proxies;

namespace SchedulerBot.FrontController.Entities
{
    public class ApiActionsFacade : IApiActionsFacade
    {
        private readonly ITelegramApiProxy _proxy;
        private readonly IStudentRepository _studentRepository;
        private Queue<ScheduleContext> _contexts;
        private readonly IButtonFactoryMethod _buttonFactory;
        private readonly IUpdateReader _updateReader;
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDateTimeManager _dateTimeManager;
        private readonly IScheduleMapper _scheduleMapper;

        public ApiActionsFacade(ITelegramApiProxy proxy,
            IStudentRepository studentRepository,
            IButtonFactoryMethod buttonsFactory,
            IUpdateReader reader,
            IGroupRepository groupRepository,
            ICourseRepository courseRepository,
            IDateTimeManager dateTimeManager,
            IScheduleMapper scheduleMapper
        )
        {
            _studentRepository = studentRepository;
            _proxy = proxy;
            _contexts = new Queue<ScheduleContext>();
            _buttonFactory = buttonsFactory;
            _updateReader = reader;
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _dateTimeManager = dateTimeManager;
            _scheduleMapper = scheduleMapper;
        }

        public void Start(Update update)
        {
            var userExists = _studentRepository.UseContext(_contexts.Dequeue()).IsRegistered(update.message.from.id);
            var markup = _buttonFactory.CreateStartMenu(userExists);
            var message = new SendMessage(_updateReader.GetUserId(update), "Тыкни на кнопку", markup);
            _proxy.SendMessage(message);
        }

        public void SendInformationAboutBot(Update update)
        {
            _contexts.Dequeue();
            var message = new SendMessage(_updateReader.GetUserId(update), "Бот сделан для тех, " +
              "кто не может запомнить свое расписание в течение четверти или семестра. " +
              "Разработчик - @holymosh");
            _proxy.SendMessage(message);
        }

        public void SendError(Update update)
        {
            _contexts.Dequeue();
            var message = new SendMessage(_updateReader.GetUserId(update), "команда не найдена");
            _proxy.SendMessage(message);
        }

        public void AddContext(ScheduleContext context)
        {
            _contexts.Enqueue(context);
        }

        public void JoinToGroup(Update update)
        {
            var user = update.callback_query.from;
            var student = new Student(user.id, user.first_name, user.last_name,
                isAdmin: false, groupId: Convert.ToInt32(_updateReader.GetArgument(update)));
            _studentRepository.UseContext(_contexts.Dequeue()).Register(student);
        }

        public void ExitFromGroup(Update update)
        {
            _studentRepository.UseContext(_contexts.Dequeue()).RemoveStudent(_updateReader.GetUserId(update));
            var markup = _buttonFactory.CreateStartMenu(IsRegistered: false);
            var message = new SendMessage(_updateReader.GetUserId(update), "Тыкни на кнопку", markup);
            _proxy.SendMessage(message);
        }

        public void InviteGroupmate(Update update)
        {
            var group = _groupRepository.UseContext(_contexts.Dequeue())
                .GetGroupByStudent(_updateReader.GetUserId(update));
            var markup = _buttonFactory.CreateInlineInviteButton(group.Name, group.Id);
            var message = new SendMessage(_updateReader.GetUserId(update), group.Name, markup);
            _proxy.SendMessage(message);
        }

        public void SendAnswerForInlineQuery(Update update)
        {
            var groupName = _groupRepository.UseContext(_contexts.Dequeue())
                .GetGroupNameById(Convert.ToInt32(update.inline_query.query));
            var answer = _buttonFactory.CreateInlineAnswer(update, groupName);
            _proxy.SendInlineAnswer(answer);
        }

        public void GetTomorrowLessons(Update update)
        {
            _courseRepository.UseContext(_contexts.Dequeue());
            if (DateTime.Today.DayOfWeek.Equals(DayOfWeek.Saturday))
            {
                _proxy.SendMessage(new SendMessage(_updateReader.GetUserId(update), "Ае ска бля универ не работает в воскресенье"));
            }
            else
            {
                var lessons = _courseRepository.GetNextDayLessons(_updateReader.GetUserId(update),
                    _dateTimeManager.GetNextDayName(), _dateTimeManager.GetCurrentWeekType());
                _proxy.SendMessage(new SendMessage(_updateReader.GetUserId(update) ,_scheduleMapper.DisplayScheduleForNextDay(lessons)));
            }
        }
    }
}