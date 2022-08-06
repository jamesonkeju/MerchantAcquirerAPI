using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Data.Models.ViewModel;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.BusinessCategory.Interface;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.State.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.State.Concrete
{
     public  class StateServices : IState
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<StateServices> _logger;
        private ICommonRoute _commonServices;
        public StateServices(MerchantAcquirerAPIAppContext context, ILogger<StateServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }



        public async Task<ApiResult<List<Data.Models.Domains.State>>> GetState()
        {
            var msg = new ApiResult<List<Data.Models.Domains.State>> ();
            try
            {

                var dataList = new List<Data.Models.Domains.State>();
                var response =  await _context.State.ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;            
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "State ");
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
                        dataList.Add(new Data.Models.Domains.State
                        {
                            StateName = item.StateName.Trim(),
                            StateCode = item.StateCode.Trim(),
                            Capital= item.Capital.Trim(),
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

        public async Task<ApiResult<List<Lga>>> GetLGAByState(string StateCode)
        {
            var msg = new ApiResult<List<Lga>>();
            try
            {
                var dataList = new List<Data.Models.Domains.Lga>();
                var getState = await _context.State.Where(a => a.StateCode.ToLower() == StateCode.ToLower()).FirstOrDefaultAsync();

                if (getState == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", "State ");

                }

                var response = await _context.LGA.Where(a => a.StateName == getState.StateName).ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }
                else if (response.Count == 0)
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
                        dataList.Add(new Data.Models.Domains.Lga
                        {
                            StateName = item.StateName.Trim(),
                            LGA = item.LGA,
                            sn = item.sn,
                            DateUpdated= item.DateUpdated,
                            UpdatedBy= item.UpdatedBy
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
