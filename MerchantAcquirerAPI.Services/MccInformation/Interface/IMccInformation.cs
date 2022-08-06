using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.MccInformation.Interface
{
    public interface  IMccInformation
    {
        Task<ApiResult<List<Data.Models.Domains.MccInfo>>> GetMccInformationList();
    }
}
