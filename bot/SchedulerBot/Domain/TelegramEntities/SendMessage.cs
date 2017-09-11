namespace Domain.TelegramEntities
{
    public class SendMessage
    {
        public string chat_id { get; set; }
        public string text { get; set; }
        public InlineKeyboardMarkup reply_markup { get; set; }


        public SendMessage(string Chat_id, string Text)
        {
            chat_id = Chat_id;
            text = Text;
        }

        public SendMessage(string chat_id, string text, InlineKeyboardMarkup reply_markup) : this(chat_id, text)
        {
            this.reply_markup = reply_markup;
        }
    }
}