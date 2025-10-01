using MediatR;
using RMT.Skill.API.Constant;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Application.IHttpServices;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetUserSkillByIdQuery : IRequest<List<GetUserSkillsByEmailResponse>>
    {
        public string Id { get; set; }
        public bool approvals { get; set; }
    }
    public class GetUserSkillByIdQueryHandler : IRequestHandler<GetUserSkillByIdQuery, List<GetUserSkillsByEmailResponse>>
    {
        private readonly IUserSkillRepository _userSkillRepository;
        private readonly IWorkflowHttpService _workflowHttpService;
        private readonly IIdentityUserDetailsHttpApi _identityServiceHttpApi;
        public GetUserSkillByIdQueryHandler(IUserSkillRepository userSkillRepository, IWorkflowHttpService workflowHttpService, IIdentityUserDetailsHttpApi identityServiceHttpApi)
        {
            _workflowHttpService = workflowHttpService;
            _userSkillRepository = userSkillRepository;
            _identityServiceHttpApi = identityServiceHttpApi;
        }
        public async Task<List<GetUserSkillsByEmailResponse>> Handle(GetUserSkillByIdQuery query, CancellationToken cancellationToken)
        {
            var userSkills = await _userSkillRepository.GetUserSkillsById(query.Id);
            if (query.approvals)
            {
                List<Guid> item_ids = userSkills.Select(x => x.Id).ToList();
                if (item_ids.Count > 0)
                {
                    List<WorkflowDTO> workflows = await _workflowHttpService.GetWorkflowDetailsByItemId(item_ids);

                    foreach (var item in userSkills)
                    {
                        WorkflowDTO workflowItem = workflows.Where(m => m.item_id.ToLower() == item.Id.ToString().ToLower()).FirstOrDefault();
                        if (workflowItem != null)
                        {
                            var workflowTaskItem = workflowItem?.task_list?.FirstOrDefault();
                            if (workflowTaskItem != null)
                            {
                                List<string> emails = new List<string> { workflowTaskItem.assigned_to };
                                var identityUserResponse = await _identityServiceHttpApi.GetEmployeesDataHttpApiQuery(emails);
                                // item.Approver = workflowTaskItem.assigned_to;
                                var Approver = identityUserResponse.FirstOrDefault(user => user.emailId.ToLower().Trim() == workflowTaskItem.assigned_to.ToLower().Trim());
                                if (Approver != null)
                                {
                                    item.Approver = Approver.name;
                                    item.ApproverEmail = Approver.emailId;
                                }

                                if (workflowTaskItem?.status.ToLower() == Constants.WorkflowTaskStatus.APPROVED.ToLower() || workflowTaskItem.status.ToLower() == Constants.WorkflowTaskStatus.REJECTED.ToLower())
                                {
                                    item.ApprovedOn = workflowTaskItem.updated_at;
                                }
                            }
                        }
                    }
                }
            }
            return userSkills;
        }
    }
}
