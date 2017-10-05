using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class UpdateReader : IUpdateReader
    {
        private readonly IList<string> _commands;

        public UpdateReader()
        {
            _commands = new List<string>
            {
                "/menu",
                "/info",
                "/start",
                "/exit",
                "/invite",
                "/tomorrow",
                "/next",
                "/name",
                "/back",
                "/week"
            };
        }

        public string GetUserId(Update update) =>
            update.message is null ? update.callback_query.from.id : update.message.from.id;

        public string GetActionData(Update update)
        {
            return update.message is null ? update.callback_query.data : update.message.text;
        }

        public bool IsInlineQuery(Update update) =>
            update.inline_query is null;

        public string GetCommand(Update update)
        {
            var actionData = GetActionData(update);
            var command = _commands.Last(entity => actionData.Contains(entity));
            return command;
        }

        public string GetArgument(Update update)
        {
            var actionData = GetActionData(update);
            var command = _commands.Last(entity => actionData.Contains(entity));
            var argument = actionData.Remove(0, command.Length + 1);
            return argument;
        }

        public int GetMessageId(Update update) => 
            update.message is null ? update.callback_query.message.message_id : update.message.message_id;
    }
}