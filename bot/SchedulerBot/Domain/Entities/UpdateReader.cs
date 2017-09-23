using Domain.Interfaces;

namespace Domain.Entities
{
    public class UpdateReader : IUpdateReader
    {
        public string GetUserId(Update update)
        {
            return update.message is null ? update.callback_query.from.id : update.message.from.id;
        }

        public string GetCommand(Update update)
        {
            return update.message is null ? update.callback_query.data : update.message.text;
        }
    }
}
