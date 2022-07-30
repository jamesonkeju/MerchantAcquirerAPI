using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class policydetails
    {// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public string phoneNumber { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailAddress { get; set; }
            public DateTime policyStartDate { get; set; }
            public DateTime policyEndDate { get; set; }
            public string localGovernment { get; set; }
            public string address { get; set; }
            public string code { get; set; }
            public bool isSuccessful { get; set; }
            public string message { get; set; }
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
