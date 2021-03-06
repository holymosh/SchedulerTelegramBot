﻿using Domain.TelegramEntities;

namespace Domain
{
    public class CallbackQuery
    {
        public string id { get; set; }
        public User from { get; set; }
        public Message message { get; set; }
        public string inline_message_id { get; set; }
        public string chat_instance { get; set; }
        public string data { get; set; }
    }
}