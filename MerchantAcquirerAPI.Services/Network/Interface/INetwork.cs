using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.Network.Interface
{
    public interface INetwork
    {
        Task<ApiResult<List<Data.Models.Domains.OperatorList>>> GetNetworkOperators();
        Task<ApiResult<Data.Models.Domains.NetworkTypeList>> GetNetworkDetail(string NetworkOperators);

    }
}
