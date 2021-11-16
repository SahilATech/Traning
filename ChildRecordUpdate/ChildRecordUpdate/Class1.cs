using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildRecordUpdate
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
                                
                EntityReference targetEntity = context.InputParameters["Target"] as EntityReference;
                
                var entity = context.InputParameters["Target"] as Entity;

                QueryExpression student = new QueryExpression()
                {
                    EntityName = "cr224_student",
                    ColumnSet = new ColumnSet("cr224_location")
                };
                student.Criteria.AddCondition("cr224_universityid", ConditionOperator.Equal, targetEntity.Id);

                EntityCollection students = service.RetrieveMultiple(student);

                foreach (var record in students.Entities)
                {
                    record["cr224_location"] = entity["cr224_universitylocation"];
                }
                
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }

        }
    }
}
