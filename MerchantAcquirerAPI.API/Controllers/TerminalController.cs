using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.API.Shared;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.BusinessCategory.Interface;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Services.Terminal.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MerchantAcquirerAPI.API.Controllers
{
    [CustomRoleFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TerminalController : BaseController
    {
       
        private ITerminal _terminal;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TerminalController(ITerminal  terminal)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {

            _terminal = terminal;
        }


        /// <summary>
        /// Fetch terminal model
        /// </summary>
        
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<TerminalModel>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTerminalModels()
        {

            try
            {
                var data = await  _terminal.GetTerminalModels();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<TerminalModel>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }





        /// <summary>
        /// Fetch terminal owner
        /// </summary>

        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<TerminalOwner>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTerminalOwners()
        {

            try
            {
                var data = await _terminal.GetTerminalOwners();
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
