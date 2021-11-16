using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace D365_entity_data1
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create IOrgannization Service Object
            IOrganizationService service = ConnectD35OnlineUsingOrgSvc();
            try
            {
                if (service != null)
                {
                    Console.WriteLine("Enter Entity Name");

                    var entity = Console.ReadLine();

                    QueryExpression query = new QueryExpression
                    {
                        EntityName = entity,
                        ColumnSet = new ColumnSet(true)
                    };

                   

                    EntityCollection Entity = service.RetrieveMultiple(query);
                    
                    foreach (var count in Entity.Entities.ToList<Entity>())
                    {
                        Console.WriteLine(count.GetAttributeValue<string>("cr224_firstname"));
                        foreach (var column in count.Attributes)
                        {
                            Console.WriteLine("\t\t" + column.Key + " : " + column.Value);
                            //Console.WriteLine(count.GetAttributeValue<string>("cr224_fullname"));
                        }
                        //Console.WriteLine(count.GetAttributeValue<string>("cr224_fullname"));
                        
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.ReadLine();
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }


        public static IOrganizationService ConnectD35OnlineUsingOrgSvc()
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured - " + ex.Message);
                return null;
            }
            return organizationService;

            //var connection = (ConfigurationManager.ConnectionStrings["CRM"]).ConnectionString;
            //using (var conn = new CrmServiceClient(connection))
            //{

            //    var service = conn.OrganizationWebProxyClient != null
            //    ? conn.OrganizationWebProxyClient
            //    : (IOrganizationService)conn.OrganizationServiceProxy;
            //    return service;
            //}

        }

    }
}
