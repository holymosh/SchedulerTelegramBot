namespace Domain
{
    public class Update
    {
        public int update_id { get; set; }
        public Message message { get; set; }
        public Message edited_message { get; set; }
        public InlineQuery inline_query { get; set; }
    }
}
