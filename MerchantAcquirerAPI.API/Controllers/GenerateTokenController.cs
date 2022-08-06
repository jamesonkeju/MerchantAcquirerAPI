using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Data.Models.ViewModel;
using MerchantAcquirerAPI.Services.Account.DTO;
using MerchantAcquirerAPI.Services.Account.Interface;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.BusinessCategory.Interface;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MerchantAcquirerAPI.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenerateTokenController : BaseController
    {

        private IAccount _account;

        public GenerateTokenController(IAccount account)
        {

            _account = account;
        }

        /// <summary>
        /// Generate System token. The token will expire after 24 hours of generation
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<Token>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AccessToken()
        {

            try
            {
                var data = await _account.AccessToken();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<Token>
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