namespace Domain.TelegramEntities
{
    public class InlineKeyboardButton
    {
        public string text { get; set; }
        public string url { get; set; }

        public InlineKeyboardButton(string text, string url)
        {
            this.text = text;
            this.url = url;
        }
    }
}
