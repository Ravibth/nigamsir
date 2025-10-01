using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class SectorIndustryQuery : IRequest<List<SectorIndustryListResponse>>
    {
        public List<GTSectorIndustryDTO> sectorIndustries { get; set; }
    }
    public class SectorIndustryCommandHandler : IRequestHandler<SectorIndustryQuery, List<SectorIndustryListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public SectorIndustryCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SectorIndustryListResponse>> Handle(SectorIndustryQuery request, CancellationToken cancellationToken)
        {
            List<SectorIndustryListResponse> response = new List<SectorIndustryListResponse>();

            SectorIndustryListResponse _response = null;
            foreach (var current_item in request.sectorIndustries)
            {
                _response = WcgtMapper.Mapper.Map<SectorIndustryListResponse>(current_item);
                try
                {
                    SectorIndustry sectorIndustry = WcgtMapper.Mapper.Map<SectorIndustry>(current_item);
                    SectorIndustry response1 = await _repository.UpdateSectorIndustry(sectorIndustry);
                }
                catch (Exception ex)
                {
                    _response.isfailed = true;
                    _response.failed_message = ex.Message;
                    var dataLog = Common.CreateWCGTDataLogObject(_response, current_item.GetType(), ex);
                    await _repository.AddWCGTDataLogEntry(dataLog);
                }
                response.Add(_response);
            }

            return response;
        }
    }

}
