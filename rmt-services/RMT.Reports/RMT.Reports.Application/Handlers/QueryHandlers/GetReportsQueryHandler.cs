using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Reports.Domain.Entities;
using RMT.Reports.Infrastructure.Repositories;

namespace RMT.Report.Application.Handlers.QueryHandlers
{
    public class GetEmployeeAllocationTimeSheetQuery : IRequest<List<EmployeeAllocationTimeSheetEntity>>
    {
        public string business_unit { get; set; }
    }
    public class GetEmployeeAllocationTimeSheetQueryHandler : IRequestHandler<GetEmployeeAllocationTimeSheetQuery, List<EmployeeAllocationTimeSheetEntity>>
    {
        private readonly IReportRepository _repository;
        public GetEmployeeAllocationTimeSheetQueryHandler(IReportRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<EmployeeAllocationTimeSheetEntity>> Handle(GetEmployeeAllocationTimeSheetQuery request, CancellationToken cancellationToken)
        {
            var transformedRequest = ReportMapper.Mapper.Map<EmployeeAllocationTimeSheetEntity>(request);
            var result = await _repository.GetEmployeeAllocationTimeSheet(transformedRequest);

            // EmployeeAllocationTimeSheetDto response = ReportMapper.Mapper.Map<EmployeeAllocationTimeSheetDto>(result);

            return result;



        }
    }
}
