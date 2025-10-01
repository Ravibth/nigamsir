using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.Migrations;
using RMT.Allocation.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RMT.Allocation.Domain.ConstantsDomain;
using static RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Infrastructure.Repositories
{
    public class RequisitionRepository : IRequisitionRepository
    {

        protected readonly AllocationDbContext _allocationDbContext;
        private readonly IConfiguration _configuration;

        public RequisitionRepository(AllocationDbContext allocationDbContext, IConfiguration configuration)
        {
            _allocationDbContext = allocationDbContext;
            _configuration = configuration;
        }

        public async Task<Requisition> GetRequisitionDetailsByRequisitionId(Guid requisitionId, bool filterZeroWeightageParameters = false)
        {
            var requisitionDetails =
            await _allocationDbContext.Requisition
                .Include(m => m.RequisitionParameterValues)
                .Include(m => m.RequisitionParameters)
                .Include(m => m.RequisitionSkill)
                .Include(m => m.RequisitionType)
                .Include(m => m.demands)
                .Where(m => m.Id == requisitionId && m.IsActive == true)
                .FirstOrDefaultAsync();

            if (requisitionDetails != null && requisitionDetails.demands != null)
            {
                requisitionDetails.demands.Requisitions = await _allocationDbContext.Requisition
                    .Where(m =>
                        m.RequisitionDemand == requisitionDetails.RequisitionDemand
                        && m.RequisitionStatus.ToLower() == RequisitionStatuses.PENDING.ToLower()
                        && m.IsActive == true
                        && (m.RequisitionType.Type == RequisitionTypeData.CreateRequisition || m.RequisitionType.Type == RequisitionTypeData.BulkRequisition)
                     )
                    .ToListAsync();

                if (filterZeroWeightageParameters)
                {
                    requisitionDetails.RequisitionParameters = requisitionDetails?.RequisitionParameters
                        .Where(m =>
                            m.RequisitionWeight != null
                            && m.RequisitionWeight > 0
                            && m.IsChecked == true
                         )
                        .ToList();
                }
            }

            return requisitionDetails;
        }



        public async Task<List<SystemSuggestionResponseDTO>> GetSystemSuggestions(int limit, int pagination, double pref_weightage_constraint, List<EmployeeDetailsDTO> employeeDetails, Requisition requisitionDetails, Guid requisitionId, int minimumPercentageForSystemSuggestions, Object[] filter, string[] parameter_value_pairs, string orderScoreBy)
        {
            List<SystemSuggestionResponseDTO> suggestions = new List<SystemSuggestionResponseDTO>() { };

            NpgsqlConnection npgsqlConnection = null;
            try
            {
                //create a constant file to store all static data like sp name with their params as well
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();

                npgsqlConnection = new NpgsqlConnection(pgsqlConnection);
                using (NpgsqlCommand command = new NpgsqlCommand(Constants.SystemSuggestionsSP, npgsqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.limit_count, limit);
                    command.Parameters.AddWithValue(Constants.pagination, pagination);
                    command.Parameters.AddWithValue(Constants.minimumPercentageForSystemSuggestions, minimumPercentageForSystemSuggestions);
                    command.Parameters.AddWithValue(Constants.orderScoreBy, orderScoreBy);

                    var pref_weightage_constraintParam = new NpgsqlParameter(Constants.pref_weightage_constraint, NpgsqlDbType.Real)
                    {
                        Value = pref_weightage_constraint,
                    };
                    command.Parameters.Add(pref_weightage_constraintParam);
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true
                    };
                    var requisitionDetailsParam = new NpgsqlParameter(Constants.requisition_details, NpgsqlDbType.Jsonb)
                    {
                        Value = System.Text.Json.JsonSerializer.Serialize(requisitionDetails, options),
                    };
                    command.Parameters.Add(requisitionDetailsParam);

                    var employeesParam = new NpgsqlParameter(Constants.employee_details, NpgsqlDbType.Jsonb)
                    {
                        Value = employeeDetails,
                    };
                    command.Parameters.Add(employeesParam);

                    var filtersParam = new NpgsqlParameter(Constants.filter, NpgsqlDbType.Jsonb)
                    {
                        Value = filter
                    };


                    command.Parameters.Add(filtersParam);

                    var parameter_value_pairs_params = new NpgsqlParameter(Constants.parameter_value_pairs, NpgsqlDbType.Text | NpgsqlDbType.Array)
                    {
                        Value = parameter_value_pairs
                    };
                    command.Parameters.Add(parameter_value_pairs_params);


                    command.Parameters.AddWithValue(Constants.requisition_id, requisitionId);

                    var outputResult = new NpgsqlParameter(Constants.SystemSuggestionsSPResponse, NpgsqlDbType.Json);
                    outputResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputResult);

                    npgsqlConnection.Open();
                    command.ExecuteNonQuery();
                    var jsonResult = command.Parameters[Constants.SystemSuggestionsSPResponse].Value.ToString();

                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        JArray jsonArray = JArray.Parse(jsonResult);

                        if (jsonArray != null)
                        {
                            foreach (var json in jsonArray)
                            {
                                var suggestionItem = JsonConvert.DeserializeObject<SystemSuggestionResponseDTO>(Convert.ToString(json));
                                suggestions.Add(suggestionItem);
                            }
                        }
                    }
                    npgsqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (npgsqlConnection != null)
                {
                    npgsqlConnection.Close();
                }

            }
            return suggestions;
        }

        public async Task<List<Requisition>> GetAllRequisitionByProjectCode(string pipelineCode, string? jobCode)
        {
            List<Requisition> result = await _allocationDbContext.Requisition
                                        .Where(m =>
                                            m.PipelineCode.ToLower() == pipelineCode.ToLower()
                                            && m.IsActive == true
                                            && (string.IsNullOrEmpty(m.JobCode) ||
                                            (!string.IsNullOrEmpty(jobCode) == true
                                                && Convert.ToString(m.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower())
                                            ))
                                        .Include(m => m.RequisitionParameters)
                                        .Include(m => m.RequisitionSkill)
                                        .Include(m => m.RequisitionParameterValues)
                                        .Include(m => m.RequisitionType)
                                        .Include(m => m.demands)
                                        .ToListAsync();
            foreach (var requisition in result)
            {
                if (requisition.demands != null)
                {
                    requisition.demands.Requisitions = requisition.demands.Requisitions
                        .Where(r => r.RequisitionStatus?.ToLower() == RequisitionStatuses.PENDING.ToLower() && r.IsActive == true
                        && (r.RequisitionType.Type == RequisitionTypeData.CreateRequisition || r.RequisitionType.Type == RequisitionTypeData.BulkRequisition)
                        )
                        .ToList();
                }
                requisition.RequisitionParameters = requisition.RequisitionParameters.ToList();
            }
            return result
                .OrderByDescending(d => d.ModifiedAt)
                .Where(a =>
                    a.IsActive == true
                    && a.RequisitionStatus?.ToLower() == RequisitionStatuses.PENDING.ToLower()
                )
                .ToList();
        }

        public async Task<List<Requisition>> GetAllRequisitionByProjectCodeForProjectDetails(string pipelineCode, string? jobCode)
        {
            List<Requisition> result = await _allocationDbContext.Requisition
                                        .Where(m =>
                                            m.PipelineCode.ToLower() == pipelineCode.ToLower()
                                            && m.IsActive == true
                                            && (string.IsNullOrEmpty(m.JobCode) ||
                                            (!string.IsNullOrEmpty(jobCode) == true
                                                && Convert.ToString(m.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower())
                                            ))
                                        .Include(m => m.RequisitionParameters)
                                        .Include(m => m.RequisitionSkill)
                                        .Include(m => m.RequisitionParameterValues)
                                        .Include(m => m.RequisitionType)
                                        .Include(m => m.demands)
                                        .ToListAsync();
            foreach (var requisition in result)
            {
                if (requisition.demands != null)
                {
                    requisition.demands.Requisitions = requisition.demands.Requisitions
                        .Where(r => r.RequisitionStatus?.ToLower() == RequisitionStatuses.PENDING.ToLower() && r.IsActive == true
                        && (r.RequisitionType.Type == RequisitionTypeData.CreateRequisition || r.RequisitionType.Type == RequisitionTypeData.BulkRequisition)
                        )
                        .ToList();
                }
                requisition.RequisitionParameters = requisition.RequisitionParameters.ToList();
            }
            return result.OrderByDescending(d => d.ModifiedAt).Where(a => a.IsActive == true).ToList();
        }

        public async Task<Requisition> DeleteRequisitionById(Guid requisitionId)
        {
            var requisitionData = await GetRequisitionDetailsByRequisitionId(requisitionId);
            if (requisitionData is null)
            {
                throw new Exception("Data not Found");
            }
            requisitionData.IsActive = false;
            RequisitionDemand requisitionDemand = await _allocationDbContext.RequisitionDemand
                .Where(m => m.Id == requisitionData.RequisitionDemand)
                .FirstOrDefaultAsync();

            if (requisitionDemand != null)
            {
                requisitionDemand.TotalDemands = requisitionDemand.TotalDemands - 1;
                if (requisitionData.RequisitionStatus.ToLower() == RequisitionStatuses.PENDING.ToLower())
                {
                    requisitionDemand.PendingDemands--;
                }
                _allocationDbContext.RequisitionDemand.Update(requisitionDemand);
            }
            var requisitionResult = _allocationDbContext.Requisition.Update(requisitionData);

            await _allocationDbContext.SaveChangesAsync();
            return requisitionResult.Entity;
        }

        public async Task<List<Requisition>> AddRequisitionAsync(RequisitionRequest entity)
        {
            var reqType = await _allocationDbContext.RequisitionType
                .Where(m => m.Type.Trim().ToLower() == RequisitionTypeData.CreateRequisition.Trim().ToLower())
                .FirstOrDefaultAsync();

            if (reqType != null)
            {
                var reqDemand = (await _allocationDbContext.Set<RequisitionDemand>().AddAsync(
                    new RequisitionDemand
                    {
                        TotalDemands = entity.NumberOfResources,
                        PendingDemands = entity.NumberOfResources,
                        AllResourcesHaveSameDetails = entity.IsAllResourcesSimilar,
                    }))
                    .Entity;

                await _allocationDbContext.SaveChangesAsync();
                entity.RequisitionTypeId = (int)reqType.Id;
                if (!entity.IsAllResourcesSimilar)
                {
                    return await AddRequisitionAsyncNonSmillarResource(entity, reqDemand);
                }
                else
                {
                    return await AddRequisitionAsyncSmillarResource(entity, reqDemand);
                }
            }
            return null;
        }
        public async Task<List<Requisition>> AddRequisitionAsyncSmillarResource(RequisitionRequest entity, RequisitionDemand reqDemand)
        {
            try
            {
                var _req = new List<Requisition> { };
                /************ Update Project Details ******************************/
                for (int idx = 0; idx < entity.NumberOfResources; idx++)
                {
                    Requisition req = new()
                    {
                        PipelineCode = entity.PipelineCode,
                        JobCode = entity.JobCode,
                        PipelineName = entity.PipelineName,
                        JobName = entity.JobName,
                        Description = entity.Description,
                        Designation = entity.Designation,
                        Grade = entity.Grade,
                        EffortsPerDay = entity.resourceEntities[0].Effort_Hrs,
                        StartDate = DateOnly.FromDateTime(entity.resourceEntities[0].StartDate),
                        EndDate = DateOnly.FromDateTime(entity.resourceEntities[0].EndDate),
                        IsPerDayHourAllocation = entity.resourceEntities[0].IsPerDayHourAllocation,
                        //Expertise = entity.Expertise,
                        //SMEG = entity.SMEG,
                        Competency = entity.resourceEntities.FirstOrDefault().Competency,
                        CompetencyId = entity.resourceEntities.FirstOrDefault().CompetencyId,
                        //TODO Org Structure
                        BUId = "1",
                        OfferingsId = "1",
                        SolutionsId = "1",
                        Offerings = entity.Offerings,
                        Solutions = entity.Solutions,
                        BusinessUnit = entity.BusinessUnit,
                        TotalHours = (bool)entity.resourceEntities[0].IsPerDayHourAllocation ? CommonUtil.totalDaysPerDaysWise(entity.resourceEntities[0].StartDate, entity.resourceEntities[0].EndDate, entity.resourceEntities[0].Effort_Hrs) : entity.resourceEntities[0].Effort_Hrs,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        ModifiedAt = entity.ModifiedAt,
                        ModifiedBy = entity.ModifiedBy,
                        IsActive = true,
                        RequisitionDemand = reqDemand.Id,
                        RequisitionStatus = RequisitionStatuses.PENDING,
                        ClientName = entity.ClientName,
                        RequisitionTypeId = entity.RequisitionTypeId,
                    };
                    var response = (await _allocationDbContext.Set<Requisition>().AddAsync(req)).Entity;
                    await _allocationDbContext.SaveChangesAsync();
                    Guid requisitionId = response.Id;
                    _req.Add(response);
                    if (entity.resourceEntities[0].Parameters != null && entity.resourceEntities[0].Parameters.Count > 0)
                    {
                        await AddParametersAsync(entity.resourceEntities[0].Parameters, requisitionId);
                    }
                    if (entity.resourceEntities[0].Skills != null && entity.resourceEntities[0].Skills.Count > 0)
                    {
                        await AddSkillAsync(entity.resourceEntities[0].Skills, requisitionId);
                    }
                    if (entity.resourceEntities[0].Locations != null && entity.resourceEntities[0].Locations.Count > 0)
                    {
                        await RequisitionParameterValueEntities(entity.resourceEntities[0].Locations, requisitionId, Requisition_Parameter_type.Location);
                    }

                    if (entity.resourceEntities[0].Industries != null && entity.resourceEntities[0].Industries.Count > 0)
                    {
                        await RequisitionParameterValueEntities(entity.resourceEntities[0].Industries, requisitionId, Requisition_Parameter_type.Industry);
                    }

                    if (entity.resourceEntities[0].SubIndustries != null && entity.resourceEntities[0].SubIndustries.Count > 0)
                    {
                        await RequisitionParameterValueEntities(entity.resourceEntities[0].SubIndustries, requisitionId, Requisition_Parameter_type.SubIndustry);
                    }
                }
                await _allocationDbContext.SaveChangesAsync();
                return _req;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Requisition>> AddRequisitionAsyncNonSmillarResource(RequisitionRequest entity, RequisitionDemand reqDemand)
        {
            try
            {
                var _req = new List<Requisition> { };
                /************ Update Project Details ******************************/
                foreach (ResourceEntities resAlloc in entity.resourceEntities)
                {
                    Requisition req = new Requisition()
                    {
                        PipelineCode = entity.PipelineCode,
                        JobCode = entity.JobCode,
                        PipelineName = entity.PipelineName,
                        JobName = entity.JobName,
                        Description = entity.Description,
                        Designation = entity.Designation,
                        Grade = entity.Grade,
                        EffortsPerDay = resAlloc.Effort_Hrs,
                        StartDate = DateOnly.FromDateTime(resAlloc.StartDate),
                        EndDate = DateOnly.FromDateTime(resAlloc.EndDate),
                        IsPerDayHourAllocation = resAlloc.IsPerDayHourAllocation,
                        //Expertise = entity.Expertise,
                        //SMEG = entity.SMEG,
                        Competency = resAlloc.Competency,
                        CompetencyId = resAlloc.CompetencyId,
                        Offerings = entity.Offerings,
                        Solutions = entity.Solutions,
                        BusinessUnit = entity.BusinessUnit,
                        TotalHours = (bool)resAlloc.IsPerDayHourAllocation
                            ? CommonUtil.totalDaysPerDaysWise(entity.resourceEntities[0].StartDate, entity.resourceEntities[0].EndDate, resAlloc.Effort_Hrs)
                            : resAlloc.Effort_Hrs,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        ModifiedAt = entity.ModifiedAt,
                        ModifiedBy = entity.ModifiedBy,
                        IsActive = true,
                        RequisitionDemand = reqDemand.Id,
                        RequisitionStatus = RequisitionStatuses.PENDING,
                        ClientName = entity.ClientName,
                        RequisitionTypeId = entity.RequisitionTypeId
                    };
                    var response = (await _allocationDbContext.Set<Requisition>().AddAsync(req)).Entity;
                    await _allocationDbContext.SaveChangesAsync();
                    Guid requisitionId = response.Id;
                    _req.Add(response);
                    if (resAlloc.Locations != null && resAlloc.Locations.Count > 0)
                    {
                        await RequisitionParameterValueEntities(resAlloc.Locations, requisitionId, Requisition_Parameter_type.Location);
                    }
                    if (resAlloc.Parameters != null && resAlloc.Parameters.Count > 0)
                    {
                        await AddParametersAsync(resAlloc.Parameters, requisitionId);
                    }
                    if (resAlloc.Skills != null && resAlloc.Skills.Count > 0)
                    {
                        await AddSkillAsync(resAlloc.Skills, requisitionId);
                    }
                    if (resAlloc.Industries != null && resAlloc.Industries.Count > 0)
                    {
                        await RequisitionParameterValueEntities(resAlloc.Industries, requisitionId, Requisition_Parameter_type.Industry);
                    }
                    if (resAlloc.SubIndustries != null && resAlloc.SubIndustries.Count > 0)
                    {
                        await RequisitionParameterValueEntities(resAlloc.SubIndustries, requisitionId, Requisition_Parameter_type.SubIndustry);
                    }
                }
                await _allocationDbContext.SaveChangesAsync();
                return _req;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task AddParametersAsync(List<ParametersEntities> parameters, Guid RequisitionId)
        {
            foreach (ParametersEntities parameter in parameters)
            {
                var responseParameter = (await _allocationDbContext.Set<RequisitionParameters>().AddAsync(new RequisitionParameters()
                {
                    RequisitionId = RequisitionId,
                    Category = parameter.Name,
                    RequisitionWeight = parameter.Value,
                    IsChecked = parameter.IsChecked,

                })).Entity;
            }
            await _allocationDbContext.SaveChangesAsync();
        }
        public async Task AddSkillAsync(List<SkillsEntities> skills, Guid RequisitionId)
        {
            foreach (SkillsEntities skill in skills)
            {
                var responseParameter = (await _allocationDbContext.Set<RequisitionSkill>().AddAsync(new RequisitionSkill()
                {
                    RequisitionId = RequisitionId,
                    SkillName = skill.SkillName,
                    Type = skill.Type != null ? skill.Type : RequisitionSkillType.MANDATORY_SKILL,
                    SkillCode = skill.SkillCode

                })).Entity;
            }
            await _allocationDbContext.SaveChangesAsync();
        }
        private async Task RequisitionParameterValueEntities(List<RequisitionParameterValueEntities> parameterValues, Guid RequisitionId, string Type)
        {
            foreach (RequisitionParameterValueEntities parameterValue in parameterValues)
            {

                var responseParameter = (await _allocationDbContext.Set<RequisitionParameterValues>().AddAsync(new RequisitionParameterValues()
                {
                    RequisitionId = RequisitionId,
                    Value = parameterValue.Label,
                    Parameter = Type,
                })).Entity;
            }
            await _allocationDbContext.SaveChangesAsync();
        }
        public async Task<double> GetAllProjectRequisitionHoursByPipelineCode(string pipelineCode, string jobCode)
        {
            var result = await _allocationDbContext.Requisition.AsNoTracking()
               .Where(m => m.IsActive == true
                    && m.PipelineCode == pipelineCode
                    && (
                        string.IsNullOrEmpty(m.JobCode) ||
                        (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(m.JobCode) == Convert.ToString(jobCode))
                       )
                //&& m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()
                )
               .SumAsync(a => Convert.ToDouble(a.TotalHours));
            return result;

        }
        public async Task<List<ProjectAllocatedHoursRatioDto>> GetAllProjectRequisitionHoursByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes)
        {
            var result = _allocationDbContext.Requisition.AsNoTracking().AsEnumerable()
                .Where(m => m.IsActive == true
                    && pipelineCodes.Any(p => p.Key == m.PipelineCode && ((p.Value == null && m.JobCode == null) || p.Value == m.JobCode))

                )
                .GroupBy(l => new KeyValuePair<string, string>(l.PipelineCode, l.JobCode))
                .Select(pc => new ProjectAllocatedHoursRatioDto
                {
                    pipelineCode = pc.FirstOrDefault().PipelineCode,
                    jobCode = pc.FirstOrDefault().JobCode,
                    requistionTotalHours = pc.Sum(a => Convert.ToDouble(a.TotalHours)),
                })
                .ToList();

            return result;

        }
        public async Task<Boolean> SuspendAllocationRequisition(List<KeyValuePair<string, string>> projectCodes)
        {
            try
            {
                var projectCodeList = projectCodes.Select(p => p.Key.ToLower().Trim()).ToList();

                var requisitionDetails = await _allocationDbContext.Requisition
                        .Where(m =>
                            projectCodeList
                                .Contains(m.PipelineCode.Trim().ToLower())
                        )
                        .ToListAsync();

                if (requisitionDetails != null)
                {
                    foreach (var requisition in requisitionDetails)
                    {
                        int isProjectStarted = DateTime.Compare(DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue), requisition.StartDate.ToDateTime(TimeOnly.MinValue));

                        if (isProjectStarted > 0)
                        {
                            requisition.EndDate = DateOnly.FromDateTime(DateTime.Now);
                            requisition.IsActive = true;
                        }
                        else
                        {
                            requisition.IsActive = false;
                            requisition.RequisitionStatus = RequisitionStatuses.SUSPENDED;
                        }

                        requisition.ModifiedAt = DateTime.Now;
                    }
                    _allocationDbContext.Requisition.UpdateRange(requisitionDetails);
                    await _allocationDbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Not in use
        //public async Task<Boolean> RollOverRequisition(Guid requisitionId, double rollForwardDays)
        //{
        //    try
        //    {
        //        var requistionDetails = await _allocationDbContext.Requisition
        //                                .Where(m => m.Id == requisitionId).FirstOrDefaultAsync();

        //        if (requistionDetails != null)
        //        {
        //            //requistionDetails.IsActive = false;
        //            requistionDetails.StartDate = requistionDetails.StartDate.AddDays((int)rollForwardDays);
        //            requistionDetails.EndDate = requistionDetails.EndDate.AddDays((int)rollForwardDays);

        //            _allocationDbContext.Requisition.Update(requistionDetails);
        //            await _allocationDbContext.SaveChangesAsync();

        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<List<Requisition>> UpdateRequisition(UpdateRequisitionRequest entity
            , bool restrictIfPreviouslyAllocated = true
            , bool updateSkills = true
            , bool updateParameters = true
            , bool updateLocations = true
             , bool updateIndustries = true
            , bool updateSubIndustries = true
        )
        {
            var response = new List<Requisition>();
            foreach (var item in entity.resourceEntities)
            {
                var requisitionDetails = await _allocationDbContext.Requisition
                    .Where(m => m.Id == item.id)
                    .FirstOrDefaultAsync();

                var demandId = requisitionDetails.RequisitionDemand;

                var initialRequisitionDemand = await _allocationDbContext.RequisitionDemand
                    .Where(m => m.Id == demandId)
                    .FirstOrDefaultAsync();

                if (initialRequisitionDemand != null && initialRequisitionDemand.AllResourcesHaveSameDetails == true)
                {
                    initialRequisitionDemand.TotalDemands = initialRequisitionDemand.TotalDemands - 1;
                    initialRequisitionDemand.PendingDemands = initialRequisitionDemand.PendingDemands - 1;
                    _allocationDbContext.Update(initialRequisitionDemand);

                    // Create new demand and separate this requisition from earlier demand
                    var addedDemand = _allocationDbContext.RequisitionDemand
                        .Add(new()
                        {
                            TotalDemands = 1,
                            PendingDemands = 1,
                            AllResourcesHaveSameDetails = false,
                        });
                    await _allocationDbContext.SaveChangesAsync();
                    demandId = addedDemand.Entity.Id;
                    requisitionDetails.RequisitionDemand = demandId;
                }


                var allocationsInitiated = await _allocationDbContext.PublishedResAllocDetails
                    .Where(m => m.RequisitionId == item.id && m.IsActive == true)
                    .ToListAsync();
                if (restrictIfPreviouslyAllocated && (allocationsInitiated.Any() || requisitionDetails == null))
                {
                    throw new Exception("Allocation Already Initiated");
                }
                else
                {
                    requisitionDetails.BusinessUnit = item?.BusinessUnit != null ? item.BusinessUnit : requisitionDetails.BusinessUnit;
                    requisitionDetails.EffortsPerDay = item?.Effort_Hrs != null ? item.Effort_Hrs : requisitionDetails.EffortsPerDay;
                    requisitionDetails.StartDate = item?.StartDate != null ? DateOnly.FromDateTime(item.StartDate) : requisitionDetails.StartDate;
                    requisitionDetails.EndDate = item?.EndDate != null ? DateOnly.FromDateTime(item.EndDate) : requisitionDetails.EndDate;
                    requisitionDetails.RequisitionStatus = !String.IsNullOrEmpty(item.Status) ? item.Status : requisitionDetails.RequisitionStatus;
                    requisitionDetails.Description = String.IsNullOrEmpty(item?.Description) ? "" : item.Description;
                    requisitionDetails.Designation = String.IsNullOrEmpty(item?.Designation) ? requisitionDetails.Designation : item.Designation;
                    requisitionDetails.IsPerDayHourAllocation = item.IsPerDayHourAllocation;
                    //requisitionDetails.Expertise = item.Expertise != null ? item.Expertise : requisitionDetails.Expertise;
                    //requisitionDetails.SMEG = item.SMEG != null ? item.SMEG : requisitionDetails.SMEG;

                    requisitionDetails.CompetencyId = item.CompetencyId != null ? item.CompetencyId : requisitionDetails.CompetencyId;
                    requisitionDetails.Competency = item.Competency != null ? item.Competency : requisitionDetails.Competency;
                    requisitionDetails.Offerings = item.Offerings != null ? item.Offerings : requisitionDetails.Offerings;
                    requisitionDetails.Solutions = item.Solutions != null ? item.Solutions : requisitionDetails.Solutions;

                    requisitionDetails.TotalHours = item.IsPerDayHourAllocation
                        ? CommonUtil.totalDaysPerDaysWise(item.StartDate, item.EndDate, item.Effort_Hrs)
                        : item.Effort_Hrs;
                    requisitionDetails.ModifiedAt = entity.ModifiedDate;
                    requisitionDetails.ModifiedBy = entity.ModifiedBy;

                    _allocationDbContext.Requisition.Update(requisitionDetails);
                    _allocationDbContext.SaveChanges();
                    if (updateLocations == true)
                    {
                        var updateReqLocation = await UpdateRequisitionParameterValues(item.id, item.Locations, entity, Requisition_Parameter_type.Location);
                    }
                    if (updateIndustries == true)
                    {
                        var updateReqIndustries = await UpdateRequisitionParameterValues(item.id, item.industries, entity, Requisition_Parameter_type.Industry);
                    }
                    if (updateSubIndustries == true)
                    {
                        var updateReqSubIndustries = await UpdateRequisitionParameterValues(item.id, item.subIndustries, entity, Requisition_Parameter_type.SubIndustry);
                    }
                    if (updateParameters == true)
                    {
                        var updateReqParameters = await UpdateRequisitionParameter(item.id, item.Parameters, entity);
                    }
                    if (updateSkills == true)
                    {
                        var updateReqSkills = await UpdateRequisitionSkill(item.id, item.Skills, entity);
                    }
                    var updatedDetails = await GetRequisitionDetailsByRequisitionId(item.id);
                    response.Add(updatedDetails);
                }
            }
            return response;
        }

        public async Task<Boolean> UpdateRequisitionParameterValues(Guid requisitionId, List<RequisitionParameterValueEntities> entities, UpdateRequisitionRequest entity, string type)
        {
            var previousParameterValues = await _allocationDbContext.RequisitionParameterValues
                .Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();
            var prevNonMatchingParameterValues = previousParameterValues.Where(m => !entities.Any(p => m.Value.Trim().ToLower() == p.Label.Trim().ToLower()) && m.Parameter == type).ToList();
            foreach (var item in prevNonMatchingParameterValues)
            {
                _allocationDbContext.RequisitionParameterValues.Remove(item);
            }

            var newlyAddedParameterValues = entities
                .Where(m => !previousParameterValues.Any(p => p.Value.Trim().ToLower() == m.Label.Trim().ToLower() && p.Parameter == type))
                .ToList();

            foreach (var item in newlyAddedParameterValues)
            {
                var newParameterValues = new RequisitionParameterValues
                {
                    RequisitionId = requisitionId,
                    Parameter = type,
                    Value = item?.Label
                };
                _allocationDbContext.Set<RequisitionParameterValues>().Add(newParameterValues);
            }
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Boolean> UpdateRequisitionParameter(Guid requisitionId, List<ParametersEntities> parameters, UpdateRequisitionRequest entity)
        {
            var previousParameter = await _allocationDbContext.RequisitionParameters
                .Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();

            //No need to update matching parameters
            var prevMatchingParameter = previousParameter.Where(m => parameters.Any(p => m.Category.Trim().ToLower() == p.Name.Trim().ToLower())).ToList();
            foreach (var item in prevMatchingParameter)
            {
                var parameterDetails = parameters.Where(m => m.Name.Trim().ToLower() == item.Category.Trim().ToLower()).FirstOrDefault();
                item.RequisitionWeight = parameterDetails.Value;
                item.IsChecked = parameterDetails.IsChecked;
                _allocationDbContext.RequisitionParameters.Update(item);
            }

            var prevNonMatchingParameter = previousParameter.Where(m => !parameters.Any(p => m.Category.Trim().ToLower() == p.Name.Trim().ToLower())).ToList();
            foreach (var item in prevNonMatchingParameter)
            {
                var parameterDetails = parameters
                    .Where(m => m.Name.Trim().ToLower() == item.Category.Trim().ToLower())
                    .FirstOrDefault();

                //Directly deleting the item 
                _allocationDbContext.RequisitionParameters.Remove(item);
            }
            var newlyAddedParameter = parameters.Where(m => !previousParameter.Any(p => p.Category.Trim().ToLower() == m.Name.Trim().ToLower())).ToList();
            foreach (var item in newlyAddedParameter)
            {
                var newParameter = new RequisitionParameters
                {
                    RequisitionId = requisitionId,
                    Category = item.Name,
                    IsChecked = item.IsChecked,
                    RequisitionWeight = item.Value,
                };
                _allocationDbContext.Set<RequisitionParameters>().Add(newParameter);
            }
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateRequisitionSkill(Guid requisitionId, List<SkillsEntities> skills, UpdateRequisitionRequest entity)
        {
            var previousSkills = await _allocationDbContext.RequisitionSkill
                .Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();

            var prevNonMatchingSkills = previousSkills
                .Where(m => !skills.Any(p => m.SkillName.Trim().ToLower() == p.SkillName.Trim().ToLower()))
                .ToList();
            foreach (var item in prevNonMatchingSkills)
            {
                var SkillsDetails = skills
                    .Where(m => m.SkillName.Trim().ToLower() == item.SkillName.Trim().ToLower())
                    .FirstOrDefault();

                //Directly deleting the item 
                _allocationDbContext.RequisitionSkill.Remove(item);
            }

            var newlyAddedSkills = skills
                .Where(m => !previousSkills.Any(p => p.SkillName.Trim().ToLower() == m.SkillName.Trim().ToLower()))
                .ToList();
            foreach (var item in newlyAddedSkills)
            {
                var newSkill = new RequisitionSkill
                {
                    RequisitionId = requisitionId,
                    SkillName = item.SkillName,
                    Type = item.Type,
                    SkillCode = item.SkillCode,
                };
                _allocationDbContext.Set<RequisitionSkill>().Add(newSkill);
            }
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<BulkRequistionResponse> AddBulkRequisitions(List<BulkRequisition> requisitions, UserDecorator userInfo)
        {
            long requisitionCount = 0;
            var bulkRequisitionId = await GetRequisitionTypeByType(RequisitionTypeData.BulkRequisition);
            foreach (BulkRequisition requisition in requisitions)
            {
                CreateRequisitionWithDemandRequestDTO request = new()
                {
                    PipelineCode = requisition.PipelineCode,
                    JobCode = requisition.JobCode,
                    PipelineName = requisition.PipelineName,
                    JobName = requisition.JobName,
                    ClientName = requisition.ClientName,
                    StartDate = requisition.StartDate,
                    EndDate = requisition.EndDate,
                    EffortsPerDay = requisition.EffortsPerDay != 0 ? requisition.EffortsPerDay : requisition.TotalHours,
                    TotalHours = requisition.TotalHours,
                    Status = RequisitionStatuses.PENDING,
                    //Expertise = requisition.Expertise,
                    Competency = requisition.Competency,
                    CompetencyId = requisition.CompetencyId,
                    Offerings = requisition.Offerings,
                    Solutions = requisition.Solutions,
                    Designation = requisition.Designation,
                    Grade = requisition.Grade,
                    Description = !string.IsNullOrWhiteSpace(requisition.Description) ? requisition.Description : requisition.requisitionDescription,
                    IsPerDayHourAllocation = requisition.IsPerDayHourAllocation,
                    BusinessUnit = requisition.BusinessUnit,
                    //SMEG = requisition.SMEG,
                    IsActive = true,
                    IsAllResourcesSimilar = true,
                    NumberOfResources = requisition.NumberOfResources,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    CreatedBy = userInfo.email,
                    ModifiedBy = userInfo.email,
                    RequisitionTypeId = bulkRequisitionId.Id,
                    Skills = new List<SkillsEntities>(),
                    Locations = new List<RequisitionParameterValueEntities>(),
                    industries = new List<RequisitionParameterValueEntities>(),
                    subIndustries = new List<RequisitionParameterValueEntities>(),
                    Parameters = new List<ParametersEntities>()
                };

                if (requisition.SkillList != null)
                {
                    foreach (var skill in requisition.SkillList)
                    {
                        request.Skills.Add(new SkillsEntities
                        {
                            SkillName = skill.SkillName,
                            SkillCode = skill.SkillCode,
                            Type = RequisitionSkillType.MANDATORY_SKILL
                        });
                    }
                }
                if (requisition.locations != null && requisition.locations.Length > 0)
                {
                    var _locations = requisition.locations.Split(',');
                    foreach (var location in _locations)
                    {
                        request.Locations.Add(new RequisitionParameterValueEntities
                        {
                            Label = location,

                        });
                    }
                }

                if (requisition.Industry != null && requisition.Industry.Length > 0)
                {
                    var _industry = requisition.Industry.Split(',');
                    foreach (var industry in _industry)
                    {
                        request.industries.Add(new RequisitionParameterValueEntities
                        {
                            Label = industry,
                        });
                    }
                }

                if (requisition.SubIndustry != null && requisition.SubIndustry.Length > 0)
                {
                    var _subIndustry = requisition.SubIndustry.Split(',');
                    foreach (var subIndustry in _subIndustry)
                    {
                        request.subIndustries.Add(new RequisitionParameterValueEntities
                        {
                            Label = subIndustry,
                        });
                    }
                }

                if (requisition.parameters.Count > 0)
                {
                    foreach (var _param in requisition.parameters)
                    {
                        request.Parameters.Add(new ParametersEntities
                        {
                            Name = _param.Name,
                            Value = _param.Value,
                            IsChecked = _param.IsChecked,
                        });
                    }
                }
                List<Requisition> requisitionsCreated = await CreateRequisitionWithDemand(request);

                requisitionCount += requisition.NumberOfResources;
            }
            return new BulkRequistionResponse()
            {
                bulkRequisition = requisitions,
                totalNumberRequisition = requisitionCount
            };
        }
        public async Task<List<Requisition>> CreateRequisitionWithDemand(CreateRequisitionWithDemandRequestDTO createRequisitionWithDemandRequestDTO)
        {
            var demand = await _allocationDbContext.RequisitionDemand.AddAsync(new RequisitionDemand
            {
                AllResourcesHaveSameDetails = createRequisitionWithDemandRequestDTO.IsAllResourcesSimilar,
                TotalDemands = createRequisitionWithDemandRequestDTO.NumberOfResources,
                PendingDemands = createRequisitionWithDemandRequestDTO.NumberOfResources,
            });
            await _allocationDbContext.SaveChangesAsync();

            var response = new List<Requisition>();

            for (var i = 0; i < createRequisitionWithDemandRequestDTO.NumberOfResources; i++)
            {
                var req = await _allocationDbContext.Requisition.AddAsync(new Requisition
                {
                    PipelineCode = createRequisitionWithDemandRequestDTO.PipelineCode,
                    JobCode = createRequisitionWithDemandRequestDTO.JobCode,
                    PipelineName = createRequisitionWithDemandRequestDTO.PipelineName,
                    JobName = createRequisitionWithDemandRequestDTO.JobName,
                    StartDate = createRequisitionWithDemandRequestDTO.StartDate,
                    EndDate = createRequisitionWithDemandRequestDTO.EndDate,
                    TotalHours = createRequisitionWithDemandRequestDTO.TotalHours,
                    RequisitionStatus = createRequisitionWithDemandRequestDTO.Status,
                    //Expertise = createRequisitionWithDemandRequestDTO.Expertise,
                    //SMEG = createRequisitionWithDemandRequestDTO.SMEG,
                    Competency = createRequisitionWithDemandRequestDTO.Competency,
                    CompetencyId = createRequisitionWithDemandRequestDTO.CompetencyId,
                    Offerings = createRequisitionWithDemandRequestDTO.Offerings,
                    Solutions = createRequisitionWithDemandRequestDTO.Solutions,

                    Designation = createRequisitionWithDemandRequestDTO.Designation,
                    Grade = createRequisitionWithDemandRequestDTO.Grade,
                    Description = createRequisitionWithDemandRequestDTO.Description,
                    CreatedBy = createRequisitionWithDemandRequestDTO.CreatedBy,
                    ModifiedBy = createRequisitionWithDemandRequestDTO.ModifiedBy,
                    RequisitionDemand = demand.Entity.Id,
                    IsActive = true,
                    CreatedAt = createRequisitionWithDemandRequestDTO.CreatedAt,
                    ModifiedAt = createRequisitionWithDemandRequestDTO.ModifiedAt,
                    ClientName = createRequisitionWithDemandRequestDTO.ClientName,
                    BusinessUnit = createRequisitionWithDemandRequestDTO.BusinessUnit,
                    EffortsPerDay = createRequisitionWithDemandRequestDTO.EffortsPerDay,
                    IsPerDayHourAllocation = createRequisitionWithDemandRequestDTO.IsPerDayHourAllocation,
                    RequisitionTypeId = createRequisitionWithDemandRequestDTO.RequisitionTypeId,

                });
                await _allocationDbContext.SaveChangesAsync();
                var entity = new RequisitionRequest
                {
                    CreatedAt = createRequisitionWithDemandRequestDTO.CreatedAt,
                    ModifiedAt = createRequisitionWithDemandRequestDTO.ModifiedAt,
                    ModifiedBy = createRequisitionWithDemandRequestDTO.ModifiedBy,
                    CreatedBy = createRequisitionWithDemandRequestDTO.CreatedBy,
                };

                if (createRequisitionWithDemandRequestDTO.Parameters != null && createRequisitionWithDemandRequestDTO.Parameters.Count > 0)
                {
                    await AddParametersAsync(
                        createRequisitionWithDemandRequestDTO.Parameters, req.Entity.Id);
                }
                if (createRequisitionWithDemandRequestDTO.Skills != null && createRequisitionWithDemandRequestDTO.Skills.Count > 0)
                {
                    await AddSkillAsync(
                        createRequisitionWithDemandRequestDTO.Skills, req.Entity.Id);
                }
                if (createRequisitionWithDemandRequestDTO.Locations != null && createRequisitionWithDemandRequestDTO.Locations.Count > 0)
                {
                    await RequisitionParameterValueEntities(
                        createRequisitionWithDemandRequestDTO.Locations, req.Entity.Id, Requisition_Parameter_type.Location);
                }
                if (createRequisitionWithDemandRequestDTO.industries != null && createRequisitionWithDemandRequestDTO.Locations.Count > 0)
                {
                    await RequisitionParameterValueEntities(
                        createRequisitionWithDemandRequestDTO.industries, req.Entity.Id, Requisition_Parameter_type.Industry);
                }
                if (createRequisitionWithDemandRequestDTO.subIndustries != null && createRequisitionWithDemandRequestDTO.Locations.Count > 0)
                {
                    await RequisitionParameterValueEntities(
                        createRequisitionWithDemandRequestDTO.subIndustries, req.Entity.Id, Requisition_Parameter_type.SubIndustry);
                }
                response.Add(await _allocationDbContext.Requisition.Where(m => m.Id == req.Entity.Id).FirstOrDefaultAsync());
            }
            await _allocationDbContext.SaveChangesAsync();
            return response;
        }
        public async Task<RequisitionType> GetRequisitionTypeByType(string type)
        {
            if (!String.IsNullOrEmpty(type))
            {
                var responseType = await _allocationDbContext.RequisitionType
                    .Where(m => m.Type.Trim().ToLower().Equals(type.ToLower().Trim()))
                    .FirstOrDefaultAsync();
                if (responseType != null)
                {
                    return responseType;
                }
            }
            return null;
        }
        //not in use
        //public async Task ChangerequisitionEndDate(DateTime endDate, string pipelineCode, string projectCode)
        //{
        //    var res = await _allocationDbContext.Requisition
        //        .Where(e => e.JobCode == projectCode && e.PipelineCode == pipelineCode)
        //        .ToListAsync();
        //    foreach (var requisition in res)
        //    {
        //        requisition.EndDate = endDate.Date;
        //        _allocationDbContext.Update(requisition);
        //    }
        //    await _allocationDbContext.SaveChangesAsync();
        //}
        public async Task<List<RequisitionSkill>> GetSkillsByRequstionId(List<Guid> requistionId)
        {
            List<RequisitionSkill> response = await _allocationDbContext.RequisitionSkill
                .Where(req => requistionId.Contains(req.RequisitionId)).ToListAsync();
            return response;
        }
        public async Task<Requisition> UpdateRequisitionByRequisitionEntity(Requisition requisition)
        {
            bool flag = false;

            if (flag)
            {
                _allocationDbContext
                .Requisition.Update(requisition);
                await _allocationDbContext.SaveChangesAsync();
            }
            return requisition;
        }

        public async Task<Boolean> IsRequistionExistsInProject(string pipelineCode, string? jobCode)
        {
            var response = await _allocationDbContext.Requisition
                .Include(m => m.RequisitionType)
                .Where(req =>
                        req.PipelineCode.ToLower() == pipelineCode.ToLower()
                        && (string.IsNullOrEmpty(req.JobCode) || (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(req.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower()))
                        && req.IsActive == true
                        && req.RequisitionStatus == RequisitionStatuses.PENDING
                        && (req.RequisitionType.Type == RequisitionTypeData.CreateRequisition || req.RequisitionType.Type == RequisitionTypeData.BulkRequisition)
                )
                .ToListAsync();
            if (response.Count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Requisition>> GetRequistionByDate(DateTime CreatedAt, DateTime ModifiedAt)
        {
            var requisitionDetails =
            await _allocationDbContext.Requisition
                .Include(m => m.RequisitionParameterValues)
                .Include(m => m.RequisitionParameters)
                .Include(m => m.RequisitionSkill)
                .Include(m => m.RequisitionType)
                .Include(m => m.demands)
                .Where(m => (m.CreatedAt.Date == CreatedAt.Date || m.ModifiedAt.Date == ModifiedAt.Date))
                .ToListAsync();
            return requisitionDetails;
        }
        public async Task<List<PublishedResAllocDetails>> GetPublishedAllocationByDate(DateTime CreatedAt, DateTime ModifiedAt)
        {
            var publishAllocations =
            await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Requisition)

                .Where(m => (m.CreatedAt.Date == CreatedAt.Date || m.ModifiedAt.Date == ModifiedAt.Date))
                .ToListAsync();
            return publishAllocations;
        }
    }
}
