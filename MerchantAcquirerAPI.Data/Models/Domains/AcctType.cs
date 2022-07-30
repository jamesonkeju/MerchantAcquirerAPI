using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class AcctType
    {
        [Key]
        public string Acctcode { get; set; }
        public string AcctDesc { get; set; }
    }
}
