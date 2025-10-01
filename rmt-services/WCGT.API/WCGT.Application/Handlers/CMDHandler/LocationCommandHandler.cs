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
    public class LocationQuery : IRequest<List<LocationListResponse>>
    {
        public List<GTLocationDTO> locations { get; set; }
    }
    public class LocationCommandHandler : IRequestHandler<LocationQuery, List<LocationListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public LocationCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LocationListResponse>> Handle(LocationQuery request, CancellationToken cancellationToken)
        {
            List<LocationListResponse> response = new List<LocationListResponse>();

            LocationListResponse _response = null;
            foreach (var current_item in request.locations)
            {
                _response = WcgtMapper.Mapper.Map<LocationListResponse>(current_item);
                try
                {
                    Location location = WcgtMapper.Mapper.Map<Location>(current_item);
                    Location response1 = await _repository.UpdateLocation(location);
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
