﻿using System.Collections.Generic;
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
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Группы", "/groups"),
            };
            IList<InlineKeyboardButton> currentButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Какая сейчас пара?", "/current"),
            };
            IList<InlineKeyboardButton> nameButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Как зовут препода, у которого сейчас пара?", "/name"),
            };
            IList<InlineKeyboardButton> nextButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Какие пары остались?", "/next"),
            };
            IList<InlineKeyboardButton> weekButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Расписание на эту неделю", "/week"),
            };
            IList<InlineKeyboardButton> downloadButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("залить расписание", "/download"),
            };
            IList<InlineKeyboardButton> messageButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("сообщение для группы", "/message"),
            };
            IList<InlineKeyboardButton> fileButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("инструкция по заполнению и файл с форматом расписания", "/file")
            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>()
            {
                groupButton,
                currentButton,
                nameButton,
                nextButton,
                weekButton,
                downloadButton,
                messageButton,
                fileButton
            };
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(listOfListOfButtons);
            var message = new SendMessage(update.message.chat.id.ToString(), "еболда", markup);
            _proxy.SendMessage(message);
        }

        public void SendInformationAboutBot(Update update)
        {
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