using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class productobject
    {// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Benefit
        {
            public int malaria { get; set; }
            public int life { get; set; }
            public int property { get; set; }
            public int propertyCrop { get; set; }
            public int hospitalization { get; set; }
            public int permanentDisability { get; set; }
            public int airTravel { get; set; }
            public int roadTravel { get; set; }
        }

        public class Result
        {
            public string returnedCode { get; set; }
            public bool isSuccessful { get; set; }
            public string message { get; set; }
            public List<ReturnedObject> returnedObject { get; set; }
        }

        public class ReturnedObject
        {
            public string productCode { get; set; }
            public double price { get; set; }
            public int rate { get; set; }
            public string description { get; set; }
            public Benefit benefit { get; set; }
            public string duration { get; set; }
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
