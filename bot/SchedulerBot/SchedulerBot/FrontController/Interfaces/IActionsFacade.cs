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
        void JoinToGroup(Update update);
        void ExitFromGroup(Update update);
        void InviteGroupmate(Update update);
        void SendAnswerForInlineQuery(Update update);
        void GetTomorrowLessons(Update update);
        void GetNextLessons(Update update);
        void GetCurrentTeacherData(Update update);
        void BackToTheMenu(Update update);
        void SendWeekButtons(Update update);
    }
}