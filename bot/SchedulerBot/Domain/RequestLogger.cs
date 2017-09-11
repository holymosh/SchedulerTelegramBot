using System.Collections.Generic;

namespace Domain
{
    public class RequestLogger
    {
        public RequestLogger()
        {
            Updates = new List<Update>();
        }
        public IList<Update> Updates { get; set; }
    }

    public enum LoggerActions
    {
        get,delete
    }
}
