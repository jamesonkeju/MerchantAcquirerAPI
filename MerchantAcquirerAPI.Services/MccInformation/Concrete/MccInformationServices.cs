using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.MccInformation.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.MccInformation.Concrete
{
     public  class MccInformationServices : IMccInformation
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<MccInformationServices> _logger;
        private ICommonRoute _commonServices;
        public MccInformationServices(MerchantAcquirerAPIAppContext context, ILogger<MccInformationServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }

 

        public async Task<ApiResult<List<Data.Models.Domains.MccInfo>>> GetMccInformationList()
        {
            var msg = new ApiResult<List<Data.Models.Domains.MccInfo>> ();
            try
            {

                var dataList = new List<Data.Models.Domains.MccInfo>();
                var response =  await _context.MccInfo.ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;            
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "MCC Information");
                }
                else if(response.Count==0)
                {
                    msg.HasError = false;             
                    msg.Message = CommonResponseMessage.MobileMessageNoRecord;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Result = response.ToList();
                }
                else
                {
                    foreach (var item in response)
                    {
                        dataList.Add(new Data.Models.Domains.MccInfo
                        {
                            MccCode = item.MccCode.Trim(),
                            MccName = item.MccName
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

       
    }
}
