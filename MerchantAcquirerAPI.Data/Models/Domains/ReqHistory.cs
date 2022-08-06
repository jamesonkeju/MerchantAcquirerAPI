using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
   public  class ReqHistory
    {
        [Key]
        public int sn { get; set; }
        public string ReqId { get; set; }
        public string Initiator { get; set; }
        public string ActionPerformed { get; set; }
        public DateTime? ActionDateTime { get; set; }

    }
}
