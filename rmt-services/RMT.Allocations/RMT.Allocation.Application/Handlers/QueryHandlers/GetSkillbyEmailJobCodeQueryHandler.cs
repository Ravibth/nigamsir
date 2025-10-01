using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetSkillbyEmailJobCodeQuery : IRequest<List<SkillResponseDto>>
    {
        public string EmailId { get; set; }
        public string JobCode { get; set; }
        public string PipelineCode { get; set; }
    }
    public class GetSkillbyEmailJobCodeQueryHandler : IRequestHandler<GetSkillbyEmailJobCodeQuery, List<SkillResponseDto>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IRequisitionRepository _requisitionRepository;
        public GetSkillbyEmailJobCodeQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IRequisitionRepository requisitionRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _requisitionRepository = requisitionRepository;
        }

        public async Task<List<SkillResponseDto>> Handle(GetSkillbyEmailJobCodeQuery request, CancellationToken cancellationToken)
        {
            var allocations = await _resourceAllocationRepository.GetProjectsByEmployeeEmailAndPipelineCode(request.EmailId, request.PipelineCode, request.JobCode, null);
            List<Guid> requistions = allocations.Select(allocation => allocation.RequisitionId).ToList();
            var skill = await _requisitionRepository.GetSkillsByRequstionId(requistions);
            List<SkillResponseDto> response = AllocationMapper.Mapper.Map<List<SkillResponseDto>>(skill);
            return response;
        }
    }
}
