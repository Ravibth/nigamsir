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

    public class GetEmployeeByParamListQuery : IRequest<GTEmployeeDTO>
    {
        public string? emp_mid { get; set; }
        public string? emp_emailid { get; set; }

    }
    public class GetEmployeeByParamListQueryHandler : IRequestHandler<GetEmployeeByParamListQuery, GTEmployeeDTO>
    {
        private readonly IWcgtDataRepository _repository;
        public GetEmployeeByParamListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<GTEmployeeDTO> Handle(GetEmployeeByParamListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetEmployeeByParam(request.emp_mid, request.emp_emailid);
            GTEmployeeDTO response = null;
            if (result != null)
            {
                response = WcgtMapper.Mapper.Map<GTEmployeeDTO>(result);
            }
            return response;
        }
    }
}
