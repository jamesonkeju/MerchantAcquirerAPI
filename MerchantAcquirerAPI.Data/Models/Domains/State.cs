using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{

    public class State 
    {
        [Key]
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Capital { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string PTSP_Primary { get; set; }

        public string PTSP_Secondary { get; set; }
        public string ostCode { get; set; }

    }

    public class Lga 
    {
        [Key]
        public string sn { get; set; }
        public string StateName { get; set; }
        public string LGA { get; set; }
        public string UpdatedBy { get; set; }
        public string DateUpdated { get; set; }

    }
}
