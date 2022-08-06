using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.Network.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.Network.Concrete
{
    public class NetworkServices : INetwork
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<NetworkServices> _logger;
        private ICommonRoute _commonServices;
        public NetworkServices(MerchantAcquirerAPIAppContext context, ILogger<NetworkServices> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }



        public async Task<ApiResult<List<Data.Models.Domains.OperatorList>>> GetNetworkOperators()
        {
            var msg = new ApiResult<List<Data.Models.Domains.OperatorList>>();
            try
            {

                var dataList = new List<Data.Models.Domains.OperatorList>();
                var response = await _context.NetworkTab.ToListAsync();

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
                        dataList.Add(new Data.Models.Domains.OperatorList
                        {
                            NetworkOperator = item.NetworkOperator.Trim(),

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


        public async Task<ApiResult<Data.Models.Domains.NetworkTypeList>> GetNetworkDetail(string OperatorName)
        {
            var msg = new ApiResult<Data.Models.Domains.NetworkTypeList>();
            try
            {

                var dataList = new Data.Models.Domains.NetworkTypeList();

                var response = await _context.NetworkTab.Where(a => a.NetworkOperator.ToLower() == OperatorName.ToLower()).FirstOrDefaultAsync();

                if (response == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }

                else
                {
                    dataList.NetworkType = response.NetworkType;


                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = dataList;
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
