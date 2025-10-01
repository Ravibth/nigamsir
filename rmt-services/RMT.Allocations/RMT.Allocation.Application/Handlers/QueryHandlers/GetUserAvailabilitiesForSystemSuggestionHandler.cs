using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetUserAvailabilitiesForSystemSuggestionQuery : IRequest<List<GetUserAvailabilitiesForSystemSuggestionResponse>>
    {
        public string? job_code { get; set; }
        public string pipeline_code { get; set; }
        public Guid requisition_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public Int64 required_hours { get; set; }
        public List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion> users { get; set; }
    }
    public class GetUserAvailabilitiesForSystemSuggestionHandler : IRequestHandler<GetUserAvailabilitiesForSystemSuggestionQuery, List<GetUserAvailabilitiesForSystemSuggestionResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;

        public GetUserAvailabilitiesForSystemSuggestionHandler(
            IWCGTMasterHttpApi wCGTMasterHttpApi
            , IResourceAllocationRepository resourceAllocationRepository
        )
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<GetUserAvailabilitiesForSystemSuggestionResponse>> Handle(GetUserAvailabilitiesForSystemSuggestionQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAllocationRepository.GetAvailabilityForSystemSuggestion(request.users, request.required_hours, request.start_date, request.end_date, request.job_code, request.pipeline_code);

        }
    }
}
