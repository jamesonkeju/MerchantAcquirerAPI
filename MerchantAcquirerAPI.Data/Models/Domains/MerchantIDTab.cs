using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class MerchantIDTab
    {
        [Key]
        public int sn { get; set; }
        public string LastMid { get; set; }
        public DateTime DateLastGen { get; set; }
    }
}
