using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace SchedulerBot.FrontController
{
    public class TelegramFrontController : ITelegramFrontController
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private IApiActionsFacade _actionsFacade;
        private readonly IUpdateReader _reader;

        public TelegramFrontController(IApiActionsFacade actionsFacade , IUpdateReader reader)
        {
            _reader = reader;
            _actionsFacade = actionsFacade;
            _dataToActions = new Dictionary<string, Action<Update>>
            {
                {"/start", _actionsFacade.Start},
                {"/info", _actionsFacade.SendInformationAboutBot},
                {"/join", actionsFacade.JoinToGroup },
                {"/exit", actionsFacade.ExitFromGroup },
                {"/invite" ,update => actionsFacade.InviteGroupMate(update)}
            };
        }

        public void DoAction(Update update)
        {
            Action<Update> action;
            try
            {
                string command = _reader.GetCommand(update);
                action = _dataToActions.SingleOrDefault(pair => pair.Key.Equals(command)).Value;
                action(update);
            }
            catch (Exception e)
            {
                _actionsFacade.SendError(update);
            }
        }
    }
}
