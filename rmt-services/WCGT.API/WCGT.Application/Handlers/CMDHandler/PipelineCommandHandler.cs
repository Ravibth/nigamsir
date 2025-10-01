using MediatR;
using System;
using System.Collections.Generic;
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
    public class PipelineQuery : IRequest<List<PipelineListResponse>>
    {
        public List<GTPipelineDTO> pipelines { get; set; }
    }

    public class PipelineCommandHandler : IRequestHandler<PipelineQuery, List<PipelineListResponse>>
    {
        private readonly IWcgtDataRepository _repository;
        public PipelineCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<PipelineListResponse>> Handle(PipelineQuery request, CancellationToken cancellationToken)
        {
            List<PipelineListResponse> response = new();

            PipelineListResponse _response = null;
            foreach (var current_item in request.pipelines)
            {
                _response = WcgtMapper.Mapper.Map<PipelineListResponse>(current_item);
                try
                {
                    Pipeline Pipeline = WcgtMapper.Mapper.Map<Pipeline>(current_item);
                    Pipeline response1 = await _repository.UpdatePipeline(Pipeline);
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
