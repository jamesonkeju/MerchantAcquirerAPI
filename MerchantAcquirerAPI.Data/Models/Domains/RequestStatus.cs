using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class RequestStatus
    {
        [Key]
        public int ReqStatId { get; set; }
        public string  ReqStatus { get; set; }
    }
}
