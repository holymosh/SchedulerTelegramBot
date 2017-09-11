using Domain;
using Domain.TelegramEntities;

namespace SchedulerBot.Facade
{
    public class ApiActions
    {
        private TelegramApiProxy _proxy;

        public ApiActions(TelegramApiProxy proxy)
        {
            _proxy = proxy;
        }

        public void Start(Update update)
        {
        }

        public void SendInformationAboutBot(Update update)
        {
            var message = new SendMessage(update.message.chat.id.ToString() , "Бот для расписания в мисосе");
            _proxy.SendMessage(message);
        }

        public void SendError(Update update)
        {
            var message = new SendMessage(update.message.chat.id.ToString(), "команда не найдена или /start пока что не работает");
            _proxy.SendMessage(message);
        }
    }
}