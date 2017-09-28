namespace Domain.TelegramEntities
{
    public class InlineKeyboardButton
    {
        public string text { get; set; }
        public string callback_data { get; set; }
        public string switch_inline_query { get; set; }
        public string url { get; set; }



        public InlineKeyboardButton(string Text, string CallbackData)
        {
            text = Text;
            callback_data = CallbackData;
        }

        public InlineKeyboardButton(string text, string callback_data, string switch_inline_query) : this(text, callback_data)
        {
            this.switch_inline_query = switch_inline_query;
        }

        public InlineKeyboardButton(string Text)
        {
            text = Text;
        }
    }
}
