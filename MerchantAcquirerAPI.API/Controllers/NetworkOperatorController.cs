using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.Network.Interface;
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
    public class NetworkOperatorController : BaseController
    {
       
        private INetwork _network;

        public NetworkOperatorController(INetwork  network)
        {

            _network = network;
        }

              
     /// <summary>
     /// Get list of network operators
     /// </summary>
     /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<Data.Models.Domains.OperatorList>>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetNetworkOperators()
        {

            try
            {
                var data = await _network.GetNetworkOperators();
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<OperatorList>
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
        /// Get all network details by operator name
        /// </summary>
        /// <param name="OperatorName"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<Data.Models.Domains.NetworkTypeList>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetNetworkDetail(string OperatorName)
        {

            try
            {
                var data = await _network.GetNetworkDetail(OperatorName);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<NetworkTypeList>
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



