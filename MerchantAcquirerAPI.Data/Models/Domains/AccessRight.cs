using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class AccessRight
    {
        [Key]
        public int AccessID { get; set; }
        public string AccessName { get; set; }
        public string AccessDescription { get; set; }
    
    }
}
