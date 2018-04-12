using System;
using System.Collections.Generic;
using Domain.TelegramEntities;

namespace Domain
{
    public class RequestLogger
    {
        public RequestLogger()
        {
            Updates = new List<Update>();
            AnswersList = new List<AnswerInlineQuery>();
        }
        public IList<Update> Updates { get; set; }
        public IList<AnswerInlineQuery> AnswersList { get; set; } 
        public dynamic O { get; set; }
    }

    public enum LoggerActions
    {
        get,delete
    }
}
