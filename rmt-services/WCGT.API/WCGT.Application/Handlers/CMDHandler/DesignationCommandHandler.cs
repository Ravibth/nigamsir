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
    public class DesignationQuery : IRequest<List<DesignationListResponse>>
    {
        public List<GTDesignationDTO> designations { get; set; }
    }
    public class DesignationCommandHandler : IRequestHandler<DesignationQuery, List<DesignationListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public DesignationCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DesignationListResponse>> Handle(DesignationQuery request, CancellationToken cancellationToken)
        {
            List<DesignationListResponse> response = new();

            DesignationListResponse _response = null;
            foreach (var current_item in request.designations)
            {
                _response = WcgtMapper.Mapper.Map<DesignationListResponse>(current_item);
                try
                {
                    Designation designation = WcgtMapper.Mapper.Map<Designation>(current_item);
                    Designation response1 = await _repository.UpdateDesignation(designation);
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
