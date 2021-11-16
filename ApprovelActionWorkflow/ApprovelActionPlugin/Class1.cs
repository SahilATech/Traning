using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApprovelActionPlugin
{
    public class Class1 : CodeActivity
    {
        [Input("EntityLogicalName")] //name of input param
        public InArgument<string> entityName { get; set; }
        [Input("id")] //name of input param
        public InArgument<string> id { get; set; }

        [Output("Output")] //name of output param
        public OutArgument<string> Output { get; set; }

        [Output("FieldsLogicalName")] //name of output param
        public OutArgument<string> FieldsLogicalName { get; set; }



        protected override void Execute(CodeActivityContext context)
        {

            IWorkflowContext workflowContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory =  context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service =  serviceFactory.CreateOrganizationService(workflowContext.UserId);
            ITracingService tracer = context.GetExtension<ITracingService>();
            try {
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
                            Values = { entityName.Get(context) }
                        }
                    }
                    },
                    ColumnSet = new ColumnSet(true)
                };
                var Json = service.RetrieveMultiple(query).Entities[0];
                //tracer.Trace("Json");
                var target = service.Retrieve(entityName.Get(context), new Guid(id.Get(context).Replace("{", "").Replace("}", "")), new ColumnSet(true));
                //tracer.Trace("project master");
                string errorMessage1 = "";
                string logicalname = "";

                var arr = JsonConvert.DeserializeObject<JArray>((string)Json["hrms_json"]);
                int i = 1;
                foreach (var col in arr)
                {
                    if (!target.Contains(col["fieldName"].ToString().Replace("{", "").Replace("}", "")))
                    {
                        if (i == 1)
                        {
                            logicalname += col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                        }
                        else
                        {
                            logicalname += "," + col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                        }
                        errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                        
                    }
                    else
                    {
                        var value = target[col["fieldName"].ToString().Replace("{", "").Replace("}", "")];
                        if (value == null)
                        {
                            if (i == 1)
                            {
                                logicalname += col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                            }
                            else
                            {
                                logicalname += "," + col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                            }
                            errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                            //throw new InvalidPluginExecutionException(errorMessage);
                        }
                        else if (value is string && string.IsNullOrEmpty(value as string))
                        {

                            if (i == 1)
                            {
                                logicalname += col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                            }
                            else
                            {
                                logicalname += "," + col["fieldName"].ToString().Replace("{", "").Replace("}", "");
                            }
                            errorMessage1 += $"{i++}. " + col["DisplayName"].ToString().Replace("{", "").Replace("}", "") + "\n";
                        }
                    }

                }
                //tracer.Trace(errorMessage1);
                Output.Set(context, errorMessage1);
                FieldsLogicalName.Set(context, logicalname);
            }

            catch (Exception ex)
            {
                tracer.Trace(ex.Message.ToString());
            }

        }
    }
    
}
