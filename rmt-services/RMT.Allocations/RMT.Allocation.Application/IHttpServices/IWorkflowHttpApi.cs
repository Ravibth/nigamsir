using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IWorkflowHttpApi
    {
        Task TerminateWorkflow(List<TerminateWorkflowDTO> workflowRADGUID, string token, UserInfoDTO userInfo);
    }
}
