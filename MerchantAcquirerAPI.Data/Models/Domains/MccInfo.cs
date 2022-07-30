using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public  class MccInfo
    {
       [Key]
        public string MccCode { get; set; }
        public string MccName { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string updatedby { get; set; }

    }
}
