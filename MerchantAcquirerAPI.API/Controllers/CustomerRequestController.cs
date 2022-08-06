using MerchantAcquirerAPI.API.Controllers;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
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

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerRequestController : BaseController
    {
        private IConfiguration _configuration;
        private ICustomerRequest _customerRequest;
        private IFileHandler _fileHandler;

        public CustomerRequestController(ICustomerRequest  customerRequest, IFileHandler fileHandler, IConfiguration configuration )
        {

            _customerRequest = customerRequest;
            _fileHandler = fileHandler;
            _configuration = configuration;
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
