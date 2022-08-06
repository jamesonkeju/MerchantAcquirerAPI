using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.API.Shared;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.Account.Interface;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.CustomerRequest.dto;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Services.FileHandler;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MerchantAcquirerAPI.API.Controllers
{
    //[CustomRoleFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerRequestController : BaseController
    {
        private IConfiguration _configuration;
        private IAccount _account;
        private ICustomerRequest _customerRequest;
        private IFileHandler _fileHandler;

        public CustomerRequestController(ICustomerRequest  customerRequest, 
            IFileHandler fileHandler, IConfiguration configuration,IAccount  account )
        {
            _customerRequest = customerRequest;
            _fileHandler = fileHandler;
            _configuration = configuration;
            _account = account;
        }


        /// <summary>
        /// Fetch Customer POS requests by account number 
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<CustomerRequestReponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomerRequestByAccountNo(string AccountNo)
        {

            try
            {
                var data = await _customerRequest.GetCustomerRequestByAccountNo(AccountNo);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<CustomerRequestReponse>
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
        /// Fetch Customer POS requests by  MerchantId 
        /// </summary>
        /// <param name="MerchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<CustomerRequestReponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPOSRequestByMerchantId(string MerchantId)
        {

            try
            {
                var data = await _customerRequest.GetPOSRequestByMerchantId(MerchantId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<CustomerRequestReponse>
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
        /// Fetch Customer POS requests status by account no 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<POSRequestResponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CheckPOSRequestStatus(string accountNo)
        {

            try
            {
                var data = await _customerRequest.CheckPOSRequestStatus(accountNo);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<POSRequestResponse>
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
        /// Fetch Customer POS requests by terminal number 
        /// </summary>
        /// <param name="Terminal"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<CustomerRequestReponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPOSRequestByTerminalid(string Terminal)
        {

            try
            {
                var data = await _customerRequest.GetPOSRequestByTerminalid(Terminal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<CustomerRequestReponse>
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
        /// This method will validate the customer's account number
        /// </summary>
        /// <param name="AccountNo"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<Utilities.LDAPModel.CustomerDetail>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AccountValidation(string? AccountNo)
        {

            try
            {
                var data = await _account.AccountValidation(AccountNo);
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<Utilities.LDAPModel.CustomerDetail>
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
        /// This method will be used to create a POS request. All required fields should be supplied 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<POSRequestResponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePOSRequest([FromForm] POSRequest payload)
        {

            bool firstupoload = false;
            bool secondupload = false;
            string resultAdded = "";
            var msg = new  ApiResult <POSRequestResponse> ();
            var returnmessage = new POSRequestResponse();
            returnmessage.Status = "Incomplete";

            try
            {

                // do some validation

                if(payload == null)
                {
                    var u = new ApiResult<POSRequestResponse>
                    {
                        HasError = true,
                        Result = returnmessage,
                        Message = "Kindly supply all the required details",
                        StatusCode = CommonResponseMessage.MobileFailed
                    };
                    return Ok(u);

                  
                }


                // attempt to upload the files 

                if(payload.ApplicationForm.Length<0)
                {
                    var u = new ApiResult<POSRequestResponse>
                    {
                        HasError = true,
                        Result = returnmessage,
                        Message = "Kindly upload application form",

                        StatusCode = CommonResponseMessage.MobileFailed
                    };
                    return Ok(u);
                }

                string result =  await _fileHandler.UploadFile(payload.ApplicationForm, _configuration["xxxx"]);

                string AppFileName = "";

                if (result.Contains("."))
                {
                    AppFileName = result;
                    firstupoload = true;
                }
                else
                {
                    AppFileName = result;
                }


                string AcceptanceFileName = "";

                if (payload.InternationalAcceptance.Length < 0)
                {
                    secondupload = true;
                }
                else
                {
                     resultAdded = await _fileHandler.UploadFile(payload.InternationalAcceptance, _configuration["xxxx"]);
                    if (resultAdded.Contains("."))
                    {
                        AcceptanceFileName = result;
                        secondupload = true;
                    }
                    else
                    {
                        AcceptanceFileName = resultAdded;
                        secondupload = false;
                    }
                }

               
                if (firstupoload == false)
                {
                    var u = new ApiResult<POSRequestResponse>
                    {
                        HasError = true,
                        Result = returnmessage,
                        Message = "File upload was not successful for " + result,
                        StatusCode = CommonResponseMessage.MobileFailed
                    };
                    return Ok(u);
                }

                if (secondupload == false)
                {
                    var u = new ApiResult<POSRequestResponse>
                    {
                        HasError = true,
                        Result = returnmessage,
                        Message = "File upload was not successful for " + resultAdded,
                        StatusCode = CommonResponseMessage.MobileFailed
                    };
                    return Ok(u);
                }

                
                // validate the account number again


                var data = await _customerRequest.CreatePOSRequest(payload, AppFileName, AcceptanceFileName);
            
                return Ok(data);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<POSRequestResponse>
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
