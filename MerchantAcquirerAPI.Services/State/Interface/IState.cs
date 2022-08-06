using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.State.Interface
{
    public interface IState
    {
        Task<ApiResult<List<Data.Models.Domains.State>>> GetState();
        Task<ApiResult<List<Data.Models.Domains.Lga>>> GetLGAByState(string StateCode);
    }
}
