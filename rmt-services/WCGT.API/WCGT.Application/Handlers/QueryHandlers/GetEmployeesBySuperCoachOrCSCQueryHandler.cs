using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{

    public class GetSuperCoachCQuery : IRequest<List<SuperCoach>>
    {

    }
    public class GetSuperCoachCQueryHandler : IRequestHandler<GetSuperCoachCQuery, List<SuperCoach>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetSuperCoachCQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SuperCoach>> Handle(GetSuperCoachCQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllSuperCoach();
            return result;
        }
    }
}
