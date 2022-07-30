using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Payload
{
    public class CustomResponse
    {     
      public  string message { get; set; }
        public string code { get; set; }

        public string data { get; set; }

        public string userId { get; set; }
    }
}
