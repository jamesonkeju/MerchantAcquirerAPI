using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MerchantAcquirerAPI.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountTypeController : BaseController
    {
       
        private IAccountType _accountType;

        public AccountTypeController( IAccountType accountType)
        {
            
            _accountType = accountType;
        }


        /// <summary>
        /// This API contains all Account Types needed by the system. This can be stored on your local storage 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<AcctType>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FetchAccountTypes()
        {

            try
            {
                var data = await _accountType.GetAccountTypes();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<AcctType>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }

    }
}
