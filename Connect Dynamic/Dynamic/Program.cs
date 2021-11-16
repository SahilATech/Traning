using System;
using System.Net;
using System.ServiceModel.Description;
using System.Runtime.Serialization;

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;

namespace WebServices00
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create IOrgannization Service Object
            IOrganizationService service = ConnectD35OnlineUsingOrgSvc();

            if (service != null)
            {
                Console.WriteLine("DO you want to see total account records? (y/n)");

                var response = Console.ReadLine();

                if (response != "y")
                    return;

                //code your logic here
                //Get count of contacts in the system
                QueryExpression qe = new QueryExpression("account");
                EntityCollection enColl = service.RetrieveMultiple(qe);
                Console.WriteLine("Total account in the System=" + enColl.Entities.Count);

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
