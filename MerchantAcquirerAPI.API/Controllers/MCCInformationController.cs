using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.API.Shared;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.MccInformation.Interface;
using MerchantAcquirerAPI.Services.State.Interface;
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
    public class MCCInformationController : BaseController
    {
       
        private IMccInformation _mccInformation;

        public MCCInformationController( IMccInformation   mccInformation)
        {

            _mccInformation = mccInformation;
        }

        /// <summary>
        /// Get all MCC Information
        /// </summary>
        /// <returns></returns>
     
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<MccInfo>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetMccInformationList()
        {

            try
            {
                var data = await _mccInformation.GetMccInformationList();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<MccInfo>
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
