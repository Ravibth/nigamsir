using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Mappers;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetAllProjectDetailsForMarketPlaceQuery : IRequest<List<MarketPlaceProjectDetailDTO>>
    {
        public string? emailId { get; set; }

        public int limit { get; set; }
        public int pagination { get; set; }
        public DateTime? currentDateValue { get; set; } = DateTime.UtcNow;

    }


    public class GetAllProjectDetailsForMarketPlaceQueryHandler : IRequestHandler<GetAllProjectDetailsForMarketPlaceQuery, List<MarketPlaceProjectDetailDTO>>
    {
        private readonly IMarketPlaceRepository _Repo;
        private readonly IAllocationServiceHttpApi _allocationServiceHttpApi;

        public GetAllProjectDetailsForMarketPlaceQueryHandler(IMarketPlaceRepository repository, IAllocationServiceHttpApi allocationServiceHttpApi)
        {
            _Repo = repository;
            _allocationServiceHttpApi = allocationServiceHttpApi;
        }

        public async Task<List<MarketPlaceProjectDetailDTO>> Handle(GetAllProjectDetailsForMarketPlaceQuery request, CancellationToken cancellationToken)
        {
            //var  result= await _Repo.GetAllMarketPlaceProjectDetail();
            var result = await _Repo.GetAllMarketPlaceProjectDetail(request.pagination, request.limit, "", false, null, null, null, null, null, null, null, null, null, "", "", request.currentDateValue);

            List<MarketPlaceProjectDetailDTO> obj = null;

            if (result != null)
            {
                obj = MarketPlaceMapper.Mapper.Map<List<MarketPlaceProjectDetailDTO>>(result);
                if (obj is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
            }

            //List<string> distinctProjectCode = obj?.Select(x => x.PipelineCode.ToLower()).Distinct().ToList();

            if (!string.IsNullOrEmpty(request.emailId))
            {
                var projectAllocations = await _allocationServiceHttpApi.GetAllocationsByEmail(request.emailId);

                foreach (var item in obj)
                {
                    if (projectAllocations.Where(x => x.PipelineCode.ToLower() == item.PipelineCode?.ToLower()).Any())
                        item.IsAllocated = true;
                    else
                        item.IsAllocated = false;
                }
            }

            return obj;

        }
    }
}










