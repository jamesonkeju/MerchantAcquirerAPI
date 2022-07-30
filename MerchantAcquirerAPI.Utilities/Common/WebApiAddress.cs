using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Utilities.Common
{
    public static class WebApiAddress
    {

        public static string Authenication = "api/v1/partner/authenticate";
        public static string GetProductList = "api/v1/partner/get-products";
        public static string GetKYC = "api/v1/partner/get-kyc";
        public static string GetStates = "api/v1/partner/get-states";
        public static string GetLocalGovt = "api/v1/partner/get-local-govts";
        public static string GetHospitalList = "api/v1/partner/get-hospitals";
        public static string Createpartnerscustomers = "api/v1/partner/createpartnerscustomers";
        public static string Renewpolicy = "api/v1/partner/renew-policy";
        public static string Getpolicydetails = "api/v1/partner/get-policy-details";
        public static string GetPolicybyPartner = "api/v1/partner/get-policy-by-partner";
    }

    public static class ApplicationURL
    {

        public static string Authenication = "api/v1/partner/authenticate";
        public static string GetProductList = "/Products/GetProducts";
        public static string GetKYC = "/Products/GetProductKYC";
        public static string GetStates = "api/v1/partner/get-states";
        public static string GetLocalGovt = "api/v1/partner/get-local-govts";
        public static string GetHospitalList = "api/v1/partner/get-hospitals";
        public static string Createpartnerscustomers = "/Products/CreatePartnersCustomers";
        public static string Renewpolicy = "/Products/RenewPolicy";
        public static string Getpolicydetails = "/Products/GetPolicyDetails";
        public static string GetPolicybyPartner = "api/v1/partner/get-policy-by-partner";
        public static string GetProvider = "/Products/GetProvider";
        public static string GetLocalGovernment = "/Products/GetLocalGovernment";
    }

    public static class ProcessStatus
    {
        public static int Pending = 1;
        public static int Processed = 2;
        public static int BusinessMananger = 3;

    }

    public static class Cables
    {
        public static string DSTV = "DSTV";
        public static string GOTV = "GOTV";
        public static string STARTIMES = "STARTIMES";
    }
}
