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
    public class GetLeavesInfoQuery:IRequest<List<LeaveReport>>
    {
        public List<string> emp_mid { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
    public class GetLeavesInfoQueryHandler : IRequestHandler<GetLeavesInfoQuery, List<LeaveReport>>
    {
        private readonly IWcgtDataRepository _wcgtDataRepository;
        public GetLeavesInfoQueryHandler(IWcgtDataRepository wcgtDataRepository)
        {
            _wcgtDataRepository = wcgtDataRepository;
        }
        public async Task<List<LeaveReport>> Handle(GetLeavesInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _wcgtDataRepository.GetLeavesInfoByStartDateEndDateAndEmails(request.emp_mid, request.start_date, request.end_date);
            return result;
        }
    }
}
