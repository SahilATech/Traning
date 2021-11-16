using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace BPF_Move
{
    public class BPFMove : IPlugin
    {
        private Guid entity2Id;
        private Guid _BPFId;
        private string _procInstanceLogicalName;
        private Guid _activeStageId;
        private string _activeStageName;
        private int _activeStagePosition;
        private Guid _nextActiveStageId;

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            try
            {               
                Entity entity = (Entity)context.InputParameters["Target"];
                var name = entity["cr224_name"];

                if (entity.Id != null)
                {
                    Entity entity2 = new Entity("cr224_actioncustom2");
                    entity2["cr224_name"] = name;
                    entity2["cr224_actioncustom1"] = new EntityReference(entity.LogicalName, entity.Id);
                    entity2Id = service.Create(entity2);
                }
                else
                {
                    return;
                }

                //BPF 
                RetrieveProcessInstancesRequest procOpp1Req = new RetrieveProcessInstancesRequest
                {
                    EntityId = entity.Id,
                    EntityLogicalName = "cr224_actioncustom1"
                };

                RetrieveProcessInstancesResponse procOpp1Resp = (RetrieveProcessInstancesResponse)service.Execute(procOpp1Req);
                
                Entity activeProcessInstance = null;

                if (procOpp1Resp.Processes.Entities.Count > 0)
                {
                    activeProcessInstance = procOpp1Resp.Processes.Entities[0]; // First record is the active process instance
                    
                    _BPFId = activeProcessInstance.Id;                                                      
                    _procInstanceLogicalName = activeProcessInstance["name"].ToString().Replace(" ", string.Empty).ToLower();
                    _activeStageId = new Guid(activeProcessInstance.Attributes["processstageid"].ToString());
                }
                               
                 // Retrieve the process stages in the active path of the current process instance
                RetrieveActivePathRequest pathReq = new RetrieveActivePathRequest
                {
                    ProcessInstanceId = _BPFId
                };
                RetrieveActivePathResponse pathResp = (RetrieveActivePathResponse)service.Execute(pathReq);

                Console.WriteLine("\nRetrieved stages in the active path of the process instance:");
                for (int i = 0; i < pathResp.ProcessStages.Entities.Count; i++)
                {
                    Console.WriteLine("\tStage {0}: {1} (StageId: {2})", i + 1,
                                     pathResp.ProcessStages.Entities[i].Attributes["stagename"], pathResp.ProcessStages.Entities[i].Attributes["processstageid"]);

                    // Retrieve the active stage name and active stage position based on the activeStageId for the process instance
                    if (pathResp.ProcessStages.Entities[i].Attributes["processstageid"].ToString() == _activeStageId.ToString())
                    {
                        _activeStageName = pathResp.ProcessStages.Entities[i].Attributes["stagename"].ToString();
                        _activeStagePosition = i;
                    }
                }

                Console.WriteLine("\nActive stage for the process instance: '{0}' (StageID: {1})", _activeStageName, _activeStageId);
                  
                // Retrieve the stage ID of the next stage that you want to set as active
                _nextActiveStageId = (Guid)pathResp.ProcessStages.Entities[_activeStagePosition + 1].Attributes["processstageid"];
                              
                Entity retrievedProcessInstance = service.Retrieve("new_actioncustom21", _BPFId, new ColumnSet(true));

                // Update the active stage to the next stage              
                retrievedProcessInstance["traversedpath"] = retrievedProcessInstance["traversedpath"] + ","  + _nextActiveStageId.ToString().Replace("{", string.Empty).Replace("}", string.Empty);
                retrievedProcessInstance["bpf_cr224_actioncustom2id"] = new EntityReference("cr224_actioncustom2", entity2Id);
                retrievedProcessInstance["activestageid"] = new EntityReference("processstage", _nextActiveStageId);
                service.Update(retrievedProcessInstance);
                                    
                
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }


        }
    }
}
