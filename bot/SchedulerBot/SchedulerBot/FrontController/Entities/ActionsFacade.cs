using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ActionsFacade : IActionsFacade
    {
        private readonly ITelegramApiProxy _proxy;
        private readonly IStudentRepository _studentRepository;
        private Queue<ScheduleContext> _contexts;
        private readonly IButtonCreator _buttonCreator;
        private readonly IUpdateReader _updateReader;
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDateTimeManager _dateTimeManager;
        private readonly IDataToMessageMapper _dataToMessageMapper;
        private readonly ITeacherRepository _teacherRepository;

        private async Task SendMessagesAsync(IEnumerable<Student> groupmates, Update update)
        {
            var messageOwner =
                groupmates.SingleOrDefault(student => student.Id.Equals(_updateReader.GetUserId(update)));
            var message = update.message.text;
            var taskFactory = new  TaskFactory(TaskCreationOptions.AttachedToParent,TaskContinuationOptions.None);
            foreach (var groupmate in groupmates)
            {
                await taskFactory.StartNew(() => _proxy.SendMessage(new SendMessage(groupmate.Id, message)));
            }
        }

        public ActionsFacade(ITelegramApiProxy proxy,
            IStudentRepository studentRepository,
            IButtonCreator buttonsCreator,
            IUpdateReader reader,
            IGroupRepository groupRepository,
            ICourseRepository courseRepository,
            IDateTimeManager dateTimeManager,
            IDataToMessageMapper dataToMessageMapper,
            ITeacherRepository teacherRepository
        )
        {
            _studentRepository = studentRepository;
            _proxy = proxy;
            _contexts = new Queue<ScheduleContext>();
            _buttonCreator = buttonsCreator;
            _updateReader = reader;
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _dateTimeManager = dateTimeManager;
            _dataToMessageMapper = dataToMessageMapper;
            _teacherRepository = teacherRepository;
        }

        public void StartDialog(Update update)
        {
            var userId = _updateReader.GetUserId(update);
            var messageId = _updateReader.GetMessageId(update) + 1;
            var userExists = _studentRepository.UseContext(_contexts.Dequeue()).IsRegistered(userId);
            var markup = _buttonCreator.CreateStartMenu(userExists, messageId.ToString());
            var message = new SendMessage(userId, "Меню", markup);
            _proxy.SendMessage(message);
        }

        public void SendInformationAboutBot(Update update)
        {
            _contexts.Dequeue();
            var message = new SendMessage(_updateReader.GetUserId(update),
                "Не трать свою память на расписание, для этого есть scheduler. Разработчик - @holymosh");
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
            var user = update.message.from;
            var student = new Student(user.id, user.first_name, user.last_name,
                isAdmin: false, groupId: Convert.ToInt32(_updateReader.GetArgument(update)));
            _studentRepository.UseContext(_contexts.Dequeue()).Register(student);
            var userId = _updateReader.GetUserId(update);
            var messageId = _updateReader.GetMessageId(update) + 1;
            var markup = _buttonCreator.CreateStartMenu(IsRegistered: true, messageId: messageId.ToString());
            var message = new SendMessage(userId, "Меню", markup);
            _proxy.SendMessage(message);

        }

        public void ExitFromGroup(Update update)
        {
            _studentRepository.UseContext(_contexts.Dequeue()).RemoveStudent(_updateReader.GetUserId(update));
            var markup = _buttonCreator.CreateStartMenu(IsRegistered: false,
                messageId: update.message.message_id.ToString());
            var message = new SendMessage(_updateReader.GetUserId(update), "Меню", markup);
            _proxy.SendMessage(message);
        }

        public void InviteGroupmate(Update update)
        {
            var group = _groupRepository.UseContext(_contexts.Dequeue())
                .GetGroupByStudent(_updateReader.GetUserId(update));
            var markup = _buttonCreator.CreateInlineInviteButton(group.Name, group.Id);
            var message = new SendMessage(_updateReader.GetUserId(update), group.Name, markup);
            _proxy.SendMessage(message);
        }

        public void SendAnswerForInlineQuery(Update update)
        {
            var groupName = _groupRepository.UseContext(_contexts.Dequeue())
                .GetGroupNameById(Convert.ToInt32(update.inline_query.query));
            var answer = _buttonCreator.CreateInlineAnswer(update, groupName);
            _proxy.SendInlineAnswer(answer);
        }

        public void SendTomorrowLessons(Update update)
        {
            _courseRepository.UseContext(_contexts.Dequeue());
            var userId = _updateReader.GetUserId(update);
            var messageId = _updateReader.GetArgument(update);
            if (DateTime.Today.DayOfWeek.Equals(DayOfWeek.Saturday))
            {
                _proxy.EditMessage(new EditMessageText(userId,
                    _updateReader.GetArgument(update),
                    "В воскресенье пар нет",
                    _buttonCreator.CreateBackButton(messageId)));
            }
            else
            {
                var lessons = _courseRepository.GetLessonsAtDay(userId,
                    _dateTimeManager.GetNextDayName(), _dateTimeManager.GetInvertedWeekType());
                var message = new EditMessageText(userId, messageId,
                    _dataToMessageMapper.CreateMessageWithSchedule(lessons),
                    _buttonCreator.CreateBackButton(messageId));
                _proxy.EditMessage(message);
            }
        }

        public void SendNextLessons(Update update)
        {
            var userId = _updateReader.GetUserId(update);
            var lessons = _courseRepository.UseContext(_contexts.Dequeue()).GetNextLessons(userId,
                _dateTimeManager.GetCurrentDayName(),
                _dateTimeManager.GetInvertedWeekType());
            _proxy.EditMessage(new EditMessageText(userId, _updateReader.GetArgument(update),
                _dataToMessageMapper.CreateMessageWithSchedule(lessons),
                _buttonCreator.CreateBackButton(_updateReader.GetArgument(update))));
        }

        public void SendCurrentTeacherData(Update update)
        {
            var userId = _updateReader.GetUserId(update);
            var context = _contexts.Dequeue();
            _teacherRepository.UseContext(context);
            _courseRepository.UseContext(context);
            try
            {
                var teacher = _teacherRepository.GetCurrentTeacher(_dateTimeManager.GetCurrentDayName(),
                    _dateTimeManager.GetInvertedWeekType(), userId, _courseRepository.GetNextLessons);
                _proxy.EditMessage(new EditMessageText(userId, _updateReader.GetArgument(update),
                    _dataToMessageMapper.CreateMessageFromTeacherData(teacher),
                    _buttonCreator.CreateBackButton(userId)));
            }
            catch (Exception e)
            {
                _proxy.EditMessage(new EditMessageText(userId, _updateReader.GetArgument(update),
                    "Предметы в данный момент не найдены", _buttonCreator.CreateBackButton(userId)));
            }
        }

        public void BackToTheMenu(Update update)
        {
            _contexts.Dequeue();
            var messageId = _updateReader.GetMessageId(update);
            var userId = _updateReader.GetUserId(update);
            _proxy.EditMessage(new EditMessageText(userId, messageId.ToString(), "Меню",
                _buttonCreator.CreateStartMenu(IsRegistered: true, messageId: messageId.ToString())));
        }

        public void SendWeekButtons(Update update)
        {
            _contexts.Dequeue();
            var messageId = _updateReader.GetMessageId(update).ToString();
            var userId = _updateReader.GetUserId(update);
            var buttons = _buttonCreator.CreateWeekButtons(messageId);
            _proxy.EditMessage(new EditMessageText(userId, messageId, "Выберите день недели", buttons));
        }

        public void SendLessonsAtCustomDay(Update update)
        {
            _courseRepository.UseContext(_contexts.Dequeue());
            var userId = _updateReader.GetUserId(update);
            var messageId = _updateReader.GetArgument(update);
            var command = _updateReader.GetCommand(update);
            var lessons = _courseRepository.GetLessonsAtDay(userId,
                _dateTimeManager.GetDayNameByCommand(command), _dateTimeManager.GetInvertedWeekType());
            var message = new EditMessageText(userId, messageId,
                _dataToMessageMapper.CreateMessageWithSchedule(lessons),
                _buttonCreator.CreateBackToWeekButton(messageId));
            _proxy.EditMessage(message);
        }

        public async void SendMessageToGroupmates(Update update)
        {
            var groupmates = _studentRepository.UseContext(_contexts.Dequeue())
                .GetGroupmates(_updateReader.GetUserId(update)).ToList();
                await SendMessagesAsync(groupmates, update);
        }
    }
}