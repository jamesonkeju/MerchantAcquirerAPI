using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.AccountType.Concrete
{
    
    public  class AccountTypeServices : IAccountType
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<AccountTypeServices> _logger;
        private ICommonRoute _commonServices;
        public AccountTypeServices(MerchantAcquirerAPIAppContext context, ILogger<AccountTypeServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }

 

        public async Task<ApiResult<List<Data.Models.Domains.AcctType>>> GetAccountTypes()
        {
            var msg = new ApiResult<List<Data.Models.Domains.AcctType>> ();
            try
            {

                var dataList = new List<Data.Models.Domains.AcctType>();
                var response =  await _context.AcctType.ToListAsync();

                if (response == null)
                {
                    msg.HasError = true;            
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
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

                    foreach(var item in response)
                    {
                        dataList.Add(new Data.Models.Domains.AcctType
                        {
                            Acctcode = item.Acctcode.Trim(),
                            AcctDesc = item.AcctDesc
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
