using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApprovelActionPlugin
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                String entityName = context.InputParameters["EntityLogicalName"].ToString();
                String id = context.InputParameters["id"].ToString();
                // var target = context.InputParameters["Target"] as Entity;

                QueryExpression query = new QueryExpression
                {
                    EntityName = "hrms_appsetting",
                    Criteria = new FilterExpression
                    {
                        Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = "hrms_name",
                            Operator = ConditionOperator.Equal,
                            Values = { entityName }
                        }
                    }
                    },
                    ColumnSet = new ColumnSet(true)
                };
                var Json = service.RetrieveMultiple(query).Entities[0];
                tracingService.Trace("Json");
                var target = service.Retrieve(entityName, new Guid(id.ToString().Replace("{", "").Replace("}", "")), new ColumnSet(true));
                tracingService.Trace("project master");
                string errorMessage1 = "";
                var flag = false;

                var arr = JsonConvert.DeserializeObject<JArray>((string)Json["hrms_json"]);
                int i = 1;
                foreach (var col in arr)
                {
                    if (!target.Contains(col["fieldName"].ToString().Replace("{", "").Replace("}", "")))
                    {
                        errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                        flag = true;
                    }
                    else
                    {
                        var value = target[col["fieldName"].ToString().Replace("{", "").Replace("}", "")];
                        if (value == null)
                        {
                            errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                            flag = true;
                            //throw new InvalidPluginExecutionException(errorMessage);
                        }
                        else if (value is string && string.IsNullOrEmpty(value as string))
                        {
                            errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                            flag = true;
                        }
                    }

                }

                context.OutputParameters["Output"] = errorMessage1;

            }

            catch (Exception ex)
            {
                tracingService.Trace(ex.Message.ToString());
            }

        }
    }
    
}
