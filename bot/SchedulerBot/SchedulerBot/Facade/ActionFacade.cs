using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace SchedulerBot.Facade
{
    public class ActionFacade
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private ApiActions _actions;

        public ActionFacade(ApiActions actions)
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
