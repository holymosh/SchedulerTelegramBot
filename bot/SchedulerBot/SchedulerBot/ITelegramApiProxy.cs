using System.Net.Http;
using Domain.TelegramEntities;

namespace SchedulerBot
{
    public interface ITelegramApiProxy
    {
        HttpResponseMessage SendMessage(SendMessage message);
    }
}