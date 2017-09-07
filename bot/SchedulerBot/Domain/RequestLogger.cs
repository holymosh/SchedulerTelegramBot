using System.Collections.Generic;

namespace Domain
{
    public class RequestLogger
    {
        public RequestLogger()
        {
            Data = new List<string>();
        }

        public IList<string> Data { get; set; }
    }
}
