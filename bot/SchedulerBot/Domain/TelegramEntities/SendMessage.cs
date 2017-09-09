namespace Domain.TelegramEntities
{
    public class SendMessage
    {
        public string chat_id { get; set; }
        public string text { get; set; }

        public SendMessage(string Chat_id, string Text)
        {
            chat_id = Chat_id;
            text = Text;
        }
    }
}
