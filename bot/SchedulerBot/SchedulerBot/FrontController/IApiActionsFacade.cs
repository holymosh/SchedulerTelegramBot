using Domain;
using Infrastructure.InfrastuctureLogic;

namespace SchedulerBot.FrontController
{
    public interface IApiActionsFacade
    {
        void Start(Update update);
        void SendInformationAboutBot(Update update);
        void SendError(Update update);
        void AddContext(ScheduleContext context);
    }
}