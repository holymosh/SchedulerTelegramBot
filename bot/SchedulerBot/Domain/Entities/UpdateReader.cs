using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.TelegramEntities;

namespace Domain.Entities
{
    public class UpdateReader : IUpdateReader
    {
        private readonly IList<string> _commands;

        public UpdateReader()
        {
            _commands = new List<string>
            {
                "/start",
                "/info",
                "/start ",
                "/exit",
                "/invite",
                "/tomorrow",
                "/next",
                "/name"
                
            };
        }

        public string GetUserId(Update update) => 
            update.message is null ? 
            update.callback_query.from.id : update.message.from.id;

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
            var argument = actionData.Remove(0, command.Length);
            return argument;
        }
    }
}
