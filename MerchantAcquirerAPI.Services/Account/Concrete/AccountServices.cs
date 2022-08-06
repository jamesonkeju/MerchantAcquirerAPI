using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models;
using MerchantAcquirerAPI.Data.Payload;
using MerchantAcquirerAPI.Services.Account.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.DataAccess;
using MerchantAcquirerAPI.Utilities.Common;
using MerchantAcquirerAPI.Data.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using MerchantAcquirerAPI.Services.Account.DTO;
using MerchantAcquirerAPI.Data.Models.Domains;

namespace MerchantAcquirerAPI.Services.Account.Concrete
{
    public class AccountServices :IAccount
    {
        private readonly MerchantAcquirerAPIAppContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        protected readonly RoleManager<ApplicationRole> _roleManager;
      
        private readonly IActivityLog _activityRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        
        public AccountServices(IConfiguration configuration,
         
            MerchantAcquirerAPIAppContext context)
        {


            _configuration = configuration;
            _context = context;
       
        }


        public async Task<ApiResult<Utilities.LDAPModel.CustomerDetail>> AccountValidation(string? AccountNo)
        {
            var customerInfo = new Utilities.LDAPModel.CustomerDetail();
            var msg = new ApiResult<Utilities.LDAPModel.CustomerDetail>();
            try
            {

                if (AccountNo == "" || AccountNo == null)
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = "Account Number is a required field. Please supply Merchant Account Number";
                    return msg;
                }




                  if(_configuration["CheckExistingAccount"] =="YES")
                {

                    var checkExistingRecord = await _context.PosReq.Where(a => a.AcctNo == AccountNo).ToListAsync();

                    if (checkExistingRecord.Count > 0)
                    {
                        msg.HasError = true;
                        msg.StatusCode = CommonResponseMessage.MobileFailed;
                        msg.Message = "An existng account number was found.";
                        return msg;
                    }

                }


                // validate the account 

                Utilities.Common.LDAP lp = new LDAP(_configuration);

                customerInfo = lp.ValidateAccount_Old(AccountNo);

                if (customerInfo.CustomerName == "Error")
                {
                    msg.HasError = true;
                    msg.Message = "Account Cannot be Validated at the moment!!";
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Result = customerInfo;
                }
                else
                {
                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = customerInfo;
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


        public string tokenEngine()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }
        public async Task<ApiResult<Token>> AccessToken()
        {
            var msg = new ApiResult<Token>();

            try
            {
                // generate new record 
                var token = new Token();

                string tokenValue = tokenEngine();
                DateTime expiredDate = DateTime.Now.AddHours(Convert.ToInt32(_configuration["TokenExpireDuration"]));

                var createASession = new AccessToken
                {
                    DateCreated = DateTime.Now,
                    TokenKey = tokenValue,
                    ExpireDate = expiredDate
                };

                await _context.AccessToken.AddAsync(createASession);
              await  _context.SaveChangesAsync();

                token.AccessToken = tokenValue;
                token.ExpireDate = expiredDate;

                msg.HasError = false;
                msg.Message = CommonResponseMessage.FetchSuccessMessage;
                msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                msg.Result = token;
                return msg;
            }
            catch(Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }
    }
}
