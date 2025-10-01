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

    public class GetHolidayListQuery: IRequest<List<GTHolidayDTO>>
    {

    }
    public class GetHolidayListQueryHandler : IRequestHandler<GetHolidayListQuery, List<GTHolidayDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetHolidayListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTHolidayDTO>> Handle(GetHolidayListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllHolidays();
            List<GTHolidayDTO> response = WcgtMapper.Mapper.Map<List<GTHolidayDTO>>(result);
            return response;
        }
    }
}
