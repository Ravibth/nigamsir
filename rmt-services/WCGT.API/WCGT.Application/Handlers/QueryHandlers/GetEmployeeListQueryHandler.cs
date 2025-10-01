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

    public class GetEmployeeListQuery : IRequest<List<GTEmployeeDTO>>
    {

    }

    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<GTEmployeeDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetEmployeeListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTEmployeeDTO>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllEmployees();
            List<GTEmployeeDTO> response = WcgtMapper.Mapper.Map<List<GTEmployeeDTO>>(result);
            return response;
        }
    }
}
