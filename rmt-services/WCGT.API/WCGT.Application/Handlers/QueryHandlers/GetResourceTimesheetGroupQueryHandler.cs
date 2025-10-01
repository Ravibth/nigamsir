using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetResourceTimesheetGroupQuery : IRequest<List<WcgtResoureTimesheetGroup>>
    {
        public string JobCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class GetResourceTimesheetGroupQueryHandler : IRequestHandler<GetResourceTimesheetGroupQuery, List<WcgtResoureTimesheetGroup>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetResourceTimesheetGroupQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<WcgtResoureTimesheetGroup>> Handle(GetResourceTimesheetGroupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _repository.GetResourceGroupTimesheet(request.JobCode, request.StartDate, request.EndDate);
                return response;
                // return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
