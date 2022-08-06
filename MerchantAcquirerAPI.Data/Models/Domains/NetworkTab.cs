using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
     public  class NetworkTab
    {
        [Key]
        public int Id { get; set; }
        public string NetworkOperator { get; set; }
        public string NetworkType { get; set; }
      


    }

    public class OperatorList
    {
 
        public string NetworkOperator { get; set; }

    }

    public class NetworkTypeList
    {
        public string NetworkType { get; set; }

    }
}
