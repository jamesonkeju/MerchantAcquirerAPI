using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Data.Models.ViewModel;
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
    public class BusinessCategoryController : BaseController
    {
       
        private IBusinessCategory _businessCategory;

        public BusinessCategoryController(IBusinessCategory  businessCategory)
        {

            _businessCategory = businessCategory;
        }


        /// <summary>
        /// Fetch registered business category
        /// </summary>
        
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<BusinessCategoryList>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBusinessCategory()
        {

            try
            {
                var data = await _businessCategory.GetBusinessCategory();
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


        /// <summary>
        /// Fetch Business  Occupation by Business Category
        /// </summary>
        /// <param name="BusinessCategoryName"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<BusinessOccupation>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBusinessOccupations(string BusinessCategoryName)
        {

            try
            {
                var data = await _businessCategory.BusinessOccupations(BusinessCategoryName);
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
