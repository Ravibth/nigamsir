using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs.Base;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetProjectBudgetByModifiedDateRangeQuery : IRequest<List<string>>
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class GetProjectBudgetByModifiedDateRangeQueryHandler : IRequestHandler<GetProjectBudgetByModifiedDateRangeQuery, List<string>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetProjectBudgetByModifiedDateRangeQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<string>> Handle(GetProjectBudgetByModifiedDateRangeQuery request, CancellationToken cancellationToken)
        {
            List<string> result = await _repository.GetProjectBudgetByModifiedDateRange(request.startDate, request.endDate);

            return result;
        }
    }

}
