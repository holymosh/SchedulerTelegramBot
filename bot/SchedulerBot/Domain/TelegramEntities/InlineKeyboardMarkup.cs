using System.Collections.Generic;

namespace Domain.TelegramEntities
{
    public class InlineKeyboardMarkup
    {
        public IList<IList<InlineKeyboardButton>> inline_keyboard { get; set; }

        public InlineKeyboardMarkup(IList<IList<InlineKeyboardButton>> inline_keyboard)
        {
            this.inline_keyboard = inline_keyboard;
        }
    }
}
