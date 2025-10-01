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

    public class GetLeavesByParamListQuery : IRequest<List<GTLeaveDTO>>
    {
        // public string? emp_mid { get; set; }
        public List<string>? emp_emailid { get; set; }
        public DateTime? created_at { get; set; }

    }
    public class GetLeavesByParamListQueryHandler : IRequestHandler<GetLeavesByParamListQuery, List<GTLeaveDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetLeavesByParamListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTLeaveDTO>> Handle(GetLeavesByParamListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllLeavesByParam(request.emp_emailid, request.created_at);
            List<GTLeaveDTO> response = WcgtMapper.Mapper.Map<List<GTLeaveDTO>>(result);
            return response;
        }
    }
}
