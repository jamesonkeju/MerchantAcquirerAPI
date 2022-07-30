using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models.Domains;

namespace MerchantAcquirerAPI.Data.Models
{
    public class ApplicationUserPasswordHistory : BaseObject
    {
        public string UserId { get; set; }
        public string HashPassword { get; set; }

        public string PasswordSalt { get; set; }
    }
}
