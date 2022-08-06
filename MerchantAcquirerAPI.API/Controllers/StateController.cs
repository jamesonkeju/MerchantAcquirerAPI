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
    public class StateController : BaseController
    {
       
        private IState _state;

        public StateController( IState  state)
        {

            _state = state;
        }

              /// <summary>
              /// Get all states
              /// </summary>
              /// <returns></returns>
     
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<State>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetState()
        {

            try
            {
                var data = await _state.GetState();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<State>
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
