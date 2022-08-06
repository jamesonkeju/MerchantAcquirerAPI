using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Data.Models.ViewModel;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.BusinessCategory.Interface;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.BusinessCategory.Concrete
{
     public  class BusinessOccupationsServices : IBusinessCategory
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<BusinessOccupationsServices> _logger;
        private ICommonRoute _commonServices;
        public BusinessOccupationsServices(MerchantAcquirerAPIAppContext context, ILogger<BusinessOccupationsServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }

      

        public async Task<ApiResult<List<BusinessCategoryList>>> GetBusinessCategory()
        {
            var msg = new ApiResult<List<BusinessCategoryList>> ();
            try
            {

                var dataList = new List<BusinessCategoryList>();
                var response =  await _context.BusinessCategory.Select(m => m.BusCatName).Distinct().ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;            
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "Business Category");
                }
                else if(response.Count==0)
                {
                    msg.HasError = false;             
                    msg.Message = CommonResponseMessage.MobileMessageNoRecord;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Result = null;
                }
                else
                {

                    foreach (var item in response)
                    {
                        dataList.Add(new BusinessCategoryList
                        {
                            BusinessCatgoryName = item.Trim(),
                         
                        });
                    }

                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = dataList.ToList();
                }

   
                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }

        public async Task<ApiResult<List<BusinessOccupation>>> BusinessOccupations(string BusinessCategoryName)
        {
            var msg = new ApiResult<List<BusinessOccupation>>();
            try
            {

                

                var response = await _context.BusinessOccupation.Where(a => a.BusCatName.ToLower() == BusinessCategoryName.ToLower()).ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "Business Occupation");
                }
                else if (response.Count == 0)
                {
                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.MobileMessageNoRecord;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Result = response;
                }
                else
                {

                 

                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = response.ToList();
                }


                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }
    }
}
