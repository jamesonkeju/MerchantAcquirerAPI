using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class TerminalOwner
    {
        [Key]
        public string TermOwner { get; set; }
        public string TermCode { get; set; }
    }
}
