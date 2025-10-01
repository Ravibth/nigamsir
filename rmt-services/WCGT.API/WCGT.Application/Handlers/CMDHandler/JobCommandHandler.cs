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
    public class JobQuery : IRequest<List<JobListResponse>>
    {
        public List<GTJobDTO> jobs { get; set; }
    }

    public class JobCommandHandler : IRequestHandler<JobQuery, List<JobListResponse>>
    {
        private readonly IWcgtDataRepository _repository;
        public JobCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<JobListResponse>> Handle(JobQuery request, CancellationToken cancellationToken)
        {
            List<JobListResponse> response = new List<JobListResponse>();

            JobListResponse _response = null;
            foreach (var current_item in request.jobs)
            {
                _response = WcgtMapper.Mapper.Map<JobListResponse>(current_item);
                try
                {
                    Job Job = WcgtMapper.Mapper.Map<Job>(current_item);
                    Job response1 = await _repository.UpdateJob(Job);
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
