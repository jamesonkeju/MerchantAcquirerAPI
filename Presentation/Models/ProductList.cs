using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class ProductList
    {
        public string ProductCode { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
