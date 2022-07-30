using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class Branch
    {
        [Key]
        public int Branchid { get; set; }
        public string BranchName { get; set; }
        public int Rname { get; set; }
    
    }
}
