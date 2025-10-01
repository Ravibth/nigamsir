using RMT.MarketPlace.Domain.DTOs.Response;
using RMT.MarketPlace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Domain.Repositories
{
    public interface IMarketPlaceRepository
    {
        Task<List<EmpProjectInterest>> GetAllAsync();

        Task<EmpProjectInterest?> UpdateOrCreateEmpProjectInterest(int? marketPlaceId, EmpProjectInterest entity);

        Task<List<EmpProjectInterest>> GetEmpProjectInterestList(EmpProjectInterest entity);

        Task<List<MarketPlaceProjectDetail>> GetAllMarketPlaceProjectDetail(int pagination, int limit, string? empEmail, bool? showLiked, List<string>? buFiltervalue, List<string>? offeringsFiltervalue, List<string>? solutionsFiltervalue,
            List<string>? industryFiltervalue, List<string>? subIndustryFiltervalue, List<string>? locationFiltervalue, bool? isAllocated, DateTime? startdate, DateTime? endDate, string? sortColumn, string? sortOrder, DateTime? currentDateValue);

        Task<MarketPlaceProjectDetail> AddProjectToMarketPlace(MarketPlaceProjectDetail entity);

        Task<EmpProjectInterestScore> UpdateOrCreateEmpProjectInterestScore(EmpProjectInterestScore entity);

        Task<List<EmpProjectInterestScore>> GetAllEmpProjectInterestScoreByProject(string PipelineCode, string? JobCode);

        Task<List<string>> GetAllMarketPlaceBU();

        Task<List<Offerings>> GetAllMarketPlaceOfferings();

        Task<List<Solutions>> GetAllMarketPlaceSolutions();

        Task<List<SubIndustry>> GetAllMarketPlaceSubIndustry();

        Task<List<string>> GetAllMarketPlaceLocation();

        Task<List<string>> GetAllMarketPlaceIndustry();

        Task<List<string>> GetListOfUsersInterestedByPipelineCode(string PipelineCode, string? JobCode);
        Task<MarketPlaceProjectDetail> GetMarketPlaceProjectDetails(string PipelineCode, string? JobCode);

        Task<MarketPlaceProjectDetail> GetMarketPlaceProjectById(Int64 id);

        Task<MarketPlaceProjectDetail> UpdateMarketPlaceProjectDetails(MarketPlaceProjectDetail entity);

        Task<List<MarketPlaceProjectDetail>> UpdateExpiredMarketPlaceProjects(DateTime expiryDate, int daysAdjustment);

        Task<List<MarketPlaceProjectDetail>> MarketPlaceProjectListByParams(DateOnly? marketPlacePublishDate);

        Task<List<MarketPlaceProjectDetaillsIntrestDTO>> GetMarketPlaceProjectDetailsIntrest();

    }
}
