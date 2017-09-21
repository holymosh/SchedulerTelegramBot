using Domain;

namespace SchedulerBot.FrontController
{
    public interface ITelegramFrontController
    {
        void DoAction(Update update);
    }
}