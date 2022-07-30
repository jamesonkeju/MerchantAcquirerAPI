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

        public AccountServices(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IConfiguration configuration,
         
            MerchantAcquirerAPIAppContext context, IActivityLog activityRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
           
            _context = context;
            _roleManager = roleManager;
            _activityRepo = activityRepo;
            _httpContextAccessor = httpContextAccessor;
        }

          public Task<MessageOut> Register(AdminUserSettingViewModel payload)
        {
            throw new NotImplementedException();
        }

        #region Seceret
    
        private async Task PrepareSignInClaims(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);

            var _claims = userClaims.ToList();
            string roles = string.Empty;
            IList<string> role = await _userManager.GetRolesAsync(user);

           
            string USERPERMISSION = SetUserPermissions(user.Id);

            var RoleName = await _roleManager.FindByIdAsync(user.RoleId);


            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.FirstName + ' '  + user.LastName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, RoleName.Name),
                            new Claim(ClaimTypes.Surname, user.LastName),
                            new Claim(ClaimTypes.MobilePhone, user.MobileNumber),
                            new Claim(ClaimTypes.GivenName, user.FirstName + ' '  + user.LastName),
                            new Claim(ClaimTypes.Anonymous, RoleName.RoleName),
                            new Claim(ClaimTypes.PostalCode, RoleName.Id)

                        }.Union(userClaims);

            foreach (var r in role)
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, r));
            }

            _claims = claims.ToList();

        
            await _signInManager.SignInWithClaimsAsync(user, false, _claims);
        }

        private string SetUserPermissions(string UserId)
        {
            try
            {

                AccessDataLayer accessDataLayer = new AccessDataLayer(_context);
                DBManager dBManager = new DBManager(_context);

                var parameters = new List<IDbDataParameter>();

                parameters.Add(dBManager.CreateParameter("@UserId", UserId, DbType.String));
                DataTable mUserPremissionRolemodel = accessDataLayer.FetchUserPermissionAndRole(parameters.ToArray(), "FetchUserPermissionAndRole");



                string userPermissions = "";
                if (mUserPremissionRolemodel != null)
                {
                    int i = 0;
                    foreach (DataRow item in mUserPremissionRolemodel.Rows)
                    {
                        i = i + 1;
                        if (i == 0)
                        {
                            userPermissions = item["PermissionCode"] + ",";
                        }
                        else
                        {
                            userPermissions = userPermissions + item["PermissionCode"].ToString() + ",";
                        }
                    }
                }
                return userPermissions;
            }
            catch (Exception ex)
            {
                //  _log.Error(ex);
                return string.Empty;
            }
        }

        public Task<MessageOut> Login(UserLoginPayload payload)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
