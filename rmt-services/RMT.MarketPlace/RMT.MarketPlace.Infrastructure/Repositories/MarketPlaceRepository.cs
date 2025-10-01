using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RMT.MarketPlace.Domain.DTOs.Response;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.MarketPlace.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMT.MarketPlace.Infrastructure.Repositories
{
    public class MarketPlaceRepository : IMarketPlaceRepository
    {
        protected readonly MarketPlaceDbContext _marketPlaceDbContext;

        public MarketPlaceRepository(MarketPlaceDbContext marketPlaceDbContext)
        {
            _marketPlaceDbContext = marketPlaceDbContext;
        }

        public async Task<List<EmpProjectInterest>> GetAllAsync()
        {
            return await _marketPlaceDbContext.EmpProjectInterest.ToListAsync();
        }

        public async Task<List<MarketPlaceProjectDetail>> MarketPlaceProjectListByParams(DateOnly? marketPlacePublishDate)
        {
            var result = await _marketPlaceDbContext.MarketPlaceProjectDetail
                                                           .Where(m => m.IsActive == true
                                                                      && m.IsPublishedToMarketPlace == true
                                                                      && (marketPlacePublishDate != null ? marketPlacePublishDate == DateOnly.FromDateTime((DateTime)m.MarketPlacePublishDate) : true))
                                                           .ToListAsync();
            return result;
        }

        public async Task<List<EmpProjectInterest>> GetEmpProjectInterestList(EmpProjectInterest entity)
        {
            return await _marketPlaceDbContext.EmpProjectInterest
                .Where(m => m.IsActive == true
                && m.IsInterested == true
                && m.PipelineCode != null && entity.PipelineCode != null
                && m.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                && (string.IsNullOrEmpty(m.JobCode) ||
                (!string.IsNullOrEmpty(entity.JobCode) == true && m.JobCode != null && entity.JobCode != null && m.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower())))
                .ToListAsync();
        }

        public async Task<EmpProjectInterest?> UpdateOrCreateEmpProjectInterest(int? marketPlaceId, EmpProjectInterest entity)
        {
            var _marketPlaceRecord = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(m => m.ID == marketPlaceId
            && m.IsActive == true).FirstOrDefaultAsync<MarketPlaceProjectDetail>();

            EmpProjectInterest? _marketPlace = null;

            if (_marketPlaceRecord != null)
            {
                _marketPlace = await _marketPlaceDbContext.EmpProjectInterest
                    .Where(m => m.PipelineCode != null && _marketPlaceRecord.PipelineCode != null
                    && m.PipelineCode.Trim().ToLower() == _marketPlaceRecord.PipelineCode.Trim().ToLower()
                    && (string.IsNullOrEmpty(m.JobCode) ||
                    (!string.IsNullOrEmpty(_marketPlaceRecord.JobCode) == true
                    && m.JobCode != null && _marketPlaceRecord.JobCode != null
                    && m.JobCode.Trim().ToLower() == _marketPlaceRecord.JobCode.Trim().ToLower()))
                    && m.EmpEmail != null && entity.EmpEmail != null
                    && m.EmpEmail.Trim().ToLower() == entity.EmpEmail.Trim().ToLower()
                    ).OrderByDescending(a => a.IsInterested).ThenByDescending(a => a.InterestDate)
                    .FirstOrDefaultAsync<EmpProjectInterest>();


                if (_marketPlace != null && entity.IsInterested == false)
                {
                    _marketPlace.EmpEmail = entity.EmpEmail;
                    _marketPlace.EmpName = entity.EmpName;
                    _marketPlace.PipelineCode = _marketPlaceRecord.PipelineCode;
                    _marketPlace.JobCode = _marketPlaceRecord.JobCode;
                    _marketPlace.JobName = _marketPlaceRecord.JobName;
                    _marketPlace.PipelineName = _marketPlaceRecord.PipelineName;
                    _marketPlace.InterestDate = entity.InterestDate;
                    _marketPlace.IsActive = false;
                    _marketPlace.IsInterested = entity.IsInterested;
                    _marketPlace.ModifiedBy = entity.ModifiedBy;
                    _marketPlace.ModifiedDate = DateTime.UtcNow;

                    _marketPlaceDbContext.EmpProjectInterest.Update(_marketPlace);

                    var empProjectInterestScoreItem = _marketPlaceDbContext.EmpProjectInterestScore
                        .Where(m => m.EmpProjectInterestId == _marketPlace.ID)
                        .FirstOrDefault();
                    if (empProjectInterestScoreItem != null)
                    {
                        empProjectInterestScoreItem.IsActive = false;
                        _marketPlaceDbContext.EmpProjectInterestScore.Update(empProjectInterestScoreItem);
                    }
                    await _marketPlaceDbContext.SaveChangesAsync();
                    return _marketPlace;
                }
                else
                {
                    entity.PipelineCode = _marketPlaceRecord.PipelineCode;
                    entity.JobCode = _marketPlaceRecord.JobCode;
                    entity.JobName = _marketPlaceRecord.JobName;
                    entity.PipelineName = _marketPlaceRecord.PipelineName;
                    entity.CreatedDate = DateTime.UtcNow;
                    entity.ModifiedDate = DateTime.UtcNow;
                    _marketPlaceDbContext.EmpProjectInterest.Add(entity);
                    await _marketPlaceDbContext.SaveChangesAsync();
                }
                _marketPlace = await _marketPlaceDbContext.EmpProjectInterest
                    .Where(m =>
                        m.PipelineCode != null && _marketPlaceRecord.PipelineCode != null
                        && m.PipelineCode.Trim().ToLower() == _marketPlaceRecord.PipelineCode.Trim().ToLower()
                        && (
                            string.IsNullOrEmpty(m.JobCode)
                            || (
                                !string.IsNullOrEmpty(_marketPlaceRecord.JobCode) == true
                                && m.JobCode != null
                                && _marketPlaceRecord.JobCode != null
                                && m.JobCode.Trim().ToLower() == _marketPlaceRecord.JobCode.Trim().ToLower()
                                )
                           )
                        && m.EmpEmail != null
                        && entity.EmpEmail != null
                        && m.EmpEmail.Trim().ToLower() == entity.EmpEmail.Trim().ToLower()
                        && m.IsActive == true
                     )
                    .FirstOrDefaultAsync<EmpProjectInterest>();
            }
            else
            {
                throw new Exception("Invalid marketplace record");
            }

            return _marketPlace;

        }

        public async Task<List<MarketPlaceProjectDetail>> GetAllMarketPlaceProjectDetail(int pagination, int limit, string? empEmail, bool? showLiked,
            List<string>? buFiltervalue, List<string>? offeringsFiltervalue, List<string>? solutionsFiltervalue,
            List<string>? industryFiltervalue, List<string>? subIndustryFiltervalue, List<string>? locationFiltervalue, bool? isAllocated, DateTime? startdate, DateTime? endDate,
            string? sortColumn, string? sortOrder, DateTime? currentDateValue)
        {
            if (pagination <= 0 || limit <= 0)
            {
                return new List<MarketPlaceProjectDetail> { };
            }

            DateTime currentDate;
            if (currentDateValue != null && currentDateValue.HasValue)
            {
                currentDate = currentDateValue.Value;
            }
            else
            {
                currentDate = DateTime.UtcNow.Date;
            }

            var result = (from marketPlace in _marketPlaceDbContext.MarketPlaceProjectDetail
                          where (marketPlace.IsActive == true && marketPlace.IsPublishedToMarketPlace == true
                          //&& (showLiked != true || jo.IsInterested == showLiked)
                          && ((buFiltervalue != null && buFiltervalue.Count > 0 && (!string.IsNullOrEmpty(marketPlace.BusinessUnit) && buFiltervalue.Contains(marketPlace.BusinessUnit.ToLower()))) || (buFiltervalue == null || buFiltervalue.Count == 0))
                          && ((offeringsFiltervalue != null && offeringsFiltervalue.Count > 0 && (!string.IsNullOrEmpty(marketPlace.Offerings) && offeringsFiltervalue.Contains(marketPlace.Offerings.ToLower()))) || (offeringsFiltervalue == null || offeringsFiltervalue.Count == 0))
                          && ((solutionsFiltervalue != null && solutionsFiltervalue.Count > 0 && (!string.IsNullOrEmpty(marketPlace.Solutions) && solutionsFiltervalue.Contains(marketPlace.Solutions.ToLower()))) || (solutionsFiltervalue == null || solutionsFiltervalue.Count == 0))
                          && ((industryFiltervalue != null && industryFiltervalue.Count > 0 && (!string.IsNullOrEmpty(marketPlace.Industry) && industryFiltervalue.Contains(marketPlace.Industry.ToLower()))) || (industryFiltervalue == null || industryFiltervalue.Count == 0))
                          && ((subIndustryFiltervalue != null && (!string.IsNullOrEmpty(marketPlace.Subindustry) && subIndustryFiltervalue.Contains(marketPlace.Subindustry.ToLower()))) || (subIndustryFiltervalue == null || subIndustryFiltervalue.Count == 0))
                          && ((locationFiltervalue != null && (!string.IsNullOrEmpty(marketPlace.Location) && locationFiltervalue.Contains(marketPlace.Location.ToLower()))) || (locationFiltervalue == null || locationFiltervalue.Count == 0))
                          && (startdate == null || (marketPlace.StartDate.HasValue && DateTime.Compare(marketPlace.StartDate.Value.Date, startdate.Value.Date) >= 0))
                          && (endDate == null || (marketPlace.EndDate.HasValue && DateTime.Compare(marketPlace.EndDate.Value.Date, endDate.Value.Date) <= 0))
                          //Date check added to show only records faliing with inpublished date and expiry date
                          && (marketPlace.MarketPlacePublishDate.HasValue && marketPlace.MarketPlacePublishDate.Value.Date <= currentDate.Date)
                          && (marketPlace.MarketPlaceExpirationDate.HasValue && currentDate.Date <= marketPlace.MarketPlaceExpirationDate.Value.Date)
                          )
                          select new MarketPlaceProjectDetail
                          {
                              ID = marketPlace.ID,
                              PipelineCode = marketPlace.PipelineCode,
                              PipelineName = marketPlace.PipelineName,
                              JobCode = marketPlace.JobCode,
                              JobName = marketPlace.JobName,
                              ClientName = marketPlace.ClientName,
                              ClientGroup = marketPlace.ClientGroup,
                              StartDate = marketPlace.StartDate,
                              EndDate = marketPlace.EndDate,
                              Description = marketPlace.Description,
                              IsPublishedToMarketPlace = marketPlace.IsPublishedToMarketPlace,
                              MarketPlacePublishDate = marketPlace.MarketPlacePublishDate,
                              CreatedBy = marketPlace.CreatedBy,
                              ModifiedBy = marketPlace.ModifiedBy,
                              IsActive = marketPlace.IsActive,
                              JsonData = marketPlace.JsonData,
                              ChargableType = marketPlace.ChargableType,
                              Location = marketPlace.Location,
                              BusinessUnit = marketPlace.BusinessUnit,

                              BUId = marketPlace.BUId,
                              Offerings = marketPlace.Offerings,
                              OfferingsId = marketPlace.OfferingsId,
                              Solutions = marketPlace.Solutions,
                              SolutionsId = marketPlace.SolutionsId,

                              Industry = marketPlace.Industry,
                              Subindustry = marketPlace.Subindustry,
                              Csp = marketPlace.Csp,
                              ProposedCsp = marketPlace.ProposedCsp,
                              ElForJob = marketPlace.ElForJob,
                              ElForPipeLine = marketPlace.ElForPipeLine,
                              JobManager = marketPlace.JobManager,
                              //IsInterested = jo.EmpEmail.ToLower().Trim() == empEmail.ToLower().Trim() && jo.IsInterested == true,//jo != null ? true : false,
                              MarketPlaceExpirationDate = marketPlace.MarketPlaceExpirationDate,
                              IspipeLine = marketPlace.IspipeLine,
                          }).Distinct().AsQueryable().OrderByDescending(x => x.ID)
                           //.ThenBy(x => x.ID)
                           .ToList<MarketPlaceProjectDetail>();

            var resultList = await Task.FromResult(result);


            var resultEmpInterest = await (from epi in _marketPlaceDbContext.EmpProjectInterest
                                           where epi.IsActive == true && epi.IsInterested == true
                                           && !string.IsNullOrEmpty(epi.EmpEmail) && !string.IsNullOrEmpty(empEmail)
                                           && epi.EmpEmail.ToLower().Trim() == empEmail.ToLower().Trim()
                                           select epi
                                        ).ToListAsync<EmpProjectInterest>();

            if (resultList != null && resultList.Count > 0)
            {
                if (resultEmpInterest != null && resultEmpInterest.Count > 0)
                {
                    foreach (var item in resultList)
                    {
                        item.IsInterested = resultEmpInterest.Any(ei => Convert.ToString(ei.PipelineCode + "--" + ei.JobCode).ToLower().Trim() == Convert.ToString(item.PipelineCode + "--" + item.JobCode).ToLower().Trim());
                    }
                }

                if (showLiked != null)
                {
                    resultList = resultList.Where(a => (a.IsInterested == showLiked && showLiked == true) || (showLiked != true)).ToList();
                }

                if (sortOrder == "desc")
                {
                    resultList = resultList.OrderByDescending(x => sortColumn == "marketPlaceExpirationDate" ? x.MarketPlaceExpirationDate : x.MarketPlacePublishDate).ToList<MarketPlaceProjectDetail>();
                }
                else
                {
                    resultList = resultList.OrderBy(x => sortColumn == "marketPlaceExpirationDate" ? x.MarketPlaceExpirationDate : x.MarketPlacePublishDate).ToList<MarketPlaceProjectDetail>();
                }

            }

            return resultList;

        }

        public async Task<MarketPlaceProjectDetail> AddProjectToMarketPlace(MarketPlaceProjectDetail entity)
        {
            try
            {
                var _marketPlace = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(m =>
                m.PipelineCode != null && entity.PipelineCode != null
                && m.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                && (string.IsNullOrEmpty(m.JobCode) ||
                (!string.IsNullOrEmpty(entity.JobCode) == true
                && m.JobCode != null && entity.JobCode != null
                && m.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))
                ).FirstOrDefaultAsync<MarketPlaceProjectDetail>();
                if (_marketPlace != null)
                {
                    _marketPlace.PipelineCode = entity.PipelineCode;
                    _marketPlace.PipelineName = entity.PipelineName;
                    _marketPlace.JobCode = entity.JobCode;
                    _marketPlace.JobName = entity.JobName;
                    _marketPlace.ClientName = entity.ClientName;
                    _marketPlace.ClientGroup = entity.ClientGroup;
                    _marketPlace.StartDate = entity.StartDate.Value.Date;
                    _marketPlace.EndDate = entity.EndDate.Value.Date;
                    _marketPlace.Description = entity.Description;
                    _marketPlace.IsPublishedToMarketPlace = entity.IsPublishedToMarketPlace;
                    _marketPlace.MarketPlacePublishDate = DateTime.UtcNow.ToLocalTime();
                    _marketPlace.CreatedDate = DateTime.UtcNow.ToLocalTime();
                    _marketPlace.CreatedBy = entity.CreatedBy;
                    _marketPlace.ModifiedDate = DateTime.UtcNow;
                    _marketPlace.ModifiedBy = entity.ModifiedBy;
                    _marketPlace.IsActive = entity.IsActive;
                    _marketPlace.JsonData = entity.JsonData;
                    _marketPlace.ChargableType = entity.ChargableType;
                    _marketPlace.Location = entity.Location;
                    _marketPlace.BusinessUnit = entity.BusinessUnit;

                    _marketPlace.BUId = entity.BUId;
                    _marketPlace.Offerings = entity.Offerings;
                    _marketPlace.OfferingsId = entity.OfferingsId;
                    _marketPlace.Solutions = entity.Solutions;
                    _marketPlace.SolutionsId = entity.SolutionsId;

                    _marketPlace.Industry = entity.Industry;
                    _marketPlace.Subindustry = entity.Subindustry;
                    _marketPlace.Csp = entity.Csp;
                    _marketPlace.ProposedCsp = entity.ProposedCsp;
                    _marketPlace.ElForJob = entity.ElForJob;
                    _marketPlace.ElForPipeLine = entity.ElForPipeLine;
                    _marketPlace.JobManager = entity.JobManager;
                    _marketPlace.IsInterested = entity.IsInterested;
                    _marketPlace.MarketPlaceExpirationDate = entity.MarketPlaceExpirationDate.Value.Date;
                    _marketPlace.IspipeLine = entity.IspipeLine;
                    _marketPlaceDbContext.MarketPlaceProjectDetail.Update(_marketPlace);
                    await _marketPlaceDbContext.SaveChangesAsync();
                }
                else
                {
                    entity.MarketPlaceExpirationDate = entity.MarketPlaceExpirationDate.Value.Date;
                    entity.MarketPlacePublishDate = DateTime.UtcNow.ToLocalTime();
                    entity.CreatedDate = DateTime.UtcNow.ToLocalTime();

                    await _marketPlaceDbContext.Set<MarketPlaceProjectDetail>().AddAsync(entity);
                    await _marketPlaceDbContext.SaveChangesAsync();
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("unable to push changes to DB", ex);
            }
        }

        public async Task<EmpProjectInterestScore> UpdateOrCreateEmpProjectInterestScore(EmpProjectInterestScore entity)
        {
            var _empProjectInterestScore = await _marketPlaceDbContext.EmpProjectInterestScore
                .Where(m => m.EmpProjectInterestId == entity.EmpProjectInterestId
                && m.RequisitionId == entity.RequisitionId).FirstOrDefaultAsync<EmpProjectInterestScore>();
            if (_empProjectInterestScore != null)
            {
                _empProjectInterestScore.EmpProjectInterestId = entity.EmpProjectInterestId;
                _empProjectInterestScore.RequisitionId = entity.RequisitionId;
                _empProjectInterestScore.RequisitionDesignation = entity.RequisitionDesignation;
                _empProjectInterestScore.RequisitionGrade = entity.RequisitionGrade;
                _empProjectInterestScore.RequisitionBU = entity.RequisitionBU;
                _empProjectInterestScore.RequisitionOfferings = entity.RequisitionOfferings;
                _empProjectInterestScore.RequisitionSolutions = entity.RequisitionSolutions;
                _empProjectInterestScore.RequisitionCompetency = entity.RequisitionCompetency;
                _empProjectInterestScore.RequisionScore = entity.RequisionScore;
                _empProjectInterestScore.IsActive = entity.IsActive;
                _empProjectInterestScore.IsInterested = entity.IsInterested;
                _empProjectInterestScore.CreatedDate = DateTime.UtcNow;
                //_empProjectInterestScore.CreatedBy = entity.CreatedBy;
                _empProjectInterestScore.ModifiedBy = entity.ModifiedBy;
                _empProjectInterestScore.Suggestion = entity.Suggestion;
                _empProjectInterestScore.RequisitionParameters = entity.RequisitionParameters;
                _empProjectInterestScore.ModifiedDate = DateTime.UtcNow;
                _marketPlaceDbContext.EmpProjectInterestScore.Update(_empProjectInterestScore);
                await _marketPlaceDbContext.SaveChangesAsync();
            }
            else
            {
                await _marketPlaceDbContext.Set<EmpProjectInterestScore>().AddAsync(entity);
                await _marketPlaceDbContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<List<EmpProjectInterestScore>> GetAllEmpProjectInterestScoreByProject(string PipelineCode, string? JobCode)
        {
            //get score for intrested and active records only
            var result = (from empInterest in _marketPlaceDbContext.EmpProjectInterest
                          join empInterestScore in _marketPlaceDbContext.EmpProjectInterestScore
                          on empInterest.ID equals empInterestScore.EmpProjectInterestId into gj
                          from x in gj.DefaultIfEmpty()
                          where empInterest.PipelineCode != null && PipelineCode != null
                          && empInterest.PipelineCode.Trim().ToLower() == PipelineCode.Trim().ToLower()
                          && (string.IsNullOrEmpty(empInterest.JobCode) ||
                          (!string.IsNullOrEmpty(JobCode) == true &&
                          empInterest.JobCode != null && JobCode != null &&
                          empInterest.JobCode.Trim().ToLower() == JobCode.Trim().ToLower()))
                          && empInterest.IsInterested == true && empInterest.IsActive == true
                          && x.IsActive == true
                          select new EmpProjectInterestScore
                          {
                              ID = x.ID,
                              EmpProjectInterestId = empInterest.ID,
                              RequisitionId = x == null ? string.Empty : x.RequisitionId,
                              RequisitionDesignation = x == null ? string.Empty : x.RequisitionDesignation,
                              RequisitionGrade = x == null ? string.Empty : x.RequisitionGrade,
                              RequisitionBU = x == null ? string.Empty : x.RequisitionBU,
                              RequisitionOfferings = x == null ? string.Empty : x.RequisitionOfferings,
                              RequisitionSolutions = x == null ? string.Empty : x.RequisitionSolutions,
                              RequisitionCompetency = x == null ? string.Empty : x.RequisitionCompetency,
                              RequisionScore = x == null ? string.Empty : x.RequisionScore,
                              Suggestion = x == null ? string.Empty : x.Suggestion,
                              RequisitionParameters = x == null ? string.Empty : x.RequisitionParameters,
                              IsInterested = empInterest.IsInterested,
                              EmpName = empInterest.EmpName,
                              EmpEmail = empInterest.EmpEmail,
                              IsActive = empInterest.IsActive,
                              CreatedDate = x == null ? empInterest.CreatedDate : x.CreatedDate,
                              CreatedBy = x == null ? empInterest.CreatedBy : x.CreatedBy,
                              ModifiedDate = x == null ? empInterest.ModifiedDate : x.ModifiedDate,
                              ModifiedBy = x == null ? empInterest.ModifiedBy : x.ModifiedBy,
                              EmpProjectInterest = empInterest

                          }).ToList<EmpProjectInterestScore>();
            //return await _marketPlaceDbContext.EmpProjectInterestScore
            //    .Include(e => e.EmpProjectInterest)
            //    .Where(e => e.EmpProjectInterest.PipelineCode.Trim().ToLower() == PipelineCode.Trim().ToLower()
            //                && ((string.IsNullOrEmpty(e.EmpProjectInterest.JobCode) && string.IsNullOrEmpty(JobCode)) ||
            //                (!string.IsNullOrEmpty(JobCode) && e.EmpProjectInterest.JobCode.Trim().ToLower() == JobCode.Trim().ToLower()))
            //                && e.IsActive == true).ToListAsync();

            return await Task.FromResult(result);
        }

        public async Task<List<string>> GetAllMarketPlaceBU()
        {
            var result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.BusinessUnit) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => x.BusinessUnit).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<Offerings>> GetAllMarketPlaceOfferings()
        {
            List<Offerings> result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.Offerings) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => new Offerings { BU = x.BusinessUnit, Name = x.Offerings }).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<Solutions>> GetAllMarketPlaceSolutions()
        {
            List<Solutions> result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.Solutions) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => new Solutions { Offerings = x.Offerings, Name = x.Solutions }).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<SubIndustry>> GetAllMarketPlaceSubIndustry()
        {
            List<SubIndustry> result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.Subindustry) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => new SubIndustry { Industry = x.Industry, Name = x.Subindustry }).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<string>> GetAllMarketPlaceLocation()
        {
            var result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.Location) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => x.Location).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<string>> GetAllMarketPlaceIndustry()
        {
            var result = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(x => string.IsNullOrEmpty(x.Industry) == false
            && x.IsPublishedToMarketPlace == true && x.IsActive == true).Select(x => x.Industry).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<string>> GetListOfUsersInterestedByPipelineCode(string PipelineCode, string? JobCode)
        {
            var interests = await _marketPlaceDbContext.EmpProjectInterest
                .Where(m => m.PipelineCode != null && PipelineCode != null
                    && m.PipelineCode.Trim().ToLower().Equals(PipelineCode.ToLower().Trim())
                    && (string.IsNullOrEmpty(m.JobCode) ||
                    (!string.IsNullOrEmpty(JobCode) == true &&
                     m.JobCode != null && JobCode != null &&
                     m.JobCode.Trim().ToLower() == JobCode.Trim().ToLower()))
                    && m.IsInterested == true
                    && m.IsActive == true
                ).ToListAsync();
            return interests.Where(x => x.EmpEmail != null).Select(m => m.EmpEmail + string.Empty).ToList();
        }

        public async Task<MarketPlaceProjectDetail> GetMarketPlaceProjectDetails (string PipelineCode, string? JobCode)
        {
            var result = await _marketPlaceDbContext.MarketPlaceProjectDetail
            .Where(m => m.PipelineCode != null && PipelineCode != null
                && m.PipelineCode.Trim().ToLower().Equals(PipelineCode.ToLower().Trim())
                && (string.IsNullOrEmpty(m.JobCode) ||
                    (!string.IsNullOrEmpty(JobCode) == true &&
                     m.JobCode != null && JobCode != null &&
                     m.JobCode.Trim().ToLower() == JobCode.Trim().ToLower()))
                && m.IsActive == true
            )
            .OrderByDescending(a => a.ModifiedDate)
            .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<MarketPlaceProjectDetaillsIntrestDTO>> GetMarketPlaceProjectDetailsIntrest()
        {
            var query = _marketPlaceDbContext.MarketPlaceProjectDetail
                .Join(
                      _marketPlaceDbContext.EmpProjectInterest,
                      projectDetail => new { projectDetail.PipelineCode, projectDetail.JobCode },
                      empInterest => new { empInterest.PipelineCode, empInterest.JobCode },
                      (projectDetail, empInterest) => new
                      {
                          ProjectDetail = projectDetail,
                          EmpInterest = empInterest
                      }
                      )
                .GroupBy(
                        result => result.ProjectDetail,
                        result => result.EmpInterest,
                        (projectDetail, empInterests) => new MarketPlaceProjectDetaillsIntrestDTO
                        {
                            ID = projectDetail.ID,
                            JobCode = projectDetail.JobCode,
                            JobName = projectDetail.JobName,
                            PipelineCode = projectDetail.PipelineCode,
                            PipelineName = projectDetail.PipelineName,
                            ClientName = projectDetail.ClientName,
                            ClientGroup = projectDetail.ClientGroup,
                            StartDate = projectDetail.StartDate,
                            EndDate = projectDetail.EndDate,
                            Description = projectDetail.Description,
                            IsPublishedToMarketPlace = projectDetail.IsPublishedToMarketPlace,
                            MarketPlacePublishDate = projectDetail.MarketPlacePublishDate,
                            CreatedDate = projectDetail.CreatedDate,
                            CreatedBy = projectDetail.CreatedBy,
                            ModifiedDate = projectDetail.ModifiedDate,
                            ModifiedBy = projectDetail.ModifiedBy,
                            IsActive = projectDetail.IsActive,
                            JsonData = projectDetail.JsonData,
                            ChargableType = projectDetail.ChargableType,
                            Location = projectDetail.Location,
                            BusinessUnit = projectDetail.BusinessUnit,

                            BUId = projectDetail.BUId,
                            Offerings = projectDetail.Offerings,
                            OfferingsId = projectDetail.OfferingsId,
                            Solutions = projectDetail.Solutions,
                            SolutionsId = projectDetail.SolutionsId,

                            Industry = projectDetail.Industry,
                            Subindustry = projectDetail.Subindustry,
                            Csp = projectDetail.Csp,
                            ProposedCsp = projectDetail.ProposedCsp,
                            ElForJob = projectDetail.ElForJob,
                            ElForPipeLine = projectDetail.ElForPipeLine,
                            JobManager = projectDetail.JobManager,
                            IsInterested = empInterests.Any(e => e.IsInterested == true), // Check if any employee is interested
                            IsAllocated = empInterests.Any(e => e.IsActive == true), // Check if any employee is allocated
                            MarketPlaceExpirationDate = projectDetail.MarketPlaceExpirationDate,
                            IspipeLine = projectDetail.IspipeLine,
                            EmployeeInterest = empInterests.ToList() // Assign the list of employee interests
                        }
                        ).Where(project => project.IsActive == true && project.EndDate > DateTime.Now);


            return await query.ToListAsync();
        }

        public async Task<MarketPlaceProjectDetail> GetMarketPlaceProjectById(Int64 id)
        {
            var _marketPlace = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(m => m.ID == id).FirstOrDefaultAsync<MarketPlaceProjectDetail>();
            return _marketPlace;
        }

        public async Task<MarketPlaceProjectDetail> UpdateMarketPlaceProjectDetails(MarketPlaceProjectDetail entity)
        {
            try
            {
                var _marketPlace = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(m =>
                m.PipelineCode != null && entity.PipelineCode != null
                && m.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                && (string.IsNullOrEmpty(m.JobCode) ||
                (!string.IsNullOrEmpty(entity.JobCode) == true
                && m.JobCode != null && entity.JobCode != null
                && m.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))
                && m.IsActive == true
                ).FirstOrDefaultAsync<MarketPlaceProjectDetail>();

                if (_marketPlace != null)
                {
                    _marketPlace.PipelineCode = entity.PipelineCode;
                    _marketPlace.JobCode = entity.JobCode;

                    //Update value of entity if new value is not null else keep old value
                    _marketPlace.JobName = entity.JobName == null ? _marketPlace.JobName : entity.JobName;
                    _marketPlace.PipelineName = entity.PipelineName == null ? _marketPlace.PipelineName : entity.PipelineName;
                    _marketPlace.ClientName = entity.ClientName == null ? _marketPlace.ClientName : entity.ClientName;
                    _marketPlace.ClientGroup = entity.ClientGroup == null ? _marketPlace.ClientGroup : entity.ClientGroup;
                    _marketPlace.StartDate = entity.StartDate == null ? _marketPlace.StartDate : entity.StartDate;
                    _marketPlace.EndDate = entity.EndDate == null ? _marketPlace.EndDate : entity.EndDate;
                    _marketPlace.Description = entity.Description == null ? _marketPlace.Description : entity.Description;
                    _marketPlace.ModifiedDate = DateTime.UtcNow;
                    _marketPlace.ModifiedBy = entity.ModifiedBy;

                    _marketPlace.IsActive = entity.IsActive == null ? _marketPlace.IsActive : entity.IsActive;
                    _marketPlace.ChargableType = entity.ChargableType == null ? _marketPlace.ChargableType : entity.ChargableType;
                    _marketPlace.Location = entity.Location == null ? _marketPlace.Location : entity.Location;
                    _marketPlace.BusinessUnit = entity.BusinessUnit == null ? _marketPlace.BusinessUnit : entity.BusinessUnit;

                    _marketPlace.BUId = entity.BUId == null ? _marketPlace.BUId : entity.BUId;
                    _marketPlace.Offerings = entity.Offerings == null ? _marketPlace.Offerings : entity.Offerings;
                    _marketPlace.OfferingsId = entity.OfferingsId == null ? _marketPlace.OfferingsId : entity.OfferingsId;
                    _marketPlace.Solutions = entity.Solutions == null ? _marketPlace.Solutions : entity.Solutions;
                    _marketPlace.SolutionsId = entity.SolutionsId == null ? _marketPlace.SolutionsId : entity.SolutionsId;

                    _marketPlace.Industry = entity.Industry == null ? _marketPlace.Industry : entity.Industry;
                    _marketPlace.Subindustry = entity.Subindustry == null ? _marketPlace.Subindustry : entity.Subindustry;
                    _marketPlaceDbContext.MarketPlaceProjectDetail.Update(_marketPlace);
                    await _marketPlaceDbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine(string.Format("Project does not exist in marketplace,PipelineCode-{0},JobCode-{1}", entity.PipelineCode, entity.JobCode));
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to update data to DB, see inner exception.", ex);
            }
        }

        public async Task<List<MarketPlaceProjectDetail>> UpdateExpiredMarketPlaceProjects(DateTime expiryDate, int daysAdjustment)
        {
            try
            {
                //optional flag to set adjust based on timezone
                expiryDate = expiryDate.AddDays(daysAdjustment);

                List<MarketPlaceProjectDetail> updateMarketplaceProject = new List<MarketPlaceProjectDetail>();

                //get all currently published project having expiray less than today date
                var expiredMPProjects = await _marketPlaceDbContext.MarketPlaceProjectDetail.Where(m =>
                                        m.IsActive == true && m.IsPublishedToMarketPlace == true
                                        && m.MarketPlaceExpirationDate.HasValue && m.MarketPlaceExpirationDate.Value.Date <= expiryDate.Date
                                        ).ToListAsync();

                if (expiredMPProjects != null && expiredMPProjects.Count > 0)
                {
                    foreach (var mpProject in expiredMPProjects)
                    {
                        try
                        {
                            mpProject.IsActive = false;
                            mpProject.IsPublishedToMarketPlace = false;
                            mpProject.ModifiedDate = DateTime.UtcNow;
                            await _marketPlaceDbContext.SaveChangesAsync();
                        }
                        catch (Exception innerEx)
                        {
                            mpProject.IsPublishedToMarketPlace = true;
                        }
                        finally
                        {
                            updateMarketplaceProject.Add(mpProject);
                        }
                    }
                }

                return updateMarketplaceProject;

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to update data to DB, see inner exception.", ex);
            }
        }

    }
}
