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

    public class GetLeaveListQuery: IRequest<List<GTLeaveDTO>>
    {

    }
    public class GetLeaveListQueryHandler : IRequestHandler<GetLeaveListQuery, List<GTLeaveDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetLeaveListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTLeaveDTO>> Handle(GetLeaveListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllLeaves();
            List<GTLeaveDTO> response = WcgtMapper.Mapper.Map<List<GTLeaveDTO>>(result);
            return response;
        }
    }
}
