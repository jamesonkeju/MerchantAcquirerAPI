using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.CustomerRequest.dto;
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
    public class CustomerRequestController : BaseController
    {
       
        private ICustomerRequest _customerRequest;

        public CustomerRequestController(ICustomerRequest  customerRequest)
        {

            _customerRequest = customerRequest;
        }


        /// <summary>
        /// Fetch Customer POS request status by account number 
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<CustomerRequestReponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerRequestStatus(string AccountNo)
        {

            try
            {
                var data = await _customerRequest.GetCustomerRequestStatus(AccountNo);
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
