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

    public class GetCompetencyListQuery : IRequest<List<GTCompetencyDTO>>
    {
        public string? CompetencyId { get; set; }
        public string? CompetencyName { get; set; }
        public string? CompetencyLeaderMID { get; set; }
        public string? BuId { get; set; }
        public Boolean? isactive { get; set; }
    }

    public class GetCompetencyListQueryHandler : IRequestHandler<GetCompetencyListQuery, List<GTCompetencyDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetCompetencyListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTCompetencyDTO>> Handle(GetCompetencyListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetCompetencies(request.CompetencyId, request.CompetencyName, request.CompetencyLeaderMID, request.BuId, request.isactive);
            List<GTCompetencyDTO> response = WcgtMapper.Mapper.Map<List<GTCompetencyDTO>>(result);
            return response;
        }
    }
}
