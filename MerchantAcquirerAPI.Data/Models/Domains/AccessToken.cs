using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
  public  class AccessToken
    {
        [Key]
        public long Id { get; set; }
        public string TokenKey { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
