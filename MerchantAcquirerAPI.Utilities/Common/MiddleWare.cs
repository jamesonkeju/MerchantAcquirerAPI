using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Newtonsoft.Json;



namespace MerchantAcquirerAPI.Utilities.Common
{
    public class MiddleWare
    {

        private static string Baseurl = "";
      

        private static string ApplicationState = ConfigHelper.AppSetting("ApplicationState", "AXAMansard");

        private static string TestBaseurl = ConfigHelper.AppSetting("BaseUrlTest", "AXAMansard");

        private static string BaseUrlProduction = ConfigHelper.AppSetting("BaseUrlProduction", "AXAMansard");
       
        private static string ProductionSecret = ConfigHelper.AppSetting("ProductionSecret", "AXAMansard");

        private static string TestSecret = ConfigHelper.AppSetting("TestSecret", "AXAMansard");


        private static string PartnerCodeTest = ConfigHelper.AppSetting("PartnerCodeTest", "AXAMansard");

        private static string PartnerCodeProduction = ConfigHelper.AppSetting("PartnerCodeProduction", "AXAMansard");


        private static string testAPI = ConfigHelper.AppSetting("apiUrlTest", "AXAMansard");

        private static string productionAPI = ConfigHelper.AppSetting("ApiUrlProduction", "AXAMansard");



        private static string SercetCode = "";
        private static string PartnerCode = "";
        private static string baseurl = "";

        private static string testAPIurl = "";
        private static string ProductionAPIurl = "";

        public static async Task<string> GetAsync(string ApiCall, string token, bool addToken)
        {
            if (ApplicationState.ToLower() == "production")
            {
                SercetCode = ProductionSecret;
                PartnerCode = ProductionSecret;
                baseurl = BaseUrlProduction;
            }
            else
            {
                SercetCode = ProductionSecret;
                PartnerCode = PartnerCodeTest;
                baseurl = TestBaseurl;
            }

            string buildurl = baseurl + ApiCall;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();

                if (addToken == true)
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                }
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage resultRespone = await client.GetAsync(ApiCall);

                using (var response = await client
                                                            .GetAsync(buildurl)
                                                            .ConfigureAwait(false))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return data;
                }
            }
        }


        public static async Task<string> PostBasicAsync(object content, CancellationToken cancellationToken, string token, string apiurl, bool addToken = true)
        {
            if (ApplicationState.ToLower() == "production")
            {
                SercetCode = ProductionSecret;
                PartnerCode = ProductionSecret;
                baseurl = BaseUrlProduction;
            }
            else
            {
                SercetCode = ProductionSecret;
                PartnerCode = PartnerCodeTest;
                baseurl = TestBaseurl;
            }

            string buildurl = baseurl + apiurl;

            var client = new HttpClient();


            using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);


                if (addToken == true)
                {

                    client.DefaultRequestHeaders.Add("token", token);
                    client.DefaultRequestHeaders.Add("Authorization", token);

                }

                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                                       .PostAsync(buildurl, stringContent)
                                       .ConfigureAwait(false))
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return data;
                    }

                }
            }
        }
        public static async Task<string> PortalPostBasicAsync(object content, CancellationToken cancellationToken,string apiurl)
        {
            if (ApplicationState.ToLower() == "production")
            {

                baseurl = productionAPI;
                
            }
            else
            {
                baseurl = testAPI;
            }

            string buildurl = baseurl + apiurl;

            var client = new HttpClient();


            using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);


             

                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                                       .PostAsync(buildurl, stringContent)
                                       .ConfigureAwait(false))
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return data;
                    }

                }
            }
        }

        public static async Task<string> PortalGetBasicAsync(string ApiCall)
        {
            if (ApplicationState.ToLower() == "production")
            {

                baseurl = productionAPI;

            }
            else
            {
                baseurl = testAPI;
            }

            string buildurl = baseurl + ApiCall;

           


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();

             
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage resultRespone = await client.GetAsync(ApiCall);

                using (var response = await client
                                                            .GetAsync(buildurl)
                                                            .ConfigureAwait(false))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return data;
                }
            }
        }
    }
}