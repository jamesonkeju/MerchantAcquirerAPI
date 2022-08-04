using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.Terminal.Interface
{
    public interface ITerminal
    {
        Task<ApiResult<List<Data.Models.Domains.TerminalModel>>> GetTerminalModels();

        Task<ApiResult<List<Data.Models.Domains.TerminalOwner>>> GetTerminalOwners();
    }
}
