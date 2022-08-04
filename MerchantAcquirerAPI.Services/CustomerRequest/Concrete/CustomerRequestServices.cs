using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.CustomerRequest.dto;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Services.Terminal.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.CustomerRequest.Concrete
{
    public class CustomerRequestServices : ICustomerRequest
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<CustomerRequestServices> _logger;
        private ICommonRoute _commonServices;
        public CustomerRequestServices(MerchantAcquirerAPIAppContext context, ILogger<CustomerRequestServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }



        public async Task<ApiResult<CustomerRequestReponse>> GetCustomerRequestStatus(string AccountNo)
        {
            var msg = new ApiResult<CustomerRequestReponse>();
            try
            {

                var dataList = new CustomerRequestReponse();

                var getData =  (from k in  _context.PosReq
                               join a in _context.AcctType on k.AcctType equals a.Acctcode
                               join s in _context.State on k.StateCode equals s.StateCode
                               join r in _context.Branch on k.ReqBranch equals r.Branchid.ToString()
                               join p in _context.RequestStatus on k.ProfilingStatus equals p.ReqStatId

                               where k.AcctNo == AccountNo
                               select new CustomerRequestReponse
                               {
                                   AccountClass = k.AccountClass,
                                   AcctNo= k.AcctNo,
                                   AcctType =a.AcctDesc,
                                   AcctName = k.AcctName,
                                   ContactName= k.ContactName,
                                   ContactTitle= k.ContactTitle,
                                   MerchantName= k.MerchantName,
                                   MobilePhone= k.MobilePhone,
                                   Comment = k.Comment,
                                   profillingDate = k.profillingDate,
                                   MerchantID= k.MerchantID,
                                   CustID= k.CustID,
                                   Status = p.ReqStatus,
                                   ReqDate= k.ReqDate,

                               }).FirstOrDefault();




                if (getData == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }
                else
                {


                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = getData;
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
