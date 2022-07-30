using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
   public class BusinessOccupation
    {
        [Key]
        public string BusCatName { get; set; }
        public string BusOcpCode { get; set; }
        public string BusOcpName { get; set; }

    }
}
