using System;
using System.Collections.Generic;

namespace Domain
{
    public class RequestLogger
    {
        public Object lastJson { get; set; }
        private IList<Object> _objects = new List<object>();

        
    }

    public enum Options
    {
        last,all,index
    }
}
