using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MerchantAcquirerAPI.API.Shared;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models;
using MerchantAcquirerAPI.Data.Models.Indentity;
using MerchantAcquirerAPI.Data.Payload;
using MerchantAcquirerAPI.Services.DataAccess;
using MerchantAcquirerAPI.Utilities.Common;
using MerchantAcquirerAPI.Data.Models.Domains;

namespace MerchantAcquirerAPI.API.Controllers
{
    public class BaseController : ControllerBase
    {
      
        public BaseController()
        {
            
        }

        [NonAction]
        public string getMainAction()
        {
            return ControllerContext.ActionDescriptor.ActionName;
        }

        [NonAction]
        public string getMainController()
        {
            return ControllerContext.ActionDescriptor.ControllerName;
        }
        [NonAction]
        public ApiResult<ApplicationSession> GetCurrentSession()
        {
            var jwt_token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var resp = new ApiResult<ApplicationSession> { HasError = false, Message = "Unable to token!" };

            if (string.IsNullOrWhiteSpace(jwt_token)) return resp;

            using (var _context = new MerchantAcquirerAPIAppContext())
            {

                try
                {
                    
                    return null;
                }

                catch (Exception ex)
                {
                    resp.Message = ex.Message;
                    return resp;
                }


            }

        }

    }
}
