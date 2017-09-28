using System.Collections.Generic;

namespace Domain.TelegramEntities
{
    public class AnswerInlineQuery
    {
        public string inline_query_id { get; set; }
        public IList<InlineQueryResultArticle> results { get; set; }

    }
}
