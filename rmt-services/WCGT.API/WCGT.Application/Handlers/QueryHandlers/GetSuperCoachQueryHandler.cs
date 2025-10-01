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

    public class GetEmployeesBySuperCoachOrCSCQuery : IRequest<List<GTEmployeeDTO>>
    {
        public string emp_mid { get; set; }

    }
    public class GetEmployeesBySuperCoachOrCSCQueryHandler : IRequestHandler<GetEmployeesBySuperCoachOrCSCQuery, List<GTEmployeeDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetEmployeesBySuperCoachOrCSCQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTEmployeeDTO>> Handle(GetEmployeesBySuperCoachOrCSCQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetEmployeeBySuperCoachOrCSC(request.emp_mid);
            List<GTEmployeeDTO> response = WcgtMapper.Mapper.Map<List<GTEmployeeDTO>>(result);
            return response;
        }
    }
}
