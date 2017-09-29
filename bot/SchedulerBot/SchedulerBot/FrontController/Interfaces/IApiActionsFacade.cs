using Domain;
using Infrastructure.InfrastuctureLogic;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IApiActionsFacade
    {
        void Start(Update update);
        void SendInformationAboutBot(Update update);
        void SendError(Update update);
        void AddContext(ScheduleContext context);
        void JoinToGroup(Update update);
        void ExitFromGroup(Update update);
        void InviteGroupmate(Update update);
        void SendAnswerForInlineQuery(Update update);
        void GetTomorrowLessons(Update update);
    }
}