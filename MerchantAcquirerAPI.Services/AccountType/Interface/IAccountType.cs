using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.AccountType.Interface
{
    public interface IAccountType
    {
        Task<ApiResult<List<Data.Models.Domains.AcctType>>> GetAccountTypes();
    }
}
