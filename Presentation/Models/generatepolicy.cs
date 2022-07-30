using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class generatepolicy
    {
        public class Result
        {
            public string isSuccessful { get; set; }
            public string message { get; set; }
            public string trackingNumber { get; set; }
        }

        public class Root
        {
            public bool hasError { get; set; }
            public string message { get; set; }
            public bool isSuccessful { get; set; }
            public Result result { get; set; }
        }
    }
}
