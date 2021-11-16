using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action_Plugin_Demo
{
    public class ActionPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace("sum:execute");
            // Obtain the execution context from the service provider.

            // For this sample, execute the plug-in code only while the client is online. 


            var Number1 = (int)context.InputParameters["a"];
            var Number2 = (int)context.InputParameters["b"];
            var Number3 = Number1 + Number2;
            tracingService.Trace("a:" + Number1);
            tracingService.Trace("b:" + Number2);
            tracingService.Trace("sum:" + Number3);
            context.OutputParameters["sum"] = Number3;
                
        }

    }
}
