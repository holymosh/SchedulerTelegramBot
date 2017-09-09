namespace Domain.TelegramEntities
{
    public class Message
    {
        public int message_id { get; set; }
        public User from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public User forward_from { get; set; }
        public Chat forward_from_chat { get; set; }
        public int edit_date { get; set; }
        public string text { get; set; }

    }
}