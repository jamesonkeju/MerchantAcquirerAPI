using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class Country : BaseObject
    {
        public string CountryCode { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<State> State { get; set; }

    }
}
