using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;

namespace Batch_Programming_Demo
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            IOrganizationService service = Connect();

            ExecuteMultipleRequest requestWithResults = new ExecuteMultipleRequest()
            {
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },

                Requests = new OrganizationRequestCollection()
            };

            for(int i = 1; i < 3; i++)
            {
                Entity entity = new Entity("cr224_batchprogramming");
                entity["cr224_name"] = "test" + i;
                CreateRequest cr = new CreateRequest
                {
                    Target = entity
                };
                requestWithResults.Requests.Add(cr);
            }
            
            ExecuteMultipleResponse responseWithResults =  (ExecuteMultipleResponse)service.Execute(requestWithResults);

            ExecuteMultipleRequest requestWithResults1 = new ExecuteMultipleRequest()
            {
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },

                Requests = new OrganizationRequestCollection()
            };

            int j = 1;
            foreach (var responseItem in responseWithResults.Responses)
            {
                
                Entity entity1 = new Entity("cr224_batchprogramming1");
                entity1["cr224_name"] = "test" + j;
                entity1["cr224_batchprogramming"] = new EntityReference("cr224_batchprogramming", new Guid(responseItem.Response["id"].ToString()));
                CreateRequest cr1 = new CreateRequest
                {
                    Target = entity1
                };
                requestWithResults1.Requests.Add(cr1);
                j++;

            }

            ExecuteMultipleResponse responseWithResults1 = (ExecuteMultipleResponse)service.Execute(requestWithResults1);
        }

        public static IOrganizationService Connect()
        {
            IOrganizationService organizationService = null;

            String username = "Sahil@SahilAgarwalTech.onmicrosoft.com";
            String password = "Mayur@8619";

            String url = "https://org88ba4541.api.crm8.dynamics.com/XRMServices/2011/Organization.svc";
            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = username;
                clientCredentials.UserName.Password = password;

                // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(url), null, clientCredentials, null);

                if (organizationService != null)
                {
                    Guid gOrgId = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).OrganizationId;
                    if (gOrgId != Guid.Empty)
                    {
                        Console.WriteLine("Connection Established Successfully...");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured - " + ex.Message);
            }
            return organizationService;
        }
    }
}
