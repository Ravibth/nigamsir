using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.DTOs;
using WCGT.Application.Mappers;
using WCGT.Domain.DTO;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetRateDesignationQuery : IRequest<List<RateDesignationDTO>>
    {
        public List<GetRateDesignationRequestDTO> Designation { get; set; }
    }

    public class GetRateDesignationQueryHandler : IRequestHandler<GetRateDesignationQuery, List<RateDesignationDTO>>
    {

        private readonly IWcgtDataRepository _repository;
        public GetRateDesignationQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RateDesignationDTO>> Handle(GetRateDesignationQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetRateByDesignation(request.Designation);
            List<RateDesignationDTO> finalResponse = new();
            foreach (var item in result)
            {
                finalResponse.Add(new()
                {
                    Designation = item.designation_id,
                    Competency = item.CompetencyName,
                    RatePerHour = item.RatePerHour
                });
            }
            //List<RateDesignationDTO> response = WcgtMapper.Mapper.Map<List<RateDesignationDTO>>(result);
            return finalResponse;
        }
    }
}
