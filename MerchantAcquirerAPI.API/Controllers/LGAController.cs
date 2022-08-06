using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.State.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MerchantAcquirerAPI.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LagController : BaseController
    {
       
        private IState _state;

        public LagController( IState  state)
        {

            _state = state;
        }

           /// <summary>
           /// Get all LGA by state Code
           /// </summary>
           /// <param name="StateCode"></param>
           /// <returns></returns>
     
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<Lga>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetLGAByState(string StateCode)
        {

            try
            {
                var data = await _state.GetLGAByState(StateCode);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<Lga>
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
