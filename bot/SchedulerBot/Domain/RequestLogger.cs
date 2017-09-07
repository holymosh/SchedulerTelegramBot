using System;
using System.Collections.Generic;

namespace Domain
{
    public class RequestLogger
    {
        public RequestLogger()
        {
            Data = new List<Update>();
        }

        public IList<Update> Data { get; set; }
        public Object lastJson { get; set; }
    }
}
