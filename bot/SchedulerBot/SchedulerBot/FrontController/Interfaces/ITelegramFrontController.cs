using Domain;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface ITelegramFrontController
    {
        void DoAction(Update update);
    }
}