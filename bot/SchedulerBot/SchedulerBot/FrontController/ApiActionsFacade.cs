﻿using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IGroupRepository _groupRepository;

        public ApiActionsFacade(ITelegramApiProxy proxy ,
                                IStudentRepository studentRepository,
                                IButtonFactoryMethod buttonsFactory,
                                IUpdateReader reader,
                                IGroupRepository groupRepository
            )
        {
            _studentRepository = studentRepository;
            _proxy = proxy;
            _contexts = new Queue<ScheduleContext>();
            _buttonFactory = buttonsFactory;
            _updateReader = reader;
            _groupRepository = groupRepository;
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

        public void JoinToGroup(Update update)
        {
            throw new System.NotImplementedException();
        }

        public void ExitFromGroup(Update update)
        {
            _studentRepository.UseContext(_contexts.Dequeue()).RemoveStudent(_updateReader.GetUserId(update));
            var markup = _buttonFactory.CreateStartMenu(IsRegistered: false);
            var message = new SendMessage(_updateReader.GetUserId(update), "Тыкни на кнопку", markup);
            _proxy.SendMessage(message);
        }

        public async Task InviteGroupMate(Update update)
        {
            var group = _groupRepository.UseContext(_contexts.Dequeue()).GetGroupByStudent(_updateReader.GetUserId(update));
            var markup = _buttonFactory.CreateInviteButton();
            var firstMessage = new SendMessage(_updateReader.GetUserId(update),group.Name,markup);
            var secondMessage = new SendMessage(_updateReader.GetUserId(update),"отправь сообщение с приглашением своему одногруппнику");
            await Task.Run(() => _proxy.SendMessage(firstMessage));
            _proxy.SendMessage(secondMessage);
        }
    }
}