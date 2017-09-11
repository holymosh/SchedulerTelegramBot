namespace Domain.TelegramEntities
{
    public class InlineKeyboardButton
    {
        public string text { get; set; }
        public string callback_data { get; set; }

        public InlineKeyboardButton(string Text, string CallbackData)
        {
            text = Text;
            callback_data = CallbackData;
        }
    }
}
