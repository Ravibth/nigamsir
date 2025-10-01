using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetBUTreeMappingByMIDQuery : IRequest<GTBUExpertiesGroupDTO>
    {
        public string? mid { get; set; }
    }
    public class GetBUTreeMappingByMIDQueryHandler : IRequestHandler<GetBUTreeMappingByMIDQuery, GTBUExpertiesGroupDTO>
    {
        private readonly IWcgtDataRepository _repository;
        public GetBUTreeMappingByMIDQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<GTBUExpertiesGroupDTO> Handle(GetBUTreeMappingByMIDQuery request, CancellationToken cancellationToken)
        {
            List<BUTreeMapping> result = await _repository.GetAllBUTreeMappingsByMID(request.mid);
            List<GTBUTreeMappingDTO> response = WcgtMapper.Mapper.Map<List<GTBUTreeMappingDTO>>(result);
            var groupResponse = new GTBUExpertiesGroupDTO();
            groupResponse.BU = new Dictionary<string, string>();
            groupResponse.Offerings = new Dictionary<string, string>();
            groupResponse.Solutions = new Dictionary<string, string>();

            foreach (var item in response)
            {
                if (item.bu_leader_mid == request.mid)
                {
                    if (item.bu_id != null && item.bu != null && !groupResponse.BU.ContainsKey(item.bu_id))
                    {
                        groupResponse.BU.TryAdd(item.bu_id, item.bu);
                    }
                }
                if (item.bu_efficiency_leader_mid == request.mid)
                {
                    if (item.bu_id != null && item.bu != null && !groupResponse.BU.ContainsKey(item.bu_id))
                    {
                        groupResponse.BU.TryAdd(item.bu_id, item.bu);
                    }
                }
                if (item.offering_leader_mid == request.mid)
                {
                    if (item.offering_id != null && item.offering != null && !groupResponse.Offerings.ContainsKey(item.offering_id))
                    {
                        groupResponse.Offerings.TryAdd(item.offering_id, item.offering);
                    }
                }
                if (item.solution_leader_mid == request.mid)
                {
                    if (item.solution_id != null && item.solution != null && !groupResponse.Solutions.ContainsKey(item.solution_id))
                    {
                        groupResponse.Solutions.TryAdd(item.solution_id, item.solution);
                    }
                }
            }

            return groupResponse;
        }
    }
}
