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
        Task<ApiResult<POSRequestResponse>> CreatePOSRequest(POSRequest payload, string AppFileName, string AcceptanceFileName);
        Task<ApiResult<string>> GetNewMID(string AccountNo, string address);
        Task<ApiResult<string>> getMerchantName(string MerchantID);
        Task writelog(string userid, string op);
        Task<string> sendText(string mm, string mcid,string fileName);
    }
}
