using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.DTO;
using WCGT.Domain.IRepositories;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetUserLeaveHolidayResponseQuery : IRequest<List<GetUserLeaveHolidayResponseWithUserMaster>>
    {
        public string designation { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
    public class GetUserLeaveHolidayResponseQueryHandler : IRequestHandler<GetUserLeaveHolidayResponseQuery, List<GetUserLeaveHolidayResponseWithUserMaster>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetUserLeaveHolidayResponseQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserLeaveHolidayResponseWithUserMaster>> Handle(GetUserLeaveHolidayResponseQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetUserLeavesHolidays(request.designation, request.start_date, request.end_date);
        }
    }
}
