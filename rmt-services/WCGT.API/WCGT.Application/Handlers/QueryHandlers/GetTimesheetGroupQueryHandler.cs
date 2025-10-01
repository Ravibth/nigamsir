using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.DTOs;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetTimesheetGroupQuery : IRequest<List<WcgtTimesheetGroup>>
    {
        public string JobCode { get; set; }
        public string TimeOption { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class GetTimesheetGroupQueryHandler : IRequestHandler<GetTimesheetGroupQuery, List<WcgtTimesheetGroup>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetTimesheetGroupQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<WcgtTimesheetGroup>> Handle(GetTimesheetGroupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _repository.GetProjectGroupTimesheet(request.JobCode, request.TimeOption, request.StartDate, request.EndDate);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
