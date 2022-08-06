using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class NewTerminalTemp
    {
        [Key]
        public long SN { get; set; }
        public string TerminalNumber { get; set; }

        public DateTime LastGeneratedDate { get; set; }
    }
}
