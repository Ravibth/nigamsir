using Gateway.API.ServiceLayerHelper.DTOs;
using System.Threading.Tasks;

namespace Gateway.API.ServiceLayerHelper.WorkflowService
{
    public interface IWorkflowService
    {
        Task initWorkflow(NotificationPayloadDTO payload);
    }
}
