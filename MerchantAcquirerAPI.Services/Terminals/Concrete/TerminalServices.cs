using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.Terminal.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.Terminal.Concrete
{
     public  class TerminalServices : ITerminal
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<TerminalServices> _logger;
        private ICommonRoute _commonServices;
        public TerminalServices(MerchantAcquirerAPIAppContext context, ILogger<TerminalServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }

 

        public async Task<ApiResult<List<Data.Models.Domains.TerminalModel>>> GetTerminalModels()
        {
            var msg = new ApiResult<List<Data.Models.Domains.TerminalModel>> ();
            try
            {

                var dataList = new List<Data.Models.Domains.TerminalModel>();
                var response =  await _context.TerminalModel.ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;            
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "Terminal Model ");
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
                        dataList.Add(new Data.Models.Domains.TerminalModel
                        {
                            TermCode = item.TermCode.Trim(),
                            TermModel = item.TermModel,
                           
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



        public async Task<ApiResult<List<Data.Models.Domains.TerminalOwner>>> GetTerminalOwners()
        {
            var msg = new ApiResult<List<Data.Models.Domains.TerminalOwner>>();
            try
            {

                var dataList = new List<Data.Models.Domains.TerminalOwner>();
                var response = await _context.TerminalOwner.ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "Terminal Owner ");
                }
                else if (response.Count == 0)
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
                        dataList.Add(new Data.Models.Domains.TerminalOwner
                        {
                            TermCode = item.TermCode.Trim(),
                            TermOwner = item.TermOwner,

                        });
                    }


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
