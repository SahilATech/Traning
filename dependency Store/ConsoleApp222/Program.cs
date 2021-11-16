using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
//<add name="CRM" connectionString="Url=https://intouchcrmdev4.crm.dynamics.com; Username=Tejas.Agarwal@benjaminmoore.com; Password=June@2018; AuthType=Office365;" />

//<add name="CRM" connectionString="Url=https://intouchcrmdev4.crm.dynamics.com; Username=Tejas.Agarwal@benjaminmoore.com; Password=June@2018; AuthType=Office365;" />


namespace ConsoleApp222
{
    class Program
    {
        static void Main(string[] args)
        {

            IOrganizationService organizationService = null;
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = "Sahil@SahilAgarwalTech.onmicrosoft.com";
            clientCredentials.UserName.Password = "Mayur@8619";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            organizationService = new OrganizationServiceProxy(new Uri("https://org88ba4541.api.crm8.dynamics.com/XRMServices/2011/Organization.svc"),
             null, clientCredentials, null);

            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();


            var connection = (ConfigurationManager.ConnectionStrings["CRM"]).ConnectionString;

            using (var conn = new CrmServiceClient(connection))
            {

                var service = conn.OrganizationWebProxyClient != null
                ? conn.OrganizationWebProxyClient
                : (IOrganizationService)conn.OrganizationServiceProxy;

                var metadataRequest = new RetrieveEntityRequest()
                {
                    LogicalName = "opportunity",
                    EntityFilters = EntityFilters.Attributes
                };
                var metadata = (RetrieveEntityResponse)service.Execute(metadataRequest);
                var attributeMetadataCollection = metadata.EntityMetadata.Attributes;
                foreach (var v in attributeMetadataCollection)
                {
                    FindDependencies(service, "opportunity", v.LogicalName, sbOutput , strSeperator, v.LogicalName);
                    
                }
                File.AppendAllText(@"C:\Users\Sahil\Desktop\Data.csv", sbOutput.ToString());
            }
        }

        public static void FindDependencies(IOrganizationService service, string entityName, string fieldName, StringBuilder sbOutput, string strSeperator, string name)
        { 
            var attributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entityName,
                LogicalName = fieldName
            };

            var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);

            var dependenciesRequest = new RetrieveDependenciesForDeleteRequest
            {
                ObjectId = (Guid)attributeResponse.AttributeMetadata.MetadataId,
                ComponentType = 2 //Attribute
            };        

            var dependenciesResponse = (RetrieveDependenciesForDeleteResponse)service.Execute(dependenciesRequest);
            Console.WriteLine("Found {0} dependencies for Component {1}", dependenciesResponse.EntityCollection.Entities.Count,attributeRequest.LogicalName );
            var i = 1;
            string str = "";
            str = str + name + ",";
            foreach (Entity dependency in dependenciesResponse.EntityCollection.Entities)
            {
                Guid componentguide = ((Guid)dependency.Attributes["dependencyid"]);
                int componentType = ((OptionSetValue)dependency.Attributes["dependentcomponenttype"]).Value;
               // string componentname = ((OptionSetValue)dependency.Attributes["dependentcomponenttype"]).Value;
                //Console.WriteLine(componentname);
                string dependentComponentTypeName = GetComponentName(service, componentType);
                Console.WriteLine("Dependency " + i + ": " + dependentComponentTypeName);
                str = str + dependentComponentTypeName + ",";
                i++;  
            }
            sbOutput.AppendLine(string.Join(strSeperator, str));        



        }


        public static string GetComponentName(IOrganizationService service, int componentType)
        {
            string dependentComponentTypeName = string.Empty;
            RetrieveOptionSetRequest componentTypeRequest = new RetrieveOptionSetRequest
            {
                Name = "componenttype",
  
            };

            RetrieveOptionSetResponse componentTypeResponse = (RetrieveOptionSetResponse)service.Execute(componentTypeRequest);
            OptionSetMetadata componentTypeOptionSet = (OptionSetMetadata)componentTypeResponse.OptionSetMetadata;
            foreach (OptionMetadata opt in componentTypeOptionSet.Options)
            {
                if (componentType == opt.Value)
                {
                    dependentComponentTypeName = opt.Label.UserLocalizedLabel.Label;
                   
                }
            }

            return dependentComponentTypeName;
        }
    }
}