﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace SchedulerBot.FrontController
{
    public class TelegramFrontController : ITelegramFrontController
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private readonly IApiActionsFacade _actionsFacade;
        private readonly IUpdateReader _updateReader;

        public TelegramFrontController(IApiActionsFacade actionsFacade , IUpdateReader updateReader)
        {
            _updateReader = updateReader;
            _actionsFacade = actionsFacade;
            _dataToActions = new Dictionary<string, Action<Update>>
            {
                {"/start", _actionsFacade.Start},
                {"/info", _actionsFacade.SendInformationAboutBot},
                {"/join", _actionsFacade.JoinToGroup },
                {"/exit", _actionsFacade.ExitFromGroup },
                {"/invite" ,_actionsFacade.InviteGroupmate}
            };
        }

        public void DoAction(Update update)
        {
            Action<Update> action;
            try
            {
                if (!_updateReader.IsInlineQuery(update))
                {
                    _actionsFacade.SendAnswerForInlineQuery(update);    
                }
                else
                {
                    string command = _updateReader.GetCommand(update);
                    action = _dataToActions.SingleOrDefault(pair => pair.Key.Equals(command)).Value;
                    action(update);
                }
            }
            catch (Exception e)
            {
                _actionsFacade.SendError(update);
            }
        }
    }
}
