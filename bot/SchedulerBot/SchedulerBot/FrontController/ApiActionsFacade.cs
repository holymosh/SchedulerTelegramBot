using System.Collections.Generic;
using Domain;
using Domain.Interfaces;
using Domain.TelegramEntities;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using SchedulerBot.Proxies;

namespace SchedulerBot.FrontController
{
    public class ApiActionsFacade : IApiActionsFacade
    {
        private readonly ITelegramApiProxy _proxy;
        private readonly IStudentRepository _studentRepository;
        private Queue<ScheduleContext> _contexts;
        private readonly IButtonFactoryMethod _buttonFactory;
        private readonly IUpdateReader _updateReader;

        public ApiActionsFacade(ITelegramApiProxy proxy ,
                                IStudentRepository studentRepository,
                                IButtonFactoryMethod buttonsFactory,
                                IUpdateReader reader)
        {
            _studentRepository = studentRepository;
            _proxy = proxy;
            _contexts = new Queue<ScheduleContext>();
            _buttonFactory = buttonsFactory;
            _updateReader = reader;
        }

        public void Start(Update update)
        {
            var userExists = _studentRepository.UseContext(_contexts.Dequeue()).IsRegistered(update.message.from.id);
            InlineKeyboardMarkup markup = _buttonFactory.CreateStartMenu(userExists);
            var message = new SendMessage(_updateReader.GetUserId(update), "Тыкни на кнопку", markup);
            _proxy.SendMessage(message);
        }

        public void SendInformationAboutBot(Update update)
        {
            _contexts.Dequeue();
            var message = new SendMessage(_updateReader.GetUserId(update), "Бот для расписания в мисосе");
            _proxy.SendMessage(message);
        }

        public void SendError(Update update)
        {
            
            var message = new SendMessage(_updateReader.GetUserId(update), "команда не найдена");
            _proxy.SendMessage(message);
        }

        public void AddContext(ScheduleContext context)
        {
            _contexts.Enqueue(context);
        }
    }
}