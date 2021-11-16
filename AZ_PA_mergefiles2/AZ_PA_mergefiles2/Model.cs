using System;
using System.Collections.Generic;
using System.Text;

namespace AZ_PA_mergefiles2
{
    public class Model
    {
        public class RequestBodyModel
        {
           
            public List<content> file { get; set; }
        }
        public class content
        {
            public string Content_type { get; set; }
            public string Content { get; set; }
        }

        public class ResponseBodyModel
        {
            public string Content { get; set; }
        }
    }
}
