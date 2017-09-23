using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace SchedulerBot.FrontController
{
    public class TelegramFrontController : ITelegramFrontController
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private IApiActionsFacade _actionsFacade;

        public TelegramFrontController(IApiActionsFacade actionsFacade)
        {
            _actionsFacade = actionsFacade;
            _dataToActions = new Dictionary<string, Action<Update>>
            {
                {"/start", _actionsFacade.Start},
                {"/info", _actionsFacade.SendInformationAboutBot}
            };
        }

        public void DoAction(Update update)
        {
            Action<Update> action;
            try
            {
                action = _dataToActions.SingleOrDefault(pair => pair.Key.Equals(update.message.text)).Value;
                action(update);
            }
            catch (Exception e)
            {
                _actionsFacade.SendError(update);
            }
        }
    }
}
