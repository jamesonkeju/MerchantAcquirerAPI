using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class NewAuditLog
    {
       [Key]
        public string TransRef { get; set; }
        public string CheckerIPAddress { get; set; }
     
        public DateTime? CheckerDate { get; set; }

        public string CheckerName { get; set; }

        public string CheckerID { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }
     
        public string Activity { get; set; }

        public string MakerIPAddress { get; set; }

        public DateTime? MakerDate { get; set; }

        public string MakerName { get; set; }

        public string MakerID { get; set; }
     
        public string TransDesc { get; set; }
       
        
    }
}
