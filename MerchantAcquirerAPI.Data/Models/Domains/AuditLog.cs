using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class AuditLog
    {
        [Key]
        public int id { get; set; }
        public string UserId { get; set; }
        public string OperationsPerformed { get; set; }
        public DateTime DateAccessed { get; set; }
        public string PageVisited { get; set; }
        public string IpAddress { get; set; }
    }
}
