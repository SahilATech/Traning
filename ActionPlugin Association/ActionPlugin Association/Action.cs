using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionPlugin_Association
{
    public class Action : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            //tracingService.Trace("Association:execute");
            try
            {
                var name = context.InputParameters["name"];
                var lastname = context.InputParameters["lastname"];
                var email = context.InputParameters["email"];
                var Contact = context.InputParameters["Contact"];
                bool manager = (bool)context.InputParameters["manager"];

                if (!manager)
                {
                    Entity managerEntity = new Entity("cr224_manager");
                    managerEntity["cr224_name"] = name;
                    managerEntity["cr224_lastname"] = lastname;
                    managerEntity["cr224_contactno"] = Contact;
                    managerEntity["cr224_email"] = email;

                    Guid manager_id = service.Create(managerEntity);

                    context.OutputParameters["Lookup"] = manager_id;
                   
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }
    }
}
