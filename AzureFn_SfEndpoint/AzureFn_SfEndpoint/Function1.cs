using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using RestSharp;

namespace AzureFn_SfEndpoint
{ 
    public static class Function1
    {
        
        [FunctionName("Salesforce")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Salesforce/authentication")] HttpRequest req, ILogger log)
        {
            try
            {
                
                var body = new header
                {
                    username = req.Form["username"],
                    password = req.Form["password"],
                    grant_type = req.Form["grant_type"],
                    client_id = req.Form["client_id"],
                    client_secret = req.Form["client_secret"]
                };

                var url = "https://login.salesforce.com/services/oauth2/token?username=" + body.username + "&password=" + body.password + "&grant_type=" + body.grant_type + "&client_id=" + body.client_id + "&client_secret=" + body.client_secret;

                var handler = new HttpClientHandler();
                handler.UseCookies = false;

                using (var httpClient = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("Post"), url))
                    {
                        
                        var response = await httpClient.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        return contents.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        [FunctionName("SalesforceGetData")]
        public static async Task<string> getAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Salesforce/getdata")] HttpRequest req, ILogger log)
        {
            try
            {
                string token = req.Headers["Authorization"];
                string query = req.QueryString.ToString(); 
                var url = "https://ap5.salesforce.com/services/data/v42.0/query/" + query;

                var handler = new HttpClientHandler();
                handler.UseCookies = false;

                using (var httpClient = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", token);
                        
                        var response = await httpClient.SendAsync(request);
                        var contents = await response.Content.ReadAsStringAsync();
                        return contents.ToString();
                    }
                }
               
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }

    public class header
        {
           public string username { get; set; }
            public string password { get; set; }
            public string grant_type { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
        }
   

  }

   

    
