using System.Collections.Generic;
using Domain;
using Domain.TelegramEntities;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using SchedulerBot.Proxies;

namespace SchedulerBot.FrontController
{
    public class ApiActionsFacade : IApiActionsFacade
    {
        private ITelegramApiProxy _proxy;
        private IStudentRepository _studentRepository;
        private Queue<ScheduleContext> _contexts;
        private IButtonFactoryMethod _buttonFactory;

        public ApiActionsFacade(ITelegramApiProxy proxy ,
                                      IStudentRepository studentRepository,
                                      IButtonFactoryMethod buttonsFactory)
        {
            _studentRepository = studentRepository;
            _proxy = proxy;
            _contexts = new Queue<ScheduleContext>();
            _buttonFactory = buttonsFactory;
        }

        public void Start(Update update)
        {
            var userExists = _studentRepository.UseContext(_contexts.Dequeue()).IsRegistered(update.message.from.id);

            InlineKeyboardMarkup markup = _buttonFactory.CreateStartMenu(userExists);
            var message = new SendMessage(update.message.chat.id.ToString(), "Тыкни на кнопку", markup);
            _proxy.SendMessage(message);
        }

        public void SendInformationAboutBot(Update update)
        {
            _contexts.Dequeue();
            var message = new SendMessage(update.message.chat.id.ToString(), "Бот для расписания в мисосе");
            _proxy.SendMessage(message);
        }

        public void SendError(Update update)
        {
            var message = new SendMessage(update.message.chat.id.ToString(), "команда не найдена");
            _proxy.SendMessage(message);
        }

        public void AddContext(ScheduleContext context)
        {
            _contexts.Enqueue(context);
        }
    }
}