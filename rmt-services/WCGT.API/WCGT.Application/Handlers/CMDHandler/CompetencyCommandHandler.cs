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

namespace WCGT.Application.Handlers.CMDHandler
{
    public class CompetencyQuery : IRequest<List<CompetencyResponse>>
    {
        public List<GTCompetencyDTO> competencyDTO { get; set; }
    }

    public class CompetencyCommandHandler : IRequestHandler<CompetencyQuery, List<CompetencyResponse>>
    {
        public readonly IWcgtDataRepository _repository;

        public CompetencyCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CompetencyResponse>> Handle(CompetencyQuery request, CancellationToken cancellationToken)
        {
            List<CompetencyResponse> response = new List<CompetencyResponse>();

            CompetencyResponse _response = null;
            if (request?.competencyDTO?.Any() == true)
            {
                foreach (var current_item in request.competencyDTO)
                {
                    _response = WcgtMapper.Mapper.Map<CompetencyResponse>(current_item);
                    try
                    {
                        Competency competency = WcgtMapper.Mapper.Map<Competency>(current_item);
                        Competency response1 = await _repository.UpdateCompetency(competency);
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
            }
            else
            {
                throw new Exception("No record to process");
            }
            return response;
        }
    }
}
