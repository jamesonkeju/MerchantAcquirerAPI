using MerchantAcquirerAPI.Services.CustomerRequest.dto;
using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.CustomerRequest.Interface
{
    public interface ICustomerRequest
    {
        Task<ApiResult<CustomerRequestReponse>> GetCustomerRequestStatus(string AccountNo);


      
    }
}
