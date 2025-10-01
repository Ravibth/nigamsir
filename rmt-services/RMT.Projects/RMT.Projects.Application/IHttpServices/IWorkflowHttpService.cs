using RMT.Projects.Application.HttpServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.IHttpServices
{
    public interface IWorkflowHttpService
    {
        Task RefreshAssignedTask(RefreshAssignedWorkflowTask request);
        Task TerminateWorkflowByPipelineCodeAndJobCode(TerminateWorkflowByPipelineCodeAndJobCodeRequestDTO request);
    }
}
