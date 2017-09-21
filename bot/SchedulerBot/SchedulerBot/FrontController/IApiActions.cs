using Domain;

namespace SchedulerBot.FrontController
{
    public interface IApiActions
    {
        void Start(Update update);
        void SendInformationAboutBot(Update update);
        void SendError(Update update);
    }
}