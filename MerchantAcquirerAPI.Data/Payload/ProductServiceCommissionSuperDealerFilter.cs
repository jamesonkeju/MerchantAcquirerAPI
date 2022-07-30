﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Payload
{
   public class ProductServiceCommissionSuperDealerFilter
    {

      
        public int ProductServiceId { get; set; }
       
        public decimal ServiceCommsion { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
       
        public int pageNumber { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckDeleted { get; set; } = true;
        public int pageSize { get; set; } = 10;
       
    }
}
