using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class KYCProperties
    {// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public string returnedCode { get; set; }
            public bool isSuccessful { get; set; }
            public string message { get; set; }
            public List<string> returnedObject { get; set; }
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
