using d365_data_consle.Dto;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace d365_data_consle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url_token = "https://login.microsoftonline.com/23bf93b2-ff16-44b1-afbd-e2fade9fcc3d/oauth2/v2.0/token";
            string url_data = "https://org1165172b.api.crm8.dynamics.com/api/data/v9.2/";
            string client_id = "94666258-0c2d-4f37-b59a-3125d037270e";
            string client_secrit = "rB2_1c2fcy6lpgf56OaY.g-Eeq.V55JMWx";
            var scope = "https://org1165172b.crm8.dynamics.com/.default";

            string token = await Gettoken(client_id, url_token, client_secrit, scope);

            if (token != null)
            {
                Console.Write("You went to get Specify Entity data (Y/n) ? ");
                var opt = Console.ReadLine();

                if (opt == "n" || opt == "N")
                {
                    var data = await getdata(token, url_data);
                    Console.WriteLine(data);
                    Console.ReadKey();
                }
                else if (opt == "y" || opt == "Y")
                {

                    Console.WriteLine("Enter Entity Name And Query after url : ");
                    Console.Write(url_data);
                    var query = Console.ReadLine();
                    var data = await getdata(token, (url_data + query));


                    //var accessTokenReponse = JsonConvert.DeserializeObject<Value>(data);


                    Console.WriteLine(data);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                    Console.ReadKey();
                }

            }
        }

        private static async Task<string> getdata(string token, string url_data)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization  = new AuthenticationHeaderValue("Bearer", token);
                    var responseTask = await httpClient.GetStringAsync(url_data);
                    return responseTask;

                }
                  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return null;
            }
        }

        private static async Task<string> Gettoken(string client_id, string url, string client_sec, string scopes)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {

                        var multipartContent = new MultipartFormDataContent();
                        multipartContent.Add(new StringContent("client_credentials"), "grant_type");
                        multipartContent.Add(new StringContent(client_id), "client_id");
                        multipartContent.Add(new StringContent(client_sec), "client_secret");
                        multipartContent.Add(new StringContent(scopes), "scope");
                        

                        var response = await httpClient.PostAsync(url, multipartContent);
                        var content = await response.Content.ReadAsAsync<LoginResponseDto>();
                        
                        return content.AccessToken;
                    
                }           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return null;
            }
        }

    }
}
