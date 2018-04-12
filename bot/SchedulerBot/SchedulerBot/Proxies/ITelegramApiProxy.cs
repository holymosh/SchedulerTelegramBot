using System.Net.Http;
using Domain.TelegramEntities;

namespace SchedulerBot.Proxies
{
    public interface ITelegramApiProxy
    {
        HttpResponseMessage SendMessage(SendMessage message);
        HttpResponseMessage SendInlineAnswer(AnswerInlineQuery answerInlineQuery);
        HttpResponseMessage EditMessage(EditMessageText text);
    }
}