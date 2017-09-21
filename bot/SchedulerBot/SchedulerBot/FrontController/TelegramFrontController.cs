using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using SchedulerBot.FrontController;

namespace SchedulerBot.FrontController
{
    public class TelegramFrontController : ITelegramFrontController
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private ApiActions _actions;

        public TelegramFrontController(ApiActions actions)
        {
            _actions = actions;
            _dataToActions = new Dictionary<string, Action<Update>>();
            _dataToActions.Add("/start" ,  _actions.Start);
            _dataToActions.Add("/info" , _actions.SendInformationAboutBot);
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
                _actions.SendError(update);
            }
        }
    }
}
