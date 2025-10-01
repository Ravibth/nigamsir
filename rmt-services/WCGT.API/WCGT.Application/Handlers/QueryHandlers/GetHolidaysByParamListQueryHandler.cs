using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.DTO;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetHolidaysByParamListQuery:IRequest<List<GTHolidayDTO>>
    {
        public List<HolidayParamsDTO> holidayParamsDTOs {  get; set; }
    }
    public class GetHolidaysByParamListQueryHandler : IRequestHandler<GetHolidaysByParamListQuery, List<GTHolidayDTO>>
    {
        private readonly IWcgtDataRepository _wcgtDataRepository;
        public GetHolidaysByParamListQueryHandler(IWcgtDataRepository wcgtDataRepository)
        {
             _wcgtDataRepository = wcgtDataRepository;
        }
        public async Task<List<GTHolidayDTO>> Handle(GetHolidaysByParamListQuery request, CancellationToken cancellationToken)
        {
            var result = await _wcgtDataRepository.GetAllHolidaysByParams(request.holidayParamsDTOs);
            List<GTHolidayDTO> response = WcgtMapper.Mapper.Map<List<GTHolidayDTO>>(result);
            return response;
        }
    }
}
