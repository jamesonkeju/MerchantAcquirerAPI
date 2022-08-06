using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.CustomerRequest.dto
{
    public class POSRequest
    {
        [Required(ErrorMessage ="Account Number is Required")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Account Name is Required")]
        public string AccountName { get; set; }
        [Required] 
        public string AccountType { get; set; }
        [Required] 
        public string CustomerID { get; set; }
        [Required] 
        public string AccountBranch { get; set; }
        [Required] 
        public string AccountCurrency { get; set; }
        [Required] 
        public string AccountSegment { get; set; }
        [Required] 
        public bool MerchantNameDifference { get; set; } = false;
        [Required(ErrorMessage = "Merchant Name is Required")]
        public string MerchantName { get; set; }
        [Required] 
        public string MerchantAlternatePhone { get; set; }
        [Required(ErrorMessage = "Merchant Email is Required")]
        public string MerchantEmail { get; set; }
        [Required] 
        public string MerchantAddress { get; set; }
        [Required(ErrorMessage = "Merchant Physical Address is Required")]
        public string PhysicalAddress { get; set; }
        [Required] 
        public string MerchantURL { get; set; }
        [Required(ErrorMessage = "Contact Title Address is Required")]
        public string ContactTitle { get; set; }
        [Required(ErrorMessage = "Contact Name Address is Required")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Contact Phone Address is Required")]
        public string ContactPhone { get; set; }
        public string SecondaryContactName { get; set; }
        public string SecondaryContactPhone { get; set; }
        public bool ReceiveEmailAlerts { get; set; } = false;
        [Required(ErrorMessage = "Business Category  is Required")]
        public string BusinessCategory { get; set; }
        [Required(ErrorMessage = "Business Occupation  is Required")]
        public string BusinessOccupation { get; set; }
        [Required(ErrorMessage = "MCC Information  is Required")]
        public string MccType { get; set; }
        [Required(ErrorMessage = "State Code is Required")]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "LGA is Required")]
        public string LocalGovernmentArea { get; set; }
        [Required(ErrorMessage = "Prefered Network Operator is Required")]
        public string PreferedNetworkOperator { get; set; }
        [Required(ErrorMessage = "Prefered Terminal Network is Required")]
        public string PreferedTerminalNetwork { get; set; }
        public string SecondaryNetworkOperator { get; set; }
        public string PTSPName { get; set; }
        [Required(ErrorMessage = "Prefered Terminal Owner is Required")]
        public string TermOwnerCode { get; set; }
        [Required(ErrorMessage = "Prefered Term Code is Required")]
        public string TermCode { get; set; }
        [Required] 
        public string ReasonForDifferentName { get; set; }
        [Required(ErrorMessage = "File Application is Required")]
        public IFormFile ApplicationForm  { get; set; }
        public IFormFile InternationalAcceptance { get; set; }
    }
}
