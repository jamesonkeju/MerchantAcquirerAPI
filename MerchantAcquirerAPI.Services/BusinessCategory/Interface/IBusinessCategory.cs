using MerchantAcquirerAPI.Data.Models.ViewModel;
using MerchantAcquirerAPI.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.BusinessCategory.Interface
{
    public interface  IBusinessCategory
    {
        Task<ApiResult<List<BusinessCategoryList>>> GetBusinessCategory();
            Task<ApiResult<List<Data.Models.Domains.BusinessOccupation>>> BusinessOccupations(string BusinessCategoryName);
    }
}
