using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using static RMT.Allocation.Domain.ConstantsDomain;
namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllocatedHoursRatioByPipelineCodeQuery : IRequest<List<ProjectAllocatedHoursRatioDto>>
    {
        public List<KeyValuePair<string, string>> pipelineCodes { get; set; }
    }

    public class GetAllocatedHoursRatioByPipelineCodeQueryHandler : IRequestHandler<GetAllocatedHoursRatioByPipelineCodeQuery, List<ProjectAllocatedHoursRatioDto>>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _raRepository;
        public GetAllocatedHoursRatioByPipelineCodeQueryHandler(IRequisitionRepository requisitionRepository, IResourceAllocationRepository raRepository)
        {
            _requisitionRepository = requisitionRepository;
            _raRepository = raRepository;
        }

        public async Task<List<ProjectAllocatedHoursRatioDto>> Handle(GetAllocatedHoursRatioByPipelineCodeQuery request, CancellationToken cancellationToken)
        {

            List<ProjectAllocatedHoursRatioDto> response = new List<ProjectAllocatedHoursRatioDto>();
            ProjectAllocatedHoursRatioDto result = null;

            List<ProjectAllocatedHoursRatioDto> requsitionTotalHours = await _requisitionRepository.GetAllProjectRequisitionHoursByPipelineCode(request.pipelineCodes);
            List<ProjectAllocatedHoursRatioDto> radAllocTotalHours = await _raRepository.GetAllProjectAllocationHoursByPipelineCode(request.pipelineCodes);

            foreach (var pc in request.pipelineCodes)
            {
                try
                {
                    var allocatedTotalHoursItem = radAllocTotalHours
                        .Where(x =>
                            x.pipelineCode == pc.Key
                            && ((x.jobCode == null && pc.Value == null) || x.jobCode == pc.Value))
                        .FirstOrDefault();
                    var requistionTotalHoursItem = requsitionTotalHours
                        .Where(x =>
                            x.pipelineCode == pc.Key
                            && ((x.jobCode == null && pc.Value == null) || x.jobCode == pc.Value))
                        .FirstOrDefault();
                    result = new ProjectAllocatedHoursRatioDto()
                    {
                        pipelineCode = pc.Key,
                        jobCode = pc.Value,
                        allocatedTotalHours = allocatedTotalHoursItem != null ? allocatedTotalHoursItem.allocatedTotalHours : 0,
                        requistionTotalHours = requistionTotalHoursItem != null ? requistionTotalHoursItem.requistionTotalHours : 0,                        
                    };
                }
                catch (Exception ex)
                {
                    result = new ProjectAllocatedHoursRatioDto()
                    {
                        pipelineCode = pc.Key,
                        jobCode = pc.Value,
                        allocatedTotalHours = 0,
                        requistionTotalHours = 0,
                    };
                }
                response.Add(result);
            }

            return response;
        }
    }
}
