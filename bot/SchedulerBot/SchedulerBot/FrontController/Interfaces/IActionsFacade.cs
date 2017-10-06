using Domain;
using Infrastructure.InfrastuctureLogic;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IActionsFacade
    {
        void StartDialog(Update update);
        void SendInformationAboutBot(Update update);
        void SendError(Update update);
        void AddContext(ScheduleContext context);
        void HandleInitialMessage(Update update);
        void ExitFromGroup(Update update);
        void InviteGroupmate(Update update);
        void SendAnswerForInlineQuery(Update update);
        void SendTomorrowLessons(Update update);
        void SendNextLessons(Update update);
        void SendCurrentTeacherData(Update update);
        void BackToTheMenu(Update update);
        void SendWeekButtons(Update update);
        void SendLessonsAtCustomDay(Update update);
        void SendMessageToGroupmates(Update update);
        void CreateGroup(Update update);
    }
}