using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
   public class BusinessCategory
   {
        [Key]
        public int BusCatCode { get; set; }
        public string BusCatName { get; set; }

    }
}
