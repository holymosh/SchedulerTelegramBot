namespace Domain.TelegramEntities
{
    public class EditMessageText
    {
        public string chat_id { get; set; }
        public string message_id { get; set; }
        public string text { get; set; }
        public InlineKeyboardMarkup reply_markup { get; set; }

        public EditMessageText(string chat_id, string message_id, string text, InlineKeyboardMarkup reply_markup)
        {
            this.chat_id = chat_id;
            this.message_id = message_id;
            this.text = text;
            this.reply_markup = reply_markup;
        }
    }
}
