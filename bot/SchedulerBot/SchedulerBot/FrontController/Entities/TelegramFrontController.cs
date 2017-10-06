using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.FrontController.Entities
{
    public class TelegramFrontController : ITelegramFrontController
    {
        private IDictionary<string, Action<Update>> _dataToActions;
        private readonly IActionsFacade _actionsFacade;
        private readonly IUpdateReader _updateReader;

        public TelegramFrontController(IActionsFacade actionsFacade , IUpdateReader updateReader)
        {
            _updateReader = updateReader;
            _actionsFacade = actionsFacade;
            _dataToActions = new Dictionary<string, Action<Update>>
            {
                {"/menu",_actionsFacade.StartDialog},
                {"/info",_actionsFacade.SendInformationAboutBot},
                {"/start",_actionsFacade.HandleInitialMessage},
                {"/exit",_actionsFacade.ExitFromGroup},
                {"/invite",_actionsFacade.InviteGroupmate},
                {"/tomorrow",_actionsFacade.SendTomorrowLessons},
                {"/next",_actionsFacade.SendNextLessons},
                {"/name",_actionsFacade.SendCurrentTeacherData},
                {"/back",_actionsFacade.BackToTheMenu},
                {"/week",_actionsFacade.SendWeekButtons},
                {"/monday" , actionsFacade.SendLessonsAtCustomDay},
                {"/tuesday" , actionsFacade.SendLessonsAtCustomDay},
                {"/wednesday",actionsFacade.SendLessonsAtCustomDay},
                {"/thursday",actionsFacade.SendLessonsAtCustomDay},
                {"/friday",actionsFacade.SendLessonsAtCustomDay },
                {"/saturday",actionsFacade.SendLessonsAtCustomDay},
                //{"/message",_actionsFacade.SendMessageToGroupmates }
                {"/create", actionsFacade.CreateGroup }

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
                //_actionsFacade.SendError(update);
                    if (update.edited_message is null)
                    {
                        _actionsFacade.SendMessageToGroupmates(update);
                    }
            }
        }
    }
}
