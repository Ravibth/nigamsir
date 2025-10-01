using RMT.Skill.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.IHttpServices
{
    public interface IWorkflowHttpService
    {
        Task<List<WorkflowDTO>> GetWorkflowDetailsByItemId(List<Guid> item_id);
    }
}
