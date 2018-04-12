namespace Domain.TelegramEntities
{
    public class InlineQueryResultArticle
    {
        public string type = "article";
        public string id { get; set; }
        public string title {get; set; }
        public InputTextMessageContent input_message_content { get; set; }
        public InlineKeyboardMarkup reply_markup { get; set; }
    }
}
