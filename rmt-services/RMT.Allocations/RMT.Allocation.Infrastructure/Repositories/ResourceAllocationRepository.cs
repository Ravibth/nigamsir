using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.Util;
using System.Collections.Immutable;
using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using RMT.Allocation.Infrastructure.Helpers;
using static RMT.Allocation.Domain.ConstantsDomain;
using static RMT.Allocation.Infrastructure.Constants;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using RMT.Allocation.Infrastructure.Migrations;
using System.Reflection;
using RMT.Allocation.Domain.DTO.Response;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Npgsql.Internal;
using RMT.Allocation.Domain.Helpers;
using System.Net.Http;

namespace RMT.Allocation.Infrastructure.Repositories
{
    public class ResourceAllocationRepository : IResourceAllocationRepository
    {
        private readonly AllocationDbContext _allocationDbContext;
        private readonly IConfiguration _configuration;

        public ResourceAllocationRepository(AllocationDbContext allocationDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _allocationDbContext = allocationDbContext;
        }
        /// <summary>
        /// Get Resource Allocation By using Email id
        /// </summary>
        /// <param name="EmpEmail">pass email id as string</param>
        /// <returns>publish resource allocation details </returns>
        public async Task<List<ResourceAllocationResponse>> GetResourceAllocationByEmail(string EmpEmail)
        {
            //List<PublishedResAlloc> _resourceAllocations = await (
            //    from resourceAllocated in _allocationDbContext.PublishedResAlloc
            //    join resourceDetails in _allocationDbContext.PublishedResAllocDetails
            //    on resourceAllocated.RequisitionId equals resourceDetails.Id
            //    where Convert.ToString(resourceDetails.EmpEmail).Trim().ToLower() == Convert.ToString(EmpEmail).Trim().ToLower()
            //    && resourceDetails.IsActive == true
            //    select resourceAllocated
            //).ToListAsync();

            List<PublishedResAlloc> _resourceAllocations = await _allocationDbContext.PublishedResAlloc
                .Include(p => p.ResourceAllocationsDetails)
                .Include(p => p.ResourceAllocationsDays)                     
                .Where(p => p.ResourceAllocationsDetails.EmpEmail.Trim().ToLower() == EmpEmail.Trim().ToLower()
                         && p.ResourceAllocationsDetails.IsActive == true)
                .ToListAsync();

            var resp = await TransformPublishedResAllocIntoResourceAllocationResponse(_resourceAllocations);
            return resp;
        }
        /// <summary>
        /// Update just the allocation status of Resource Allocation Details
        /// </summary>
        /// <param name="resourceAllocationDetails"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ResourceAllocationDetailsResponse> UpdateAllocationStatus(UnPublishedResAllocDetails resourceAllocationDetails)
        {

            var updatedResult = _allocationDbContext.UnPublishedResAllocDetails.Update(resourceAllocationDetails);
            await _allocationDbContext.SaveChangesAsync();
            var response = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<UnPublishedResAllocDetails> { updatedResult.Entity });
            return response.FirstOrDefault();
        }
        public async Task<ResourceAllocationDetailsResponse> MoveUnPublishedRecordToPublishedRecords(UnPublishedResAllocDetails unPublishedResAllocDetails, string jobId, GetHolidayLeaveResignedAbsconded leaveReult = null)
        {
            try
            {
                List<PublishedResAllocSkillEntity> skills = new();
                List<PublishedResAlloc> publishedResAllocations = new();
                if (unPublishedResAllocDetails != null && unPublishedResAllocDetails.Skills != null)
                {
                    foreach (var skill in unPublishedResAllocDetails.Skills)
                    {
                        skills.Add(new PublishedResAllocSkillEntity()
                        {
                            SkillCode = skill.SkillCode,
                            SkillName = skill.SkillName,
                            RequisitionId = skill.RequisitionId,
                        });
                    }
                }

                if (unPublishedResAllocDetails != null && unPublishedResAllocDetails.ResourceAllocations != null)
                {
                    foreach (var allocationItem in unPublishedResAllocDetails.ResourceAllocations)
                    {
                        publishedResAllocations.Add(new PublishedResAlloc()
                        {
                            RequisitionId = allocationItem.RequisitionId,
                            StartDate = allocationItem.StartDate,
                            Currency = allocationItem.Currency,
                            IsPerDayAllocation = allocationItem.IsPerDayAllocation,
                            Efforts = allocationItem.Efforts,
                            EndDate = allocationItem.EndDate,
                            RatePerHour = allocationItem.RatePerHour,
                            TotalWorkingDays = allocationItem.TotalWorkingDays,
                            
                        });
                    }
                }

                PublishedResAllocDetails publishedResAllocDetailsToAdd = new()
                {
                    StartDate = unPublishedResAllocDetails.StartDate,
                    EmpEmail = unPublishedResAllocDetails.EmpEmail,
                    ConfirmedAllocationDate = DateTime.UtcNow,
                    AllocationStatus = unPublishedResAllocDetails.AllocationStatus,
                    AllocationVersion = unPublishedResAllocDetails.AllocationVersion,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = unPublishedResAllocDetails.ModifiedBy,
                    ModifiedAt = unPublishedResAllocDetails.ParentPublishedResAllocDetailsId != null ? DateTime.UtcNow.AddSeconds(60) : DateTime.UtcNow,
                    ModifiedBy = unPublishedResAllocDetails.ModifiedBy,
                    Description = unPublishedResAllocDetails.Description,
                    EmpName = unPublishedResAllocDetails.EmpName,
                    EndDate = unPublishedResAllocDetails.EndDate,
                    RequisitionId = unPublishedResAllocDetails.RequisitionId,
                    TotalEffort = unPublishedResAllocDetails.TotalEffort,
                    Skills = skills.ToList(),
                    ResourceAllocations = publishedResAllocations,
                    IsActive = true,
                    IsUpdated = false,
                    ActualStartDate = unPublishedResAllocDetails.StartDate,
                    ActualEndDate = unPublishedResAllocDetails.EndDate
                };

                // Add New Records as published
                return await AddPublishedRecords(publishedResAllocDetailsToAdd, jobId, leaveReult);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public async Task<ResourceAllocationDetailsResponse> AddPublishedRecords(PublishedResAllocDetails publishedResAllocDetails, string jobId, GetHolidayLeaveResignedAbsconded leaveResult = null)
        {
            try
            {
                var allocationsToAdd = publishedResAllocDetails.ResourceAllocations
                    .OrderBy(m => m.StartDate)
                    .ToList();
                var skillsToAdd = publishedResAllocDetails.Skills.ToList();

                publishedResAllocDetails.Skills = new List<PublishedResAllocSkillEntity> { };
                publishedResAllocDetails.ResourceAllocations = new List<PublishedResAlloc> { };

                var addedPublishedResAllocDetails = await _allocationDbContext.Set<PublishedResAllocDetails>().AddAsync(publishedResAllocDetails);
                await _allocationDbContext.SaveChangesAsync();

                List<PublishedResAlloc> resourceAllocationsToBeAdded = new();
                var resourceAllocatedForItem = addedPublishedResAllocDetails.Entity;
                var requisitionDetails = _allocationDbContext.Requisition
                    .Where(m => m.Id == resourceAllocatedForItem.RequisitionId)
                    .FirstOrDefault();


                if (skillsToAdd != null)
                {
                    List<PublishedResAllocSkillEntity> resourceAllocationSkillEntity = new();
                    foreach (var skills in skillsToAdd)
                    {
                        resourceAllocationSkillEntity.Add(new()
                        {
                            SkillName = skills.SkillName,
                            SkillCode = skills.SkillCode,
                            RequisitionId = resourceAllocatedForItem.RequisitionId,
                            PublishedResAllocDetailsId = resourceAllocatedForItem.Id,
                        });
                    }
                    _allocationDbContext.PublishedResAllocSkillEntity.AddRange(resourceAllocationSkillEntity);
                }

                List<PublishedResAlloc> addedPublishedResAlloc = new();
                foreach (var item in allocationsToAdd)
                {
                    var resAlloc = item;
                    resAlloc.PublishedResAllocDetailsId = addedPublishedResAllocDetails.Entity.Id;
                    addedPublishedResAlloc.Add(resAlloc);
                }
                addedPublishedResAlloc = addedPublishedResAlloc
                    .OrderBy(m => m.StartDate)
                    .ToList();

                await _allocationDbContext.Set<PublishedResAlloc>().AddRangeAsync(addedPublishedResAlloc.ToArray());
                await _allocationDbContext.SaveChangesAsync();
                var finalAddedResAllocations = await _allocationDbContext.PublishedResAlloc
                    .Where(m => m.PublishedResAllocDetailsId == resourceAllocatedForItem.Id)
                    .ToListAsync();

                foreach (var resAlloc in finalAddedResAllocations)
                {
                    var resAllocReq = await TransformPublishedResAllocIntoResourceAllocationResponse(new List<PublishedResAlloc> { resAlloc });
                    if (resAlloc.IsPerDayAllocation)
                    {
                        await ResourceAllocationPerDaysUpdateAsync(resAllocReq.FirstOrDefault(), resourceAllocatedForItem.EmpEmail, jobId, leaveResult);
                    }
                    else
                    {
                        await ResourceAllocationDaysUpdateAsync(resAllocReq.FirstOrDefault(), resourceAllocatedForItem.EmpEmail, jobId, leaveResult);
                    }
                }
                return await GetAllocationByGuidHandler(resourceAllocatedForItem.Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<UpdateListOfAllocationDetailsStatusResponse> UpdateListOfAllocationDetailsStatus(List<UpdateListOfAllocationDetailsStatusRequest> request, GetHolidayLeaveResignedAbsconded leaveResult = null)
        {
            try
            {
                List<Guid> guids = new();
                List<string> employeeListForSuperCoach = new();
                bool isProjectCompetencyToRefresh = false;
                var resourceAllocationDetailsDeletionCount = 0;
                string pipelineCode = "";
                string jobCode = null;
                foreach (var req in request)
                {
                    var unPublishedResAllocationDetails = await _allocationDbContext.UnPublishedResAllocDetails
                        .Include(m => m.Requisition)
                        .Include(m => m.Skills)
                        .Where(m => m.Id == req.guid && m.IsActive == true)
                        .Include(m => m.ResourceAllocations)
                        .FirstOrDefaultAsync();

                    if (unPublishedResAllocationDetails is null)
                    {
                        throw new Exception($"No Un-Published Resource Allocation Details Found with '{req.guid}' id");
                    }

                    var requisitionDetails = await _allocationDbContext.Requisition
                        .Where(m => m.Id == unPublishedResAllocationDetails.RequisitionId && m.IsActive == true)
                        .Include(m => m.RequisitionType)
                        .FirstOrDefaultAsync();

                    var jobId = await _allocationDbContext.UnPublishedResAllocDays
                        .Where(m => m.RequisitionId == requisitionDetails.Id)
                        .Select(m => m.JobId)
                        .FirstOrDefaultAsync();

                    if (requisitionDetails is null)
                    {
                        throw new Exception("Unable to find requisition details");
                    }
                    pipelineCode = requisitionDetails.PipelineCode;
                    jobCode = requisitionDetails.JobCode;
                    AllocationAndRequisitionStatusForWorkflow responseStatus = new();
                    var isRollbackAllocation = false;
                    if (req.WorkflowSubModule != null && req.WorkflowSubModule.ToLower().Trim() == "employee_allocation_update_workflow")
                    {
                        responseStatus.AllocationStatus = req.AllocationStatus;
                        responseStatus.IsAllocationDeleted = false;
                        responseStatus.IsRequisitionDeleted = false;
                        responseStatus.RequisitionStatus = "";
                        responseStatus.AllocationConfirmedDate = null;
                        isRollbackAllocation = true;
                    }
                    if (Helpers.Helpers.IsWorkflowTaskCreatedForSuperCoach(req.AllocationStatus))
                    {
                        employeeListForSuperCoach.Add(unPublishedResAllocationDetails.EmpEmail);
                    }
                    responseStatus = Helpers.Helpers.GetAllocationAndRequisitionStatusByWorkflowStatus(req.AllocationStatus, requisitionDetails.RequisitionType.Type);

                    if (!String.IsNullOrEmpty(responseStatus.AllocationStatus))
                    {
                        unPublishedResAllocationDetails.AllocationStatus = responseStatus.AllocationStatus;
                        unPublishedResAllocationDetails.ModifiedAt = req.ModifiedDate;
                        if (req.ModifiedBy != null)
                        {
                            unPublishedResAllocationDetails.ModifiedBy = req.ModifiedBy;
                        }

                        if (responseStatus.RequisitionStatus == RequisitionStatuses.APPROVED)
                        {
                            //Remove previous unpublished days if allocated
                            await DeleteUnPublishedResAllocDaysByDetailsId(unPublishedResAllocationDetails.Id);

                            if (unPublishedResAllocationDetails.ParentPublishedResAllocDetailsId != null)
                            {
                                //Remove the previous published record if exists
                                await DeletePublishedResAllocDetailsByReqId(unPublishedResAllocationDetails.RequisitionId);
                            }

                            // Move unpublished items to published
                            var response = await MoveUnPublishedRecordToPublishedRecords(unPublishedResAllocationDetails, jobId, leaveResult);
                            guids.Add(response.Id);
                            isProjectCompetencyToRefresh = true;
                            ////Remove unpublished items
                            await DeleteUnPublishedAllocationsByDetails(unPublishedResAllocationDetails);

                            //Update requisition status as well
                            //requisitionDetails.RequisitionStatus = responseStatus.RequisitionStatus;
                            //_allocationDbContext.Update(requisitionDetails);
                        }
                        else if (responseStatus.RequisitionStatus == RequisitionStatuses.PENDING)
                        {
                            var response = await DeleteUnPublishedAllocationsByDetails(unPublishedResAllocationDetails);
                            guids.Add(response.Id);
                            pipelineCode = unPublishedResAllocationDetails.Requisition.PipelineCode;
                            jobCode = unPublishedResAllocationDetails.Requisition.JobCode;
                            resourceAllocationDetailsDeletionCount++;
                            if (unPublishedResAllocationDetails.ParentPublishedResAllocDetailsId == null)
                            {
                                // If there is no existing approved published state, release requisition 
                                await ReleaseRequisitionById(unPublishedResAllocationDetails.RequisitionId, String.IsNullOrEmpty(responseStatus.RequisitionStatus) ? RequisitionStatuses.PENDING : responseStatus.RequisitionStatus, false);
                            }
                            else
                            {
                                // Mark the published entry as with no updated unpublished record
                                var publishedDetails = await _allocationDbContext.PublishedResAllocDetails
                                    .Where(m => m.Id == unPublishedResAllocationDetails.ParentPublishedResAllocDetailsId && m.IsActive == true)
                                    .FirstOrDefaultAsync();
                                if (publishedDetails != null)
                                {
                                    publishedDetails.IsUpdated = false;
                                    publishedDetails.ModifiedAt = DateTime.UtcNow;
                                    publishedDetails.ActualStartDate = publishedDetails.StartDate;
                                    publishedDetails.ActualEndDate = publishedDetails.EndDate;
                                    _allocationDbContext.Update(publishedDetails);
                                }
                            }
                        }
                        else
                        {
                            _allocationDbContext.Update(unPublishedResAllocationDetails);
                        }
                    }
                }

                await _allocationDbContext.SaveChangesAsync();
                var result = new UpdateListOfAllocationDetailsStatusResponse
                {
                    Guids = guids,
                    AllocationDeletionCount = resourceAllocationDetailsDeletionCount,
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                    IsProjectCompetencyRefresh = isProjectCompetencyToRefresh,
                    EmployeeListForSuperCoach = employeeListForSuperCoach

                };
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<ResourceAllocationDetailsResponse>> AddAsync(List<UnPublishedResAllocDetails> entity, AllocationObj[] allocationsObj, Dictionary<string, double> resourceRate, List<AddResourceAllocationSkillRequestDTO> addResourceAllocationSkillRequestDTOs, GetHolidayLeaveResignedAbsconded leaveResults, string jobId)
        {
            try
            {
                var requisitionsToUpdateAllocationFor = entity.Select(m => m.RequisitionId);

                var allocations = await _allocationDbContext.UnPublishedResAllocDetails
                    .Where(m =>
                            requisitionsToUpdateAllocationFor.ToList().Contains(m.RequisitionId)
                            && m.AllocationStatus.ToLower() != AllocationStatuses.REJECTED.ToLower()
                            && m.IsActive == true
                        )
                    .ToListAsync();
                if (allocations is null || allocations.Any())
                {
                    throw new Exception("User is already allocated for the requisition");
                }

                await _allocationDbContext.Set<UnPublishedResAllocDetails>().AddRangeAsync(entity.ToArray());
                await _allocationDbContext.SaveChangesAsync();

                List<UnPublishedResAllocDetails> unPublishedResourceDetailsAdded = await _allocationDbContext.UnPublishedResAllocDetails
                    .Where(m => requisitionsToUpdateAllocationFor.ToList().Contains(m.RequisitionId) && m.IsActive == true)
                    .ToListAsync();

                foreach (var unPublishedResAllocDetailAdded in unPublishedResourceDetailsAdded)
                {
                    List<UnPublishedResAlloc> resourceAllocationsToBeAdded = new();
                    var resourceAllocatedForItem = entity.Where(m => m.RequisitionId == unPublishedResAllocDetailAdded.RequisitionId).FirstOrDefault();
                    var requisitionDetails = _allocationDbContext.Requisition.Where(m => m.Id == unPublishedResAllocDetailAdded.RequisitionId).FirstOrDefault();

                    var skillsToAdd = addResourceAllocationSkillRequestDTOs.Where(m => m.RequisitionId == unPublishedResAllocDetailAdded.RequisitionId).FirstOrDefault().Skills.ToList();
                    if (skillsToAdd != null)
                    {
                        List<UnPublishedResAllocSkillEntity> resourceAllocationSkillEntity = new();
                        foreach (var skills in skillsToAdd)
                        {
                            resourceAllocationSkillEntity.Add(new()
                            {
                                SkillName = skills.SkillName,
                                SkillCode = skills.SkillCode,
                                RequisitionId = resourceAllocatedForItem.RequisitionId,
                                UnPublishedResAllocDetailsId = resourceAllocatedForItem.Id,
                            });
                        }
                        _allocationDbContext.UnPublishedResAllocSkillEntity.AddRange(resourceAllocationSkillEntity);
                    }
                    await _allocationDbContext.SaveChangesAsync();

                    foreach (var allocationDetails in allocationsObj)
                    {
                        UnPublishedResAlloc unPublishedResourceAllocation = new()
                        {
                            StartDate = allocationDetails.startDate,
                            EndDate = allocationDetails.endDate,
                            Efforts = allocationDetails.effort,
                            IsPerDayAllocation = allocationDetails.isPerDayHourAllocation,
                            RequisitionId = resourceAllocatedForItem.RequisitionId,
                            RatePerHour = resourceRate.ContainsKey(resourceAllocatedForItem.EmpEmail) ? resourceRate[resourceAllocatedForItem.EmpEmail] : 0,
                            //TODO CHECK 
                            //!WARNING
                            TotalWorkingDays = 1,
                            UnPublishedResAllocDetailsId = resourceAllocatedForItem.Id,
                            //ResAllocDetailsId = item.Id,
                            //EmpEmail = item.EmpEmail,
                            //EmpName = item.EmpName,
                            //PipelineCode = item.PipelineCode,
                            //JobCode = item.JobCode,
                            //JobName = item.JobName,
                            //IsActive = item.IsActive,
                            //ConfirmedAllocationStartDate = allocationDetails.startDate,
                            //ConfirmedAllocationEndDate = allocationDetails.endDate,
                            //ConfirmedPerDayHours = allocationDetails.effort,
                            //isPerDayHourAllocation = allocationDetails.isPerDayHourAllocation,
                            ////ProjectCode = item.ProjectCode,
                            //RequisitionId = item.RequisitionId,
                            //AllocationStatus = item.AllocationStatus,
                            //RecordType = item.RecordType,
                            //PipelineName = item.PipelineName,
                            //CreatedBy = item.CreatedBy,
                            //ModifiedBy = item.ModifiedBy,
                            //CreatedDate = item.CreatedDate,
                            //ModifiedDate = item.ModifiedDate,
                            //ClientName = requisitionDetails.ClientName != null ? requisitionDetails.ClientName : "",
                            //RatePerHour = resourceRate.ContainsKey(item.EmpEmail) ? resourceRate[item.EmpEmail] : 0,

                        };

                        resourceAllocationsToBeAdded.Add(unPublishedResourceAllocation);
                    }
                    await _allocationDbContext.Set<UnPublishedResAlloc>().AddRangeAsync(resourceAllocationsToBeAdded.ToArray());
                    await _allocationDbContext.SaveChangesAsync();

                    var finalAddedResAllocations = await _allocationDbContext.UnPublishedResAlloc
                        .Include(m => m.ResourceAllocationsDetails)
                        .Where(m => m.UnPublishedResAllocDetailsId == resourceAllocatedForItem.Id)
                        .ToListAsync();

                    foreach (var resAlloc in finalAddedResAllocations)
                    {
                        var resAllocReq = await TransformUnPublishedResAllocIntoResourceAllocationResponse(new List<UnPublishedResAlloc> { resAlloc });
                        if (resAlloc.IsPerDayAllocation)
                        {
                            await ResourceAllocationPerDaysUpdateAsync(resAllocReq.FirstOrDefault(), resourceAllocatedForItem.EmpEmail, jobId, leaveResults);
                        }
                        else
                        {
                            await ResourceAllocationDaysUpdateAsync(resAllocReq.FirstOrDefault(), resourceAllocatedForItem.EmpEmail, jobId, leaveResults);
                        }
                    }

                }

                var requisitions = await _allocationDbContext.Requisition
                    .Where(m => requisitionsToUpdateAllocationFor.ToList().Contains(m.Id)).ToListAsync();
                foreach (var requisitionItem in requisitions)
                {
                    var allocationItemDetailsStatus = await _allocationDbContext.UnPublishedResAllocDetails
                        .Where(m => m.RequisitionId == requisitionItem.Id && m.IsActive)
                        .Select(m => m.AllocationStatus)
                        .FirstOrDefaultAsync();

                    requisitionItem.RequisitionStatus = RequisitionStatuses.ALLOCATED;
                    _allocationDbContext.Requisition.Update(requisitionItem);
                }

                var demands = await _allocationDbContext.RequisitionDemand.Where(m => requisitions[0].RequisitionDemand == m.Id).FirstOrDefaultAsync();
                demands.PendingDemands -= entity.Count;

                await _allocationDbContext.SaveChangesAsync();


                var response = await _allocationDbContext.UnPublishedResAllocDetails
                    .Include(m => m.ResourceAllocations)
                    .Include(m => m.Requisition)
                    .Include(m => m.Requisition.RequisitionType)
                    .Include(m => m.Skills)
                    .Where(m => requisitionsToUpdateAllocationFor.ToList().Contains(m.RequisitionId) && m.IsActive == true)
                    .ToListAsync();

                foreach (var item in response)
                {
                    item.Requisition = await _allocationDbContext.Requisition
                        .Include(m => m.RequisitionType)
                        .Where(m => m.Id == item.Requisition.Id)
                        .FirstOrDefaultAsync();
                }

                return await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //getAllActiveAllocations
        //reqId FK -> INC
        //required Table.incudes(ResourceAllocationSkills,RequisitionLocation,)
        //return List<ResourceAllocationDetails>
        //ResponseError List
        //data
        /// <summary>
        /// Get all the Projects employee is allocated to currently
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        // public async Task<List<ResourceAllocation>> GetProjectsByEmployeeEmail(string email)
        // {
        //     //use case1
        //     return await _allocationDbContext.ResourceAllocation.Where(m => (m.IsActive == true && Convert.ToString(m.EmpEmail).Trim().ToLower() == Convert.ToString(email).Trim().ToLower())).ToListAsync<ResourceAllocation>();
        // }

        public async Task<List<ResourceAllocationResponse>> GetProjectsByEmployeeEmailAndPipelineCode
            (string? email, string? pipelineCode, string? jobCode, string? allocationType)
        {
            List<PublishedResAlloc> _resourceAllocations = await (
            from resourceAllocated in _allocationDbContext.PublishedResAlloc
            join resourceDetails in _allocationDbContext.PublishedResAllocDetails
            on resourceAllocated.PublishedResAllocDetailsId equals resourceDetails.Id
            join requisition in _allocationDbContext.Requisition
            on resourceAllocated.RequisitionId equals requisition.Id
            where
            (string.IsNullOrEmpty(email) || (!string.IsNullOrEmpty(email) == true && Convert.ToString(resourceDetails.EmpEmail).Trim().ToLower() == Convert.ToString(email).Trim().ToLower()))
            && Convert.ToString(requisition.PipelineCode).Trim().ToLower() == Convert.ToString(pipelineCode).Trim().ToLower()
            && (string.IsNullOrEmpty(requisition.JobCode) || (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(requisition.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower()))
            && (string.IsNullOrEmpty(allocationType) || !string.IsNullOrEmpty(allocationType) == true && Convert.ToString(resourceDetails.AllocationStatus).Trim().ToLower() == Convert.ToString(email).Trim().ToLower())
            && resourceDetails.IsActive == true
            select resourceAllocated
            ).ToListAsync();
            var resp = await TransformPublishedResAllocIntoResourceAllocationResponse(_resourceAllocations);
            return resp;
        }

        //Old
        //public async Task<ResourceAllocationDetailsResponse?> GetAllocationByRequisitionId(Guid requisitionId)
        //{

        //    var resourceAllocation = await _allocationDbContext.ResourceAllocation.AsNoTracking()
        //        .Where(m => (m.IsActive == true
        //        && m.RequisitionId == requisitionId))
        //        .ToListAsync();

        //    var requestion = await _allocationDbContext.Requisition.AsNoTracking()
        //        .Where(req => req.RequisionId == requisitionId)
        //        .Include(req => req.RequisitionTypes)
        //        .Include(req => req.RequisitionSkill)
        //        .Include(req => req.RequisitionLocation)
        //        .Include(req => req.RequisitionParameters)
        //        .FirstAsync();
        //    var result = _allocationDbContext.ResourceAllocationDetails.AsNoTracking()
        //        .Where(resourceAllocated => resourceAllocated.IsActive == true
        //    && resourceAllocated.RequisitionId == requisitionId)
        //        .Include(m => m.ResourceAllocationSkillEntity)
        //        .ToImmutableList();
        //    if (result == null || result.Count == 0)
        //    {
        //        return new ResourceAllocationDetails();
        //    }
        //    else
        //    {
        //        result[0].ResourceAllocation = resourceAllocation;
        //        result[0].Requisitions = requestion;
        //        return await Task.FromResult(result.First());
        //    }
        //}
        public async Task<ResourceAllocationDetailsResponse> GetAllocationByRequisitionId(Guid requisitionId)
        {
            var unpublishedRecord = await _allocationDbContext.UnPublishedResAllocDetails
               .Where(m => m.RequisitionId == requisitionId && m.IsActive == true)
               .Include(m => m.Requisition)
               .Include(m => m.Requisition.RequisitionType)
               .Include(m => m.ResourceAllocations.OrderBy(m => m.StartDate))
               .Include(m => m.Skills)
               .FirstOrDefaultAsync();
            if (unpublishedRecord != null)
            {
                var response = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<UnPublishedResAllocDetails> { unpublishedRecord });
                return response.FirstOrDefault();
            }
            var publishedRecord = await _allocationDbContext.PublishedResAllocDetails
                .Where(m => m.RequisitionId == requisitionId && m.IsActive == true)
                .Include(m => m.Requisition)
                .Include(m => m.Requisition.RequisitionType)
                .Include(m => m.ResourceAllocations.OrderBy(m => m.StartDate))
                .Include(m => m.Skills)
                .FirstOrDefaultAsync();
            if (publishedRecord != null)
            {
                var response = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<PublishedResAllocDetails> { publishedRecord });
                return response.FirstOrDefault();
            }
            return null;
        }

        public async Task<Int64> GetWithConsumedHours(Guid requisitionId)
        {
            Int64 consumedHours = 0;
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var pastDateAllocationItem = await _allocationDbContext.PublishedResAlloc
                .Where(m => m.StartDate <= currentDate && m.EndDate >= currentDate && m.RequisitionId == requisitionId)
                .Include(m => m.ResourceAllocationsDays.Where(m => m.AllocationDate <= currentDate))
                .FirstOrDefaultAsync();
            if (pastDateAllocationItem != null)
            {
                consumedHours = pastDateAllocationItem.ResourceAllocationsDays
                    .Sum(m => m.Efforts);
            }
            return consumedHours;
        }


        public async Task<ResourceAllocationDetailsResponse> UpdateUnPublishedRecordsAsync(UnPublishedResAllocDetails unPublishedResAllocDetailsToUpdate, GetHolidayLeaveResignedAbsconded leaveResults, string jobId, bool? updateAllocationDates = false)
        {

            var newAllocations = unPublishedResAllocDetailsToUpdate.ResourceAllocations;
            unPublishedResAllocDetailsToUpdate.ResourceAllocations = null;
            _allocationDbContext.UnPublishedResAllocDetails.Update(unPublishedResAllocDetailsToUpdate);

            var prevUnPublishedResAllocations = await _allocationDbContext.UnPublishedResAlloc
                .Where(m => m.UnPublishedResAllocDetailsId == unPublishedResAllocDetailsToUpdate.Id)
                .ToListAsync();

            await this.CleanUnPublishedAllocationByRequisitionId(null, unPublishedResAllocDetailsToUpdate.Id, false);

            //Add new UnPublished Allocations
            await _allocationDbContext.Set<UnPublishedResAlloc>().AddRangeAsync(newAllocations);
            await _allocationDbContext.SaveChangesAsync();

            var newlyPublishedResAllocations = await _allocationDbContext.UnPublishedResAlloc
                .Where(m => m.UnPublishedResAllocDetailsId == unPublishedResAllocDetailsToUpdate.Id)
                .ToListAsync();

            foreach (var resAlloc in newlyPublishedResAllocations)
            {
                var resAllocReq = await TransformUnPublishedResAllocIntoResourceAllocationResponse(new List<UnPublishedResAlloc> { resAlloc });
                if (resAlloc.IsPerDayAllocation)
                {
                    await ResourceAllocationPerDaysUpdateAsync(resAllocReq.FirstOrDefault(), unPublishedResAllocDetailsToUpdate.EmpEmail, jobId, leaveResults);
                }
                else
                {
                    await ResourceAllocationDaysUpdateAsync(resAllocReq.FirstOrDefault(), unPublishedResAllocDetailsToUpdate.EmpEmail, jobId, leaveResults);
                }
            }

            return await GetAllocationByGuidHandler(unPublishedResAllocDetailsToUpdate.Id);
        }

        public async Task<ResourceAllocationDetailsResponse> UpdateAsync(ResourceAllocationDetailsResponse resourceAllocationDetails, bool? updateAllocationDates = false)
        {
            return null;
            //Todo: uncomment later.
            //var resultResourceAllocationHistory = await addAllocationHistory(resourceAllocationDetails);

            //var resAllocation = await _allocationDbContext.ResourceAllocationDetails
            //    .Where(a => a.RequisitionId == resourceAllocationDetails.RequisitionId && a.IsActive == true)
            //    .Include(m => m.ResourceAllocationSkillEntity)
            //    .FirstOrDefaultAsync();
            ////if (resAllocation == null)
            ////{
            //if (resAllocation == null)
            //{
            //    var addedDemand = await _allocationDbContext.Set<ResourceAllocationDetails>().AddAsync(resourceAllocationDetails);
            //}

            ////}
            //else
            //{
            //    if (updateAllocationDates == true)
            //    {
            //        resAllocation.AllocationStartDate = resourceAllocationDetails.AllocationStartDate;
            //        resAllocation.AllocationEndDate = resourceAllocationDetails.AllocationEndDate;
            //    }
            //    resAllocation.Description = resourceAllocationDetails.Description;
            //    resAllocation.IsContinuousAllocation = resourceAllocationDetails.IsContinuousAllocation;
            //    resAllocation.ModifiedBy = resourceAllocationDetails.ModifiedBy;
            //    resAllocation.ModifiedDate = resourceAllocationDetails.ModifiedDate;
            //    resAllocation.IsActive = resourceAllocationDetails.IsActive;
            //    resAllocation.TotalEffort = resourceAllocationDetails.TotalEffort;
            //    resAllocation.AllocationStatus = resourceAllocationDetails.AllocationStatus;
            //    resAllocation.PreviousGuid = resourceAllocationDetails.guid;
            //    _allocationDbContext.ResourceAllocationDetails.Update(resAllocation);
            //}
            /////************************ add resource allocations entries ****************************/
            //foreach (ResourceAllocation resAlloc in resourceAllocationDetails.ResourceAllocation)
            //{
            //    var resAllocationEntity = await _allocationDbContext.ResourceAllocation.Where(a => a.Id == resAlloc.Id).FirstOrDefaultAsync();
            //    if (resAlloc.Id == 0 || resAllocationEntity == null)
            //    {
            //        var resourceAllocationEntity = new ResourceAllocation();
            //        resourceAllocationEntity.EmpEmail = resourceAllocationDetails.EmpEmail;
            //        resourceAllocationEntity.EmpName = resourceAllocationDetails.EmpName;
            //        resourceAllocationEntity.PipelineCode = resourceAllocationDetails.PipelineCode;
            //        resourceAllocationEntity.JobCode = resourceAllocationDetails.JobCode;
            //        resourceAllocationEntity.JobName = resourceAllocationDetails.JobName;
            //        resourceAllocationEntity.IsActive = resourceAllocationDetails.IsActive;
            //        resourceAllocationEntity.ConfirmedAllocationStartDate = resAlloc.ConfirmedAllocationStartDate.GetUniversalTime();
            //        resourceAllocationEntity.ConfirmedAllocationEndDate = resAlloc.ConfirmedAllocationEndDate.GetUniversalTime();
            //        resourceAllocationEntity.ConfirmedPerDayHours = resAlloc.ConfirmedPerDayHours;
            //        resourceAllocationEntity.TotalWorkingDays = resAlloc.TotalWorkingDays;
            //        //resourceAllocationEntity.ProjectCode = resourceAllocationDetails.ProjectCode;
            //        resourceAllocationEntity.IsActive = resAlloc.IsActive;
            //        resourceAllocationEntity.RequisitionId = resourceAllocationDetails.RequisitionId;
            //        resourceAllocationEntity.AllocationStatus = resourceAllocationDetails.AllocationStatus;
            //        resourceAllocationEntity.RecordType = resourceAllocationDetails.RecordType;
            //        resourceAllocationEntity.PipelineName = resourceAllocationDetails.PipelineName;
            //        resourceAllocationEntity.isPerDayHourAllocation = resourceAllocationEntity.isPerDayHourAllocation;
            //        resourceAllocationEntity.CreatedBy = resAlloc.CreatedBy;// "";
            //        resourceAllocationEntity.ModifiedBy = resAlloc.ModifiedBy;// "";
            //        resourceAllocationEntity.ModifiedDate = DateTime.UtcNow;
            //        resourceAllocationEntity.isPerDayHourAllocation = resAlloc.isPerDayHourAllocation;
            //        resourceAllocationEntity.CreatedDate = DateTime.UtcNow;
            //        resourceAllocationEntity.ResAllocDetailsId = resAlloc.ResAllocDetailsId;
            //        resourceAllocationEntity.ClientName = resAlloc.ClientName;


            //        var result1 = await _allocationDbContext.Set<ResourceAllocation>().AddAsync(resourceAllocationEntity);
            //        await _allocationDbContext.SaveChangesAsync();
            //        resourceAllocationEntity.Id = result1.Entity.Id;
            //        if (resourceAllocationEntity.isPerDayHourAllocation)
            //            await ResourceAllocationPerDaysUpdateAsync(resourceAllocationEntity);
            //        else
            //            await ResourceAllocationDaysUpdateAsync(resourceAllocationEntity);
            //    }
            //    else
            //    {
            //        if (resAllocationEntity != null)
            //        {
            //            resAllocationEntity.ConfirmedAllocationStartDate = resAlloc.ConfirmedAllocationStartDate;
            //            resAllocationEntity.ConfirmedAllocationEndDate = resAlloc.ConfirmedAllocationEndDate;

            //            resAllocationEntity.RecordType = resourceAllocationDetails.RecordType;
            //            resAllocationEntity.AllocationStatus = resourceAllocationDetails.AllocationStatus;

            //            resAllocationEntity.ConfirmedPerDayHours = resAlloc.ConfirmedPerDayHours;
            //            resAllocationEntity.TotalWorkingDays = resAlloc.TotalWorkingDays;
            //            resAllocationEntity.IsActive = resAlloc.IsActive;
            //            resAllocationEntity.isPerDayHourAllocation = resAlloc.isPerDayHourAllocation;
            //            resAllocationEntity.ModifiedBy = resAlloc.ModifiedBy;// "";
            //            resAllocationEntity.ModifiedDate = DateTime.UtcNow;
            //            _allocationDbContext.ResourceAllocation.Update(resAllocationEntity);
            //            await _allocationDbContext.SaveChangesAsync();
            //            if (resAllocationEntity.isPerDayHourAllocation)
            //                await ResourceAllocationPerDaysUpdateAsync(resAllocationEntity);
            //            else
            //                await ResourceAllocationDaysUpdateAsync(resAllocationEntity);
            //        }
            //    }
            //}
            //var result = await GetAllocationByRequisitionId(resourceAllocationDetails.RequisitionId);
            //return result;
        }

        public async Task<Boolean> ResourceAllocationDaysUpdateAsync(ResourceAllocationResponse resourceAllocation, string empEmail, string jobId, GetHolidayLeaveResignedAbsconded? leaveResults = null)
        {
            try
            {
                DateOnly startDate = resourceAllocation.StartDate;
                DateOnly endDate = resourceAllocation.EndDate;
                Boolean isDeleted = await DeleteAllocatedDaysByResAllocId(resourceAllocation.Id, resourceAllocation.Type);
                var allocatedAllocations = await GetAllocatedDaysByEmailId(startDate, endDate, empEmail, true);
                Int64 requireAllocationHours = resourceAllocation.Efforts;

                if (requireAllocationHours > 0)
                {
                    var finalEndingDate = startDate;
                    DateOnly? finalStartDate = null;
                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        bool isHoliday = false;
                        GTLeaveBaseDTO currentLeaveDate = null;
                        if (requireAllocationHours <= 0)
                        {
                            break;
                        }
                        if (leaveResults != null && leaveResults.HolidayResponseTask != null && leaveResults.HolidayResponseTask.Count > 0)
                            isHoliday = leaveResults.HolidayResponseTask.Where(holiday => DateOnly.FromDateTime(holiday.holiday_date.Date).Equals(date)).Any();
                        if (leaveResults != null && leaveResults.LeavesResponseTask != null && leaveResults.LeavesResponseTask.Count > 0)
                        {
                            currentLeaveDate = leaveResults.LeavesResponseTask.Where(leave => date == leave.leave_date).FirstOrDefault();

                        }

                        if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday && !isHoliday && !(currentLeaveDate != null && currentLeaveDate.leave_type == TimelineType.FULL_DAY_LEAVE))
                        {
                            finalEndingDate = date;

                            var allocatedHours = allocatedAllocations
                                .Where(a => a.AllocationDate == date && a.RequisitionId != resourceAllocation.RequisitionId)
                                .Sum(a => a.Efforts);
                            var todayWorkingHrs = Constants.WorkingHourPerDay;
                            //Skiping half day leave
                            if (currentLeaveDate != null && (currentLeaveDate.leave_type == TimelineType.FIRST_HALF_LEAVE || currentLeaveDate.leave_type == TimelineType.SECOND_HALF_LEAVE))
                            {
                                todayWorkingHrs = Constants.HalfDayHours;
                            }
                            //change
                            if (allocatedHours <= todayWorkingHrs || allocatedHours == null)
                            {
                                if (allocatedHours == null)
                                {
                                    allocatedHours = 0;
                                }
                                if (allocatedHours < todayWorkingHrs)
                                {
                                    if (finalStartDate == null)
                                    {
                                        finalStartDate = date;
                                    }
                                    //8 -> today 4 -> today
                                    Int64 remainingHours = todayWorkingHrs - allocatedHours;
                                    Int64 allocateHours = 0;
                                    if (remainingHours < requireAllocationHours)
                                    {
                                        allocateHours = remainingHours;
                                        requireAllocationHours -= remainingHours;
                                    }
                                    else
                                    {
                                        allocateHours = requireAllocationHours;
                                        requireAllocationHours = 0;
                                    }
                                    if (resourceAllocation.Type == AllocationType.PUBLISHED)
                                    {
                                        PublishedResAllocDays publishedDays = new()
                                        {
                                            AllocationDate = date,
                                            Efforts = allocateHours,
                                            RequisitionId = resourceAllocation.RequisitionId,
                                            PublishedResAllocId = resourceAllocation.Id,
                                            EmailId = empEmail,
                                            JobCode = resourceAllocation.JobCode,
                                            PipelineCode = resourceAllocation.PipelineCode,
                                            RatePerHour = resourceAllocation.RatePerHour,
                                            JobId = String.IsNullOrEmpty(jobId) ? String.Empty : jobId
                                        };
                                        await _allocationDbContext.Set<PublishedResAllocDays>().AddAsync(publishedDays);
                                    }
                                    else
                                    {
                                        UnPublishedResAllocDays unpublishedDays = new()
                                        {
                                            AllocationDate = date,
                                            Efforts = allocateHours,
                                            RequisitionId = resourceAllocation.RequisitionId,
                                            UnPublishedResAllocId = resourceAllocation.Id,
                                            EmailId = empEmail,
                                            JobCode = resourceAllocation.JobCode,
                                            PipelineCode = resourceAllocation.PipelineCode,
                                            RatePerHour = resourceAllocation.RatePerHour,
                                            JobId = String.IsNullOrEmpty(jobId) ? String.Empty : jobId
                                        };
                                        await _allocationDbContext.Set<UnPublishedResAllocDays>().AddAsync(unpublishedDays);
                                    }
                                }
                            }
                        }
                    }
                    if (finalStartDate != null)
                    {
                        if (resourceAllocation.Type == AllocationType.PUBLISHED)
                        {
                            var resAlloc = await _allocationDbContext.PublishedResAlloc
                                .Where(m => m.Id == resourceAllocation.Id)
                                .FirstOrDefaultAsync();
                            resAlloc.StartDate = (DateOnly)finalStartDate;
                            resAlloc.EndDate = finalEndingDate;
                            _allocationDbContext.PublishedResAlloc.Update(resAlloc);
                            await _allocationDbContext.SaveChangesAsync();

                            var publishedResAllocDetails = await _allocationDbContext.PublishedResAllocDetails
                                .Where(m => m.Id == resourceAllocation.PublishedResAllocDetailsId)
                                .Include(m => m.ResourceAllocations)
                                .FirstOrDefaultAsync();
                            publishedResAllocDetails.EndDate = publishedResAllocDetails.ResourceAllocations.MaxBy(a => a.EndDate).EndDate;
                            publishedResAllocDetails.StartDate = publishedResAllocDetails.ResourceAllocations.MinBy(a => a.StartDate).StartDate;
                            publishedResAllocDetails.ActualStartDate = publishedResAllocDetails.ResourceAllocations.MinBy(a => a.StartDate).StartDate;
                            publishedResAllocDetails.ActualEndDate = publishedResAllocDetails.ResourceAllocations.MaxBy(a => a.EndDate).EndDate;
                            _allocationDbContext.PublishedResAllocDetails.Update(publishedResAllocDetails);
                            await _allocationDbContext.SaveChangesAsync();

                        }
                        else if (resourceAllocation.Type == AllocationType.UnPUBLISHED)
                        {
                            var resAlloc = await _allocationDbContext.UnPublishedResAlloc
                                .Where(m => m.Id == resourceAllocation.Id)
                                .FirstOrDefaultAsync();
                            resAlloc.StartDate = (DateOnly)finalStartDate;
                            resAlloc.EndDate = finalEndingDate;
                            _allocationDbContext.UnPublishedResAlloc.Update(resAlloc);
                            await _allocationDbContext.SaveChangesAsync();

                            var unPublishedResAllocDetails = await _allocationDbContext.UnPublishedResAllocDetails
                                .Where(m => m.Id == resourceAllocation.UnPublishedResAllocDetailsId)
                                .Include(m => m.ResourceAllocations)
                                .FirstOrDefaultAsync();
                            unPublishedResAllocDetails.EndDate = unPublishedResAllocDetails.ResourceAllocations.MaxBy(a => a.EndDate).EndDate;
                            unPublishedResAllocDetails.StartDate = unPublishedResAllocDetails.ResourceAllocations.MinBy(a => a.StartDate).StartDate;
                            _allocationDbContext.UnPublishedResAllocDetails.Update(unPublishedResAllocDetails);
                            await _allocationDbContext.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<Boolean> ResourceAllocationPerDaysUpdateAsync(ResourceAllocationResponse resourceAllocation, string empEmail, string jobId, GetHolidayLeaveResignedAbsconded? leaveResults = null)
        {
            try
            {
                DateOnly startDate = resourceAllocation.StartDate;
                DateOnly endDate = resourceAllocation.EndDate;
                Boolean isDeleted = await DeleteAllocatedDaysByResAllocId(resourceAllocation.Id, resourceAllocation.Type);
                var allocatedAllocations = await GetAllocatedDaysByEmailId(startDate, endDate, empEmail, true);
                Int64 requireAllocationHours = resourceAllocation.Efforts;
                if (requireAllocationHours > 0)
                {
                    var finalEndingDate = startDate;
                    DateOnly? finalStartDate = null;

                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        bool isHoliday = false;
                        GTLeaveBaseDTO currentLeaveDate = null;
                        if (leaveResults != null && leaveResults.HolidayResponseTask != null && leaveResults.HolidayResponseTask.Count > 0)
                            isHoliday = leaveResults.HolidayResponseTask.Where(holiday => DateOnly.FromDateTime(holiday.holiday_date.Date).Equals(date)).Any();
                        if (leaveResults != null && leaveResults.LeavesResponseTask != null && leaveResults.LeavesResponseTask.Count > 0)
                        {
                            currentLeaveDate = leaveResults.LeavesResponseTask.Where(leave => date == leave.leave_date).FirstOrDefault();
                        }

                        if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday && !isHoliday && !(currentLeaveDate != null && currentLeaveDate.leave_type == TimelineType.FULL_DAY_LEAVE))
                        {

                            finalEndingDate = date;

                            var allocatedHours = allocatedAllocations
                                .Where(a => a.AllocationDate == date)
                                .Sum(a => a.Efforts);
                            var todayWorkingHrs = Constants.WorkingHourPerDay;

                            if (currentLeaveDate != null && (currentLeaveDate.leave_type == TimelineType.FIRST_HALF_LEAVE || currentLeaveDate.leave_type == TimelineType.SECOND_HALF_LEAVE))
                            {
                                todayWorkingHrs = Constants.HalfDayHours;
                            }
                            //change
                            if (allocatedHours <= todayWorkingHrs || allocatedHours == null)
                            {
                                if (finalStartDate == null)
                                {
                                    finalStartDate = date;
                                }

                                if (resourceAllocation.Type == AllocationType.PUBLISHED)
                                {
                                    PublishedResAllocDays publishedDays = new()
                                    {
                                        AllocationDate = date,
                                        Efforts = requireAllocationHours,
                                        RequisitionId = resourceAllocation.RequisitionId,
                                        PublishedResAllocId = resourceAllocation.Id,
                                        Currency = resourceAllocation.Currency,
                                        EmailId = empEmail,
                                        JobCode = resourceAllocation.JobCode,
                                        PipelineCode = resourceAllocation.PipelineCode,
                                        RatePerHour = resourceAllocation.RatePerHour,
                                        JobId = String.IsNullOrEmpty(jobId) ? String.Empty : jobId
                                    };
                                    await _allocationDbContext.Set<PublishedResAllocDays>().AddAsync(publishedDays);
                                }
                                else
                                {
                                    UnPublishedResAllocDays unPublishedResAllocationDays = new()
                                    {
                                        AllocationDate = date,
                                        Efforts = requireAllocationHours,
                                        RequisitionId = resourceAllocation.RequisitionId,
                                        UnPublishedResAllocId = resourceAllocation.Id,
                                        EmailId = empEmail,
                                        JobCode = resourceAllocation.JobCode,
                                        PipelineCode = resourceAllocation.PipelineCode,
                                        RatePerHour = resourceAllocation.RatePerHour,
                                        JobId = String.IsNullOrEmpty(jobId) ? String.Empty : jobId
                                    };
                                    await _allocationDbContext.Set<UnPublishedResAllocDays>().AddAsync(unPublishedResAllocationDays);
                                }
                            }
                        }
                    }
                    if (finalStartDate != null)
                    {

                        if (resourceAllocation.Type == AllocationType.PUBLISHED)
                        {
                            var resAlloc = await _allocationDbContext.PublishedResAlloc
                                .Where(m => m.Id == resourceAllocation.Id)
                                .FirstOrDefaultAsync();
                            resAlloc.StartDate = (DateOnly)finalStartDate;
                            resAlloc.EndDate = finalEndingDate;
                            _allocationDbContext.PublishedResAlloc.Update(resAlloc);
                            await _allocationDbContext.SaveChangesAsync();

                            var publishedResAllocDetails = await _allocationDbContext.PublishedResAllocDetails
                                .Where(m => m.Id == resourceAllocation.PublishedResAllocDetailsId)
                                .Include(m => m.ResourceAllocations)
                                .FirstOrDefaultAsync();
                            publishedResAllocDetails.EndDate = publishedResAllocDetails.ResourceAllocations.MaxBy(a => a.EndDate).EndDate;
                            publishedResAllocDetails.StartDate = publishedResAllocDetails.ResourceAllocations.MinBy(a => a.StartDate).StartDate;
                            _allocationDbContext.PublishedResAllocDetails.Update(publishedResAllocDetails);
                            await _allocationDbContext.SaveChangesAsync();

                        }
                        else if (resourceAllocation.Type == AllocationType.UnPUBLISHED)
                        {
                            var resAlloc = await _allocationDbContext.UnPublishedResAlloc
                                .Where(m => m.Id == resourceAllocation.Id)
                                .FirstOrDefaultAsync();
                            resAlloc.StartDate = (DateOnly)finalStartDate;
                            resAlloc.EndDate = finalEndingDate;
                            _allocationDbContext.UnPublishedResAlloc.Update(resAlloc);
                            await _allocationDbContext.SaveChangesAsync();

                            var unPublishedResAllocDetails = await _allocationDbContext.UnPublishedResAllocDetails
                                .Where(m => m.Id == resourceAllocation.UnPublishedResAllocDetailsId)
                                .Include(m => m.ResourceAllocations)
                                .FirstOrDefaultAsync();
                            unPublishedResAllocDetails.EndDate = unPublishedResAllocDetails.ResourceAllocations.MaxBy(a => a.EndDate).EndDate;
                            unPublishedResAllocDetails.StartDate = unPublishedResAllocDetails.ResourceAllocations.MinBy(a => a.StartDate).StartDate;
                            _allocationDbContext.UnPublishedResAllocDetails.Update(unPublishedResAllocDetails);
                            await _allocationDbContext.SaveChangesAsync();
                        }
                    }
                    await _allocationDbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// Get Confirmed allocated per day hour between a date range including both dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ResourceAllocationDaysResponse>> GetConfirmedPerDayHoursByDate(DateTime startdate, DateTime enddate)
        {
            var _publishAlocationPerDay = await CommonAllocationHelper.GetUserPerDayPublishAllocationsByDates(_allocationDbContext, startdate, enddate);
            var _unPublicshAlocationPerDay = await CommonAllocationHelper.GetUserPerDayUnPublishAllocationsByDates(_allocationDbContext, startdate, enddate);
            var _publishResponse = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(_publishAlocationPerDay);
            var _unPublishResponse = await TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(_unPublicshAlocationPerDay);
            var joinedResult = _publishResponse.Concat(_unPublishResponse).ToList();

            joinedResult = joinedResult.Where(m => m.AllocationDate <= DateOnly.FromDateTime(enddate) && m.AllocationDate >= DateOnly.FromDateTime(enddate)).ToList();
            return joinedResult;


            //List<ResAllocationDays> res = await (from raDays in _allocationDbContext.ResourceAllocationDays
            //                                     join rad in _allocationDbContext.ResourceAllocationDetails
            //                                     on raDays.ResAllocDetailsId equals rad.Id
            //                                     where rad.IsActive == true && raDays.IsActive == true
            //                                     && raDays.ConfirmedAllocationStartDate.HasValue
            //                                     && (startDate.GetUniversalTime().Date <= raDays.ConfirmedAllocationStartDate.Value.Date
            //                                     && raDays.ConfirmedAllocationStartDate.Value.Date <= endDate.GetUniversalTime().Date)
            //                                     select raDays)
            //                                     .ToListAsync();

            //return res;

        }
        //Old
        //public async Task<List<ResAllocationDays>> GetAllocatedDaysByEmailId(DateTime startDate, DateTime endDate, string emailId)
        //{
        //    return await _allocationDbContext.ResourceAllocationDays
        //        .Where(a => a.EmpEmail == emailId
        //            && startDate.GetUniversalTime() <= a.ConfirmedAllocationStartDate
        //            && a.ConfirmedAllocationStartDate <= endDate.GetUniversalTime() 
        //            && a.IsActive == true)
        //        .ToListAsync();
        //}
        public async Task<List<ResourceAllocationDaysResponse>> GetAllocatedDaysByEmailId(DateOnly startDate, DateOnly endDate, string emailId, bool excludeDraft)
        {
            List<ResourceAllocationDaysResponse> response = new();

            //var unPublishedUserAllocationDetail = await _allocationDbContext.UnPublishedResAllocDetails
            //    .Where(m =>
            //        m.EmpEmail.ToLower().Trim() == emailId.ToLower().Trim()
            //        && startDate <= m.StartDate
            //        && m.EndDate <= endDate
            //     )
            //    .ToListAsync();
            var unpublishedDaysAllocated = await _allocationDbContext.UnPublishedResAllocDays
                .Include(m => m.ResAlloc.ResourceAllocationsDetails)
                .Where(m =>
                    m.EmailId.ToLower().Trim() == emailId.ToLower().Trim()
                    && startDate <= m.AllocationDate
                    && m.AllocationDate <= endDate
                    && (excludeDraft == false || (excludeDraft && m.ResAlloc != null && m.ResAlloc.ResourceAllocationsDetails != null && m.ResAlloc.ResourceAllocationsDetails.AllocationStatus != AllocationStatuses.DRAFT))
                 )
                .ToListAsync();
            var unPublishedResponses = await TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(unpublishedDaysAllocated);
            response.AddRange(unPublishedResponses);

            //var publishedUserAllocationDetail = await _allocationDbContext.PublishedResAllocDetails
            //   .Where(m =>
            //       m.EmpEmail.ToLower().Trim() == emailId.ToLower().Trim()
            //       && startDate <= m.StartDate
            //       && m.EndDate <= endDate
            //    )
            //   .ToListAsync();
            var publishedDaysAllocated = await _allocationDbContext.PublishedResAllocDays
                .Where(m =>
                    m.EmailId.ToLower().Trim() == emailId.ToLower().Trim()
                    && startDate <= m.AllocationDate
                    && m.AllocationDate <= endDate
                 )
                .ToListAsync();
            var publishedResponses = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(publishedDaysAllocated);
            response.AddRange(publishedResponses);
            return response;
        }
        public async Task<List<ResourceAllocationDaysResponse>> GetPublishedResourceAllocationDays(DateTime startDate, DateTime endDate)
        {
            var publishedDaysAllocated = await _allocationDbContext.PublishedResAllocDays.Where(m =>
                                                            m.AllocationDate >= DateOnly.FromDateTime(startDate)
                                                            && m.AllocationDate <= DateOnly.FromDateTime(endDate))
                                                      .ToListAsync();
            var publishedResponses = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(publishedDaysAllocated);
            return publishedResponses;
        }
        public async Task<List<ResourceAllocationDaysResponse>> GetPublishedResourceAllocationDays(List<string> employeeEmails, DateTime startDate, DateTime endDate)
        {
            var smallEmails = employeeEmails.Select(e => e.ToLower());
            var publishedDaysAllocated = await _allocationDbContext.PublishedResAllocDays.Where(m =>
                                                            smallEmails.Any(e => m.EmailId.ToLower() == e)
                                                            && (m.AllocationDate >= DateOnly.FromDateTime(startDate)
                                                            && m.AllocationDate <= DateOnly.FromDateTime(endDate))
                                                       )
                                                      .ToListAsync();
            var publishedResponses = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(publishedDaysAllocated);
            return publishedResponses;
        }
        /// <summary>
        /// CHECKING AVAILABLITY
        /// </summary>
        /// <param name="resourceAvailable"></param>
        /// <param name="holidayList"></param>
        /// <param name="leaveList"></param>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public async Task<ResourceAvailable> GetResourceAvailableHours(ResourceAvailable resourceAvailable, List<GTHolidayDTO> holidayList, List<GTLeaveBaseDTO> leaveList, string? pipelineCode, string? jobCode)
        {
            int totalHours = 0;

            string dateFormat = "dd-MM-yyyy";
            List<string> errorMsg = new();
            resourceAvailable.ErrorMsg = string.Empty;
            DateTime startDate = resourceAvailable.StartDate;
            DateTime endDate = resourceAvailable.EndDate;

            bool isAllocationDraft = false;
            if (resourceAvailable.RequisitionId != null)
            {
                var isADraftTypeAllocation = await _allocationDbContext.UnPublishedResAllocDetails
                    .Where(m =>
                        m.RequisitionId == resourceAvailable.RequisitionId
                        && m.AllocationStatus == AllocationStatuses.DRAFT
                        && m.IsActive == true
                    )
                    .AnyAsync();

                if (isADraftTypeAllocation == true)
                {
                    isAllocationDraft = true;
                }
            }

            if (pipelineCode != null || (pipelineCode != null && isAllocationDraft))
            {
                bool IsUserAlreadyAllocatedForSameProjectInBetweenDates = await this.IsUserAlreadyAllocatedForSameProjectInBetweenDates
                    (resourceAvailable.EmailId
                    , pipelineCode
                    , jobCode
                    , DateOnly.FromDateTime(startDate)
                    , DateOnly.FromDateTime(endDate)
                    , resourceAvailable.RequisitionId
                );


                if (IsUserAlreadyAllocatedForSameProjectInBetweenDates)
                {
                    resourceAvailable.IsHoursAvialable = false;
                    resourceAvailable.ErrorMsg = "The selected user is already allocated on this project, Kindly update user’s allocation from Project Listings or Allocation Detail Page";                    
                    return resourceAvailable;
                }
            }
            List<AllocationCommonView> resAllocationDays = await _allocationDbContext.allocation_common_view
                .Where(a => !string.IsNullOrEmpty(a.EmailId)
                && !string.IsNullOrEmpty(resourceAvailable.EmailId)
                && Convert.ToString(a.EmailId).ToLower() == Convert.ToString(resourceAvailable.EmailId).ToLower()
                && (resourceAvailable.RequisitionId == null || a.RequisitionId != resourceAvailable.RequisitionId)
                && DateOnly.FromDateTime(resourceAvailable.StartDate) <= a.AllocationDate
                && a.AllocationDate <= DateOnly.FromDateTime(resourceAvailable.EndDate)
                && a.AllocationStatus != AllocationStatuses.DRAFT
                )
                .ToListAsync();

            List<string> emailId = new() { resourceAvailable.EmailId.ToLower() };

            int totalWorkingDays = 0;
            bool isAvaiableHours = true;
            foreach (DateTime date in new Domain.Util.DateRange(startDate, endDate))
            {
                Int64 perDayHoursAvailble = 0;
                bool isHoliday = holidayList.Where(holiday => holiday.holiday_date.Date.Equals(date.Date)).Any();
                GTLeaveBaseDTO currentLeaveDate = leaveList.Where(leave => DateOnly.FromDateTime(date.Date) == leave.leave_date).FirstOrDefault();
                //bool isLeave = leaveList.Where(leave => date.Date >= leave.leave_start_date && date.Date <= leave.leave_end_date).Any();
                if (CommonUtil.isWeekdays(date) && !isHoliday && !(currentLeaveDate != null && currentLeaveDate.leave_type != null && currentLeaveDate.leave_type == TimelineType.FULL_DAY_LEAVE))
                {
                    perDayHoursAvailble = Constants.WorkingHourPerDay;
                    if (currentLeaveDate != null && currentLeaveDate.leave_type != null && (currentLeaveDate.leave_type == TimelineType.FIRST_HALF_LEAVE || currentLeaveDate.leave_type == TimelineType.SECOND_HALF_LEAVE))
                    {
                        perDayHoursAvailble -= Constants.HalfDayHours;
                    }
                    totalWorkingDays += 1;
                    if (resAllocationDays.Count > 0)
                    {
                        List<AllocationCommonView> lstResAllDays = resAllocationDays
                            .Where(a =>
                                a.AllocationDate == DateOnly.FromDateTime(date))
                            .ToList();
                        if (lstResAllDays.Count > 0)
                        {
                            Int64 allocatedhours = 0;
                            lstResAllDays.ForEach(item =>
                            {
                                allocatedhours += item.Efforts;
                            });
                            if (allocatedhours != 0)
                            {
                                perDayHoursAvailble -= allocatedhours;
                            }
                        }
                    }
                    if (resourceAvailable.isPerDayHourAllocation)
                    {
                        if (resourceAvailable.RequireWorkingHours > perDayHoursAvailble || perDayHoursAvailble <= 0)
                        {
                            errorMsg.Add(date.ToString(dateFormat));
                        }
                    }
                    totalHours += (int)perDayHoursAvailble;
                }
                else if (isHoliday || (currentLeaveDate != null && currentLeaveDate.leave_type == TimelineType.FULL_DAY_LEAVE))
                {
                    errorMsg.Add(date.ToString(dateFormat));
                }
                //else if (isHoliday || (currentLeaveDate != null && currentLeaveDate.leave_type == TimelineType.FULL_DAY_LEAVE))
                //{
                //    errorMsg.Add(date.ToString(dateFormat));
                //}
            }
            resourceAvailable.TotalAvaibleHours = totalHours;
            resourceAvailable.IsHoursAvialable = totalHours >= resourceAvailable.RequireWorkingHours;
            resourceAvailable.TotalWorkingDays = totalWorkingDays;
            if (errorMsg.Count > 0 && resourceAvailable.isPerDayHourAllocation)
            {
                resourceAvailable.IsHoursAvialable = false;
                resourceAvailable.TotalWorkingHours = totalWorkingDays * (int)resourceAvailable.RequireWorkingHours;
                resourceAvailable.ErrorMsg = String.Format(Constants.error_IsAvailableHour_PerDay,
                    resourceAvailable.RequireWorkingHours, string.Join(",", errorMsg));
            }
            else if (!resourceAvailable.isPerDayHourAllocation)
            {
                resourceAvailable.IsHoursAvialable = resourceAvailable.RequireWorkingHours <= totalHours;
                resourceAvailable.TotalWorkingHours += totalHours;
                resourceAvailable.ErrorMsg = resourceAvailable.RequireWorkingHours <= totalHours
                    ? ""
                    : String.Format(Constants.error_IsAvailableHour_TotalEffort, resourceAvailable.RequireWorkingHours);
            }
            else
            {
                resourceAvailable.IsHoursAvialable = totalHours >= resourceAvailable.RequireWorkingHours;
            }
            return resourceAvailable;
        }
        public async Task<bool> UpdatePublishAllocationActualEfforts(List<UpdatePublishAllocationActualEffortsRequestDTO> req)
        {
            foreach (var item in req)
            {
                if (item.EmployeeMID != null && item.EmpEmail != null && item.JobCode != null && item.DateLog != null)
                {
                    string empEmailId = $"{item.EmployeeMID}__{item.EmpEmail}";
                    var allocationDay = await _allocationDbContext.PublishedResAllocDays
                        .Where(t => (t.JobCode != null && t.JobCode.ToLower().Trim() == item.JobCode.ToLower().Trim())
                                                                          && t.EmailId.ToLower().Trim() == empEmailId.ToLower().Trim()
                                                                          && t.AllocationDate == DateOnly.FromDateTime((DateTime)item.DateLog))
                        .FirstOrDefaultAsync();
                    if (allocationDay != null)
                    {
                        allocationDay.ActualEffort = item.TotalTime;
                        _allocationDbContext.PublishedResAllocDays.Update(allocationDay);
                    }
                    else
                    {
                        allocationDay = await _allocationDbContext.PublishedResAllocDays
                            .Where(t => (t.JobCode != null && t.JobCode.ToLower().Trim() == item.JobCode.ToLower().Trim())
                             && t.EmailId.ToLower().Trim() == empEmailId.ToLower().Trim()).FirstOrDefaultAsync();
                        if (allocationDay != null)
                        {
                            //*********************** Add Publish Allocation Days ***********************************/
                            var flag = await AddPublishAllocationDays(item, allocationDay);
                            //**************************** Publish Allocation Details *************************************//
                            var publishAllocationDetails = await _allocationDbContext.PublishedResAllocDetails
                                        .Where(a => a.RequisitionId == allocationDay.RequisitionId).FirstOrDefaultAsync();
                            if (publishAllocationDetails != null)
                            {
                                if (publishAllocationDetails.StartDate > DateOnly.FromDateTime((DateTime)item.DateLog))
                                {
                                    publishAllocationDetails.ActualStartDate = DateOnly.FromDateTime((DateTime)item.DateLog);
                                }
                                else if (publishAllocationDetails.EndDate < DateOnly.FromDateTime((DateTime)item.DateLog))
                                {
                                    publishAllocationDetails.ActualEndDate = DateOnly.FromDateTime((DateTime)item.DateLog);

                                }
                                else if (publishAllocationDetails.ActualStartDate == null || publishAllocationDetails.ActualEndDate == null)
                                {
                                    publishAllocationDetails.ActualStartDate = publishAllocationDetails.StartDate;
                                    publishAllocationDetails.ActualEndDate = publishAllocationDetails.EndDate;
                                }
                                _allocationDbContext.PublishedResAllocDetails.Update(publishAllocationDetails);
                            }
                        }
                    }

                }
            }
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }
        
       
        public async Task<Boolean> AddPublishAllocationDays (UpdatePublishAllocationActualEffortsRequestDTO item, PublishedResAllocDays publishReshAllocDays )
        {
            publishReshAllocDays.Id = new Guid();
            publishReshAllocDays.Efforts = 0;
            publishReshAllocDays.ActualEffort = (long)item.TotalTime;
            publishReshAllocDays.AllocationDate = DateOnly.FromDateTime((DateTime)item.DateLog);
            _allocationDbContext.PublishedResAllocDays.Add(publishReshAllocDays);
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Boolean> DeleteAllocatedDaysByResAllocId(Guid resourceAllocationId, string type)
        {
            if (type == AllocationType.UnPUBLISHED)
            {
                var unPublishedResAllocationDays = await _allocationDbContext.UnPublishedResAllocDays
                    .Where(a => a.UnPublishedResAllocId == resourceAllocationId).ToListAsync();
                if (unPublishedResAllocationDays.Count > 0)
                {
                    _allocationDbContext.UnPublishedResAllocDays.RemoveRange(unPublishedResAllocationDays);
                    await _allocationDbContext.SaveChangesAsync();
                }
                return true;
            }
            else if (type == AllocationType.PUBLISHED)
            {
                var publishedResAllocationDays = await _allocationDbContext.PublishedResAllocDays
                    .Where(a => a.PublishedResAllocId == resourceAllocationId).ToListAsync();
                if (publishedResAllocationDays.Count > 0)
                {
                    _allocationDbContext.PublishedResAllocDays.RemoveRange(publishedResAllocationDays);
                    await _allocationDbContext.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }
        public async Task<List<UsersAvailability>> isUserAvailable(UsersAvailabilityCheckDTO usersAvailabilityCheck)
        {
            List<UsersAvailability> availabilities = new() { };

            NpgsqlConnection npgsqlConnection = null;
            try
            {
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();
                npgsqlConnection = new NpgsqlConnection(pgsqlConnection);
                using (NpgsqlCommand command = new(Constants.UserAvailabilitySP, npgsqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var employeesParam = new NpgsqlParameter(Constants.employee_details, NpgsqlDbType.Text | NpgsqlDbType.Array)
                    {
                        Value = usersAvailabilityCheck.emails,
                    };
                    command.Parameters.Add(employeesParam);
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true
                    };
                    var leaves = new NpgsqlParameter(Constants.Leaves, NpgsqlDbType.Jsonb)
                    {
                        Value = System.Text.Json.JsonSerializer.Serialize(usersAvailabilityCheck.leaves, options),
                    };
                    command.Parameters.Add(leaves);
                    //command.Parameters.AddWithValue(Constants.Leaves, usersAvailabilityCheck.leaves);

                    command.Parameters.AddWithValue(Constants.StartDate, usersAvailabilityCheck.start_date);
                    command.Parameters.AddWithValue(Constants.EndDate, usersAvailabilityCheck.end_date);
                    command.Parameters.AddWithValue(Constants.TotalRequiredHours, usersAvailabilityCheck.total_required_hours);
                    command.Parameters.AddWithValue(Constants.PerDayMaxEffort, usersAvailabilityCheck.perday_max_effort);
                    var outputResult = new NpgsqlParameter(Constants.UserAvailabilitySPResponse, NpgsqlDbType.Jsonb)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputResult);
                    npgsqlConnection.Open();
                    command.ExecuteNonQuery();
                    var jsonResult = command.Parameters[Constants.UserAvailabilitySPResponse].Value.ToString();
                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        var parsedData = JsonConvert.DeserializeObject<JObject>(jsonResult);
                        foreach (var property in parsedData.Properties())
                        {
                            UsersAvailability userAvailability = new()
                            {
                                email = property.Name,
                                available = property.Value.ToObject<bool>(),
                            };
                            availabilities.Add(userAvailability);
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
            return availabilities;
        }
        public async Task<List<ResourceAllocationDaysResponse>> GetUserPerDayAllocationsByEmailAndDates(List<string> emails, DateTime startdate, DateTime enddate)
        {
            var _publishAlocationPerDay = await CommonAllocationHelper.GetUserPerDayPublishAllocationsByEmailAndDates(_allocationDbContext, emails, startdate, enddate);
            var _unPublicshAlocationPerDay = await CommonAllocationHelper.GetUserPerDayUnPublishAllocationsByEmailAndDates(_allocationDbContext, emails, startdate, enddate);
            var _publishResponse = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(_publishAlocationPerDay);
            var _unPublishResponse = await TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(_unPublicshAlocationPerDay);
            var joinedResult = _publishResponse.Concat(_unPublishResponse).ToList();

            joinedResult = joinedResult.Where(m => m.AllocationDate <= DateOnly.FromDateTime(enddate) && m.AllocationDate >= DateOnly.FromDateTime(startdate)).ToList();
            var filteredAllocations = joinedResult
                .GroupBy(a => new { a.RequisitionId, a.AllocationDate })
                .Select(g => g.OrderByDescending(a => a.Efforts).First())
                .ToList();
            return filteredAllocations;
        }
        public async Task<List<ResourceAllocationResponse>> GetUserAllocationsByEmailAndDates(List<string> emails, DateTime startdate, DateTime enddate)
        {
            List<string> emailsList = emails.Select(s => s.ToLower().Trim()).ToList();
            List<PublishedResAlloc> _resourceAllocations = await (
               from resourceAllocated in _allocationDbContext.PublishedResAlloc
               join resourceDetails in _allocationDbContext.PublishedResAllocDetails
               on resourceAllocated.PublishedResAllocDetailsId equals resourceDetails.Id
               where
               emailsList.ToArray().Any(p => resourceDetails.EmpEmail.ToLower().Equals(p))
               && resourceDetails.StartDate <= DateOnly.FromDateTime(enddate)
               && resourceDetails.EndDate >= DateOnly.FromDateTime(startdate)
               && resourceDetails.IsActive == true
               select resourceAllocated
           ).ToListAsync();
            var resp = await TransformPublishedResAllocIntoResourceAllocationResponse(_resourceAllocations);
            return resp;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> GetUserAllocationDetailsByEmailAndDates(List<string> emails, DateTime? startdate, DateTime? enddate, List<string>? PipelineCodes, bool? CheckUnPublishedAsWell = false)
        {
            List<string> emailsList = emails.Select(s => s.ToLower().Trim()).ToList();
            if (startdate == null || enddate == null)
            {
                var publishedResponse = await _allocationDbContext.PublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Include(m => m.ResourceAllocations)
                    .Where(m =>
                        emailsList.ToArray()
                            .Any(p => m.EmpEmail.ToLower().Trim().Equals(p))
                        && (
                            (PipelineCodes == null)
                            ||
                            ((PipelineCodes != null && (PipelineCodes.Contains(m.Requisition.PipelineCode)
                            || (PipelineCodes.Contains(m.Requisition.JobCode)))))
                        )
                        && m.IsActive == true
                    )
                    .ToListAsync();
                if (publishedResponse != null)
                {
                    var transformedResult = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedResponse);
                    return transformedResult;
                }
                if (CheckUnPublishedAsWell == true)
                {
                    var unPublishedResponse = await _allocationDbContext.UnPublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Include(m => m.ResourceAllocations)
                    .Where(m =>
                        emailsList.ToArray()
                            .Any(p => m.EmpEmail.ToLower().Trim().Equals(p))
                        && (
                            (PipelineCodes == null)
                            ||
                            ((PipelineCodes != null && (PipelineCodes.Contains(m.Requisition.PipelineCode)
                            || (PipelineCodes.Contains(m.Requisition.JobCode)))))
                        )
                        && m.IsActive == true
                    )
                    .ToListAsync();
                    if (unPublishedResponse != null)
                    {
                        var transformedResult = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(unPublishedResponse);
                        return transformedResult;
                    }
                }
                return null;
            }
            else
            {
                var _startdate = DateOnly.FromDateTime((DateTime)startdate);
                var _enddate = DateOnly.FromDateTime((DateTime)enddate);
                var publishedResponse = await _allocationDbContext.PublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Include(m => m.ResourceAllocations)
                    .Where(m =>
                        emailsList.ToArray()
                            .Any(p => m.EmpEmail.ToLower().Trim().Equals(p))
                        && (
                                (_startdate < m.StartDate && _enddate >= m.StartDate) ||
                                (_startdate >= m.StartDate && _enddate <= m.EndDate) ||
                                (_startdate >= m.StartDate && _startdate < m.EndDate)
                        )
                        && (
                                (PipelineCodes == null)
                                ||
                                ((PipelineCodes != null && (PipelineCodes.Contains(m.Requisition.PipelineCode)
                                || (PipelineCodes.Contains(m.Requisition.JobCode)))))
                        )
                        && m.IsActive == true
                    )
                    .ToListAsync();
                if (publishedResponse != null)
                {
                    var transformedResult = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedResponse);
                    return transformedResult;
                }
                if (CheckUnPublishedAsWell == true)
                {
                    var unPublishedResponse = await _allocationDbContext.UnPublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Include(m => m.ResourceAllocations)
                    .Where(m =>
                        emailsList.ToArray()
                            .Any(p => m.EmpEmail.ToLower().Trim().Equals(p))
                        && (
                            (PipelineCodes == null)
                            ||
                            ((PipelineCodes != null && (PipelineCodes.Contains(m.Requisition.PipelineCode)
                            || (PipelineCodes.Contains(m.Requisition.JobCode)))))
                        )
                        && m.IsActive == true
                    )
                    .ToListAsync();
                    if (unPublishedResponse != null)
                    {
                        var transformedResult = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(unPublishedResponse);
                        return transformedResult;
                    }
                }
                return null;
            }
        }
        public async Task<List<ResourceAllocationResponse>> GetAllocationByEmailAndLeaveStartDateAndEndDate(string email, DateTime leave_start_date, DateTime leave_end_date)
        {
            var _leave_start_date = DateOnly.FromDateTime(leave_end_date);
            var _leave_end_date = DateOnly.FromDateTime(leave_end_date);

            var allocationsListWithLeaves = await (from resourceAllocated in _allocationDbContext.PublishedResAlloc
                                                   join resourceDetails in _allocationDbContext.PublishedResAllocDetails
                                                    on resourceAllocated.PublishedResAllocDetailsId equals resourceDetails.Id
                                                   where Convert.ToString(resourceDetails.EmpEmail).Trim().ToLower() == Convert.ToString(email).Trim().ToLower()
                                                   && (
                                                (resourceDetails.StartDate <= _leave_start_date && resourceDetails.EndDate >= _leave_end_date)
                                                || (resourceDetails.StartDate >= _leave_start_date && resourceDetails.EndDate >= _leave_end_date && resourceDetails.StartDate <= _leave_end_date)
                                                || (resourceDetails.StartDate <= _leave_start_date && resourceDetails.EndDate <= _leave_end_date && resourceDetails.StartDate >= _leave_end_date)
                                                || (resourceDetails.StartDate >= _leave_start_date && resourceDetails.EndDate <= _leave_end_date)
                                                )
                                                   select resourceAllocated)
                                                .ToListAsync();
            var resp = await TransformPublishedResAllocIntoResourceAllocationResponse(allocationsListWithLeaves);
            return resp;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> GetAllocationByJobCodeHandler(List<string> jobCodes)
        {
            //Old
            //return await _allocationDbContext.ResourceAllocation.Where(m =>
            //            jobCodes.ToArray().Any(p => m.JobCode.Equals(p))
            //            && m.IsActive == true
            //        )
            //    .ToListAsync();

            var publishedRecords = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Skills)
                .Include(m => m.Requisition)
                .Where(m =>
                        jobCodes.ToArray().Any(p => m.Requisition.JobCode.Equals(p))
                        && m.IsActive == true
                    )
                .ToListAsync();
            var response = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedRecords);
            return response;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(List<PublishedResAllocDetails> publishedResAllocDetails)
        {
            List<ResourceAllocationDetailsResponse> response = new();
            foreach (var item in publishedResAllocDetails)
            {
                var skills = await TransformPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(item?.Skills);
                var publishedResAllocation = item.ResourceAllocations != null && item.ResourceAllocations.Count > 0 ? await TransformPublishedResAllocIntoResourceAllocationResponse(item.ResourceAllocations) : null;
                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);
                response.Add(new ResourceAllocationDetailsResponse()
                {
                    Skills = skills,
                    StartDate = item.StartDate,
                    EmpEmail = item.EmpEmail,
                    AllocationStatus = item.AllocationStatus,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    Description = item.Description,
                    EmpName = item.EmpName,
                    EndDate = item.EndDate,
                    Id = item.Id,
                    IsActive = item.IsActive,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedBy,
                    Designation = requisition.Designation,
                    //Competency = requisition.Competency,
                    Grade = requisition.Grade,
                    RequisitionId = item.RequisitionId,
                    Requisition = item.Requisition,
                    TotalEffort = item.TotalEffort,
                    AllocationVersion = item.AllocationVersion,
                    ConfirmedAllocationDate = item.ConfirmedAllocationDate,
                    ResourceAllocations = publishedResAllocation,
                    Type = AllocationType.PUBLISHED,
                    PipelineCode = requisition.PipelineCode,
                    PipelineName = requisition.PipelineName,
                    JobCode = requisition.JobCode,
                    JobName = requisition.JobName,
                    IsUpdated = item.IsUpdated
                });
            }
            return response;
        }
        private async Task<Requisition> getRequisitionById(Requisition item, Guid requisitionId)
        {
            var requisition = new Requisition();
            if (item != null)
            {
                requisition = item;
            }
            else
            {
                requisition = await _allocationDbContext.Requisition
                    .Include(m => m.RequisitionType)
                    .Where(m => m.Id == requisitionId && m.IsActive == true)
                    .FirstOrDefaultAsync();
            }

            return requisition;
        }
        private async Task<List<PublishedResAllocSkillEntity>> getPublishedResAllocSkillEntity(Guid requisitionId)
        {
            return await _allocationDbContext.PublishedResAllocSkillEntity                    
                    .Where(m => m.RequisitionId == requisitionId)
                    .ToListAsync();            
        }
        public async Task<List<ResourceAllocationDaysResponse>> TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(List<PublishedResAllocDays> publishedResAllocDays)
        {

            List<ResourceAllocationDaysResponse> response = new();
            foreach (var item in publishedResAllocDays)
            {

                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);

                response.Add(new ResourceAllocationDaysResponse()
                {
                    AllocationDate = item.AllocationDate,
                    Efforts = item.Efforts,
                    Id = item.Id,
                    RequisitionId = item.RequisitionId,
                    PublishedResAllocId = item.PublishedResAllocId,
                    Type = AllocationType.PUBLISHED,
                    PipelineCode = item.PipelineCode,
                    PipelineName = requisition != null ? requisition.PipelineName : string.Empty,
                    JobCode = item.JobCode,
                    JobName = requisition != null ? requisition.JobName : string.Empty,
                    EmailId = item.EmailId,
                    Currency = item.Currency,
                    RatePerHour = item.RatePerHour,
                });
            }
            return response;
        }
        public async Task<List<ResourceAllocationResponse>> TransformPublishedResAllocIntoResourceAllocationResponse(List<PublishedResAlloc> publishedResAlloc)
        {
            List<ResourceAllocationResponse> response = new();
            foreach (var item in publishedResAlloc)
            {
                var ResourceAllocationDays = item.ResourceAllocationsDays != null && item.ResourceAllocationsDays.Count > 0
                    ? await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(item.ResourceAllocationsDays)
                    : null;
                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);
                List<PublishedResAllocSkillEntity>? PublishedResAllocSkillEntity = await getPublishedResAllocSkillEntity(item.RequisitionId);
                if (item.ResourceAllocationsDetails == null)
                {
                    //NewlyAdded
                    item.ResourceAllocationsDetails = await GetPublishedResAllocDetailsById(item.PublishedResAllocDetailsId);
                }

                response.Add(new ResourceAllocationResponse()
                {
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Currency = item.Currency,
                    Efforts = item.Efforts,
                    IsPerDayAllocation = item.IsPerDayAllocation,
                    RatePerHour = item.RatePerHour,
                    RequisitionId = item.RequisitionId,
                    TotalWorkingDays = item.TotalWorkingDays,
                    Id = item.Id,
                    PublishedResAllocDetailsId = item.PublishedResAllocDetailsId,
                    ResourceAllocationDays = ResourceAllocationDays,
                    Type = AllocationType.PUBLISHED,
                    PipelineCode = (requisition != null && !string.IsNullOrEmpty(requisition.PipelineCode)) ? requisition.PipelineCode : string.Empty,
                    PipelineName = (requisition != null && !string.IsNullOrEmpty(requisition.PipelineName)) ? requisition.PipelineName : string.Empty,
                    JobCode = requisition != null ? requisition.JobCode : string.Empty,
                    JobName = requisition != null ? requisition.JobName : string.Empty,
                    Requisition = item.Requisition,
                    PublishedResAllocSkillEntity = PublishedResAllocSkillEntity,
                    EmpEmail = item.ResourceAllocationsDetails != null ? item.ResourceAllocationsDetails.EmpEmail : string.Empty
                });
            }
            return response;
        }
        public async Task<List<ResourceAllocationSkillsResponse>> TransformPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(List<PublishedResAllocSkillEntity>? publishedResAllocSkillEntity)
        {
            List<ResourceAllocationSkillsResponse> response = new();
            if (publishedResAllocSkillEntity != null)
            {

                foreach (var item in publishedResAllocSkillEntity)
                {
                    //var resAllocDetails = item.ResAllocDetails != null ? await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<PublishedResAllocDetails> { item.ResAllocDetails }) : null;

                    response.Add(new ResourceAllocationSkillsResponse()
                    {
                        SkillCode = item.SkillCode,
                        SkillName = item.SkillName,
                        Id = item.Id,
                        RequisitionId = item.RequisitionId,
                        PublishedResAllocDetailsId = item.PublishedResAllocDetailsId,
                        //Requisition = item?.Requisition,
                        //ResAllocDetails = resAllocDetails.FirstOrDefault(),
                        Type = AllocationType.PUBLISHED,

                    });
                }
            }
            return response;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(List<UnPublishedResAllocDetails> unPublishedResAllocDetails)
        {
            List<ResourceAllocationDetailsResponse> response = new();
            foreach (var item in unPublishedResAllocDetails)
            {
                var skills = await TransformUnPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(item.Skills);
                var unPublishedResAllocation = item.ResourceAllocations != null && item.ResourceAllocations.Count > 0 ? await TransformUnPublishedResAllocIntoResourceAllocationResponse(item.ResourceAllocations) : null;
                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);
                response.Add(new ResourceAllocationDetailsResponse()
                {
                    Skills = skills,
                    StartDate = item.StartDate,
                    EmpEmail = item.EmpEmail,
                    AllocationStatus = item.AllocationStatus,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    Description = item.Description,
                    EmpName = item.EmpName,
                    EndDate = item.EndDate,
                    Id = item.Id,
                    IsActive = item.IsActive,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedBy,
                    Designation = requisition.Designation,
                    //Competency = requisition.Competency,
                    Grade = requisition.Grade,
                    RequisitionId = item.RequisitionId,
                    ParentPublishedResAllocDetailsId = item.ParentPublishedResAllocDetailsId,
                    Requisition = item.Requisition,
                    TotalEffort = item.TotalEffort,
                    ResourceAllocations = unPublishedResAllocation,
                    AllocationVersion = item.AllocationVersion,
                    Type = AllocationType.UnPUBLISHED,
                    PipelineCode = requisition != null ? requisition.PipelineCode : string.Empty,
                    PipelineName = requisition != null ? requisition.PipelineName : string.Empty,
                    JobCode = requisition != null ? requisition.JobCode : string.Empty,
                    JobName = requisition != null ? requisition.JobName : string.Empty,
                });
            }
            return response;
        }
        public async Task<List<ResourceAllocationDaysResponse>> TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(List<UnPublishedResAllocDays> UnpublishedResAllocDays)
        {
            List<ResourceAllocationDaysResponse> response = new();
            foreach (var item in UnpublishedResAllocDays)
            {
                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);
                response.Add(new ResourceAllocationDaysResponse()
                {
                    AllocationDate = item.AllocationDate,
                    Efforts = item.Efforts,
                    Id = item.Id,
                    RequisitionId = item.RequisitionId,
                    UnPublishedResAllocId = item.UnPublishedResAllocId,
                    //Requisition = item?.Requisition,
                    Type = AllocationType.UnPUBLISHED,
                    PipelineCode = requisition != null ? requisition.PipelineCode : string.Empty,
                    PipelineName = requisition != null ? requisition.PipelineName : string.Empty,
                    JobCode = item.JobCode,
                    JobName = requisition != null ? requisition.JobName : string.Empty,
                    EmailId = item.EmailId,
                    Currency = item.Currency,
                    RatePerHour = item.RatePerHour,
                });
            }
            return response;
        }
        public async Task<List<ResourceAllocationResponse>> TransformUnPublishedResAllocIntoResourceAllocationResponse(List<UnPublishedResAlloc> UnpublishedResAlloc)
        {
            List<ResourceAllocationResponse> response = new();
            foreach (var item in UnpublishedResAlloc)
            {
                var ResourceAllocationDays = item.ResourceAllocationsDays != null && item.ResourceAllocationsDays.Count > 0
                    ? await TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(item.ResourceAllocationsDays)
                    : null;
                Requisition? requisition = await getRequisitionById(item.Requisition, item.RequisitionId);
                response.Add(new ResourceAllocationResponse()
                {
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Currency = item.Currency,
                    Efforts = item.Efforts,
                    IsPerDayAllocation = item.IsPerDayAllocation,
                    RatePerHour = item.RatePerHour,
                    RequisitionId = item.RequisitionId,
                    TotalWorkingDays = item.TotalWorkingDays,
                    Id = item.Id,
                    UnPublishedResAllocDetailsId = item.UnPublishedResAllocDetailsId,
                    ResourceAllocationDays = ResourceAllocationDays,
                    Type = AllocationType.UnPUBLISHED,
                    PipelineCode = requisition != null ? requisition.PipelineCode : string.Empty,
                    PipelineName = requisition != null ? requisition.PipelineName : string.Empty,
                    JobCode = requisition != null ? requisition.JobCode : string.Empty,
                    JobName = requisition != null ? requisition.JobName : string.Empty,
                    //Requisition = item.Requisition,
                    EmpEmail = item.ResourceAllocationsDetails.EmpEmail
                });
            }
            return response;
        }
        public async Task<List<ResourceAllocationSkillsResponse>> TransformUnPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(List<UnPublishedResAllocSkillEntity> UnpublishedResAllocSkillEntity)
        {
            List<ResourceAllocationSkillsResponse> response = new();
            foreach (var item in UnpublishedResAllocSkillEntity)
            {
                //var resAllocDetails = item.ResAllocDetails != null ? await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<UnPublishedResAllocDetails> { item.ResAllocDetails }) : null;
                response.Add(new ResourceAllocationSkillsResponse()
                {
                    SkillCode = item.SkillCode,
                    SkillName = item.SkillName,
                    Id = item.Id,
                    RequisitionId = item.RequisitionId,
                    UnPublishedResAllocDetailsId = item.UnPublishedResAllocDetailsId,
                    //Requisition = item?.Requisition,
                    //ResAllocDetails = resAllocDetails.FirstOrDefault(),
                    Type = AllocationType.UnPUBLISHED,
                });
            }
            return response;
        }
        public async Task<ResourceAllocationDetailsResponse> GetAllocationByGuidHandler(Guid guid, bool? discardInactiveRecords = true)
        {
            try
            {
                var unpublishedRecord = await _allocationDbContext.UnPublishedResAllocDetails
                    .Where(m => m.Id == guid
                            && (
                                (discardInactiveRecords == true && m.IsActive == true)
                                || discardInactiveRecords == false
                            ))
                    .Include(m => m.Requisition)
                    .Include(m => m.Requisition.RequisitionType)
                    .Include(m => m.ResourceAllocations)
                    .Include(m => m.Skills)
                     .FirstOrDefaultAsync();

                if (unpublishedRecord != null)
                {
                    var response = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<UnPublishedResAllocDetails> { unpublishedRecord });
                    return response.FirstOrDefault();
                }

                var publishedRecord = await _allocationDbContext.PublishedResAllocDetails
                    .Where(m => m.Id == guid
                            && (
                                (discardInactiveRecords == true && m.IsActive == true)
                                || discardInactiveRecords == false
                            ))
                    .Include(m => m.Requisition)
                    .Include(m => m.Skills)
                     .FirstOrDefaultAsync();

                if (publishedRecord != null)
                {
                    var response = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<PublishedResAllocDetails> { publishedRecord });
                    return response.FirstOrDefault();
                }

                return new ResourceAllocationDetailsResponse();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        //public async Task<Boolean> DeleteAllocationByRequisitionId(List<Guid> requisitionIds)
        //{
        //    foreach (var requisitionId in requisitionIds)
        //    {
        //        List<ResourceAllocationDetails> resourceAllocationDetails = await _allocationDbContext.ResourceAllocationDetails.AsNoTracking()
        //            .Where(m => (m.RequisitionId == requisitionId))
        //            .Include(m => m.ResourceAllocationSkillEntity)
        //            .ToListAsync();

        //        foreach (var radItem in resourceAllocationDetails)
        //        {
        //            radItem.IsActive = false;
        //            _allocationDbContext.ResourceAllocationDetails.Update(radItem);
        //        }

        //        List<ResourceAllocation> resourceAllocation = await _allocationDbContext.ResourceAllocation.AsNoTracking().Where(m => (m.RequisitionId == requisitionId)).ToListAsync();

        //        foreach (var raItem in resourceAllocation)
        //        {
        //            raItem.IsActive = false;
        //            _allocationDbContext.ResourceAllocation.Update(raItem);
        //        }

        //        //List<ResourceAllocationSkills> resourceAllocationSkills = await _allocationDbContext.ResourceAllocationSkills.AsNoTracking().Where(m => (m.RequisitionId == requisitionId)).ToListAsync();

        //        //foreach (var rasItem in resourceAllocation)
        //        //{
        //        //    rasItem.IsActive = false;
        //        //    _allocationDbContext.ResourceAllocation.Update(rasItem);
        //        //}

        //        List<ResAllocationDays> resourceAllocationDays = await _allocationDbContext.ResourceAllocationDays.AsNoTracking().Where(m => (m.RequisitionId == requisitionId)).ToListAsync();

        //        foreach (var radayItem in resourceAllocation)
        //        {
        //            radayItem.IsActive = false;
        //            _allocationDbContext.ResourceAllocation.Update(radayItem);
        //        }

        //        Requisition requestion = await _allocationDbContext.Requisition.AsNoTracking().Where(req => req.RequisionId == requisitionId).FirstAsync();
        //        requestion.RequisitionStatus = RequisitionStatus.AllocationPending.ToString();
        //        _allocationDbContext.Requisition.Update(requestion);
        //    }

        //    await _allocationDbContext.SaveChangesAsync();

        //    return await Task.FromResult(true);

        //}
        public async Task<Boolean> SuspendAllocationPerDay(List<KeyValuePair<string, string>> projectCodes)
        {
            try
            {
                var projectCodeList = projectCodes.Select(p => p.Key.ToLower().Trim()).ToList();

                DateOnly dateToday = DateOnly.FromDateTime(DateTime.UtcNow);
                var publishedResAllocDays = await _allocationDbContext.PublishedResAllocDays
                    .Include(m => m.Requisition)
                    .Where(m =>
                        projectCodeList.Contains(m.Requisition.PipelineCode.ToLower().Trim())
                        && m.AllocationDate > dateToday
                    )
                    .ToListAsync();
                if (publishedResAllocDays != null && publishedResAllocDays.Count > 0)
                {
                    _allocationDbContext.PublishedResAllocDays.RemoveRange(publishedResAllocDays);
                }

                var unPublishedResAllocDays = await _allocationDbContext.UnPublishedResAllocDays
                    .Include(m => m.Requisition)
                    .Where(m =>
                        projectCodeList.Contains(m.Requisition.PipelineCode.ToLower().Trim())
                        && m.AllocationDate > dateToday
                    )
                    .ToListAsync();
                if (unPublishedResAllocDays != null && unPublishedResAllocDays.Count > 0)
                {
                    _allocationDbContext.UnPublishedResAllocDays.RemoveRange(unPublishedResAllocDays);
                }
                await _allocationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Boolean> SuspendAllocations(List<KeyValuePair<string, string>> projectCodes)
        {
            try
            {
                var projectCodeList = projectCodes
                    .Select(p => p.Key.ToLower().Trim())
                    .ToList();

                DateTime dateToday = (DateOnly.FromDateTime(DateTime.UtcNow)).ToDateTime(TimeOnly.MinValue);
                var publishedResAllocations = await _allocationDbContext.PublishedResAlloc
                    .Include(m => m.ResourceAllocationsDetails)
                    .Include(m => m.Requisition)
                    .Where(m =>
                             projectCodeList.Contains(m.Requisition.PipelineCode.ToLower())
                    )
                    .ToListAsync();
                if (publishedResAllocations != null)
                {
                    foreach (var item in publishedResAllocations)
                    {
                        int isAllocationStarted = DateTime.Compare(dateToday, item.StartDate.ToDateTime(TimeOnly.MinValue));
                        if (isAllocationStarted > 0)
                        {
                            item.EndDate = DateOnly.FromDateTime(dateToday);
                            _allocationDbContext.Update(item);
                        }
                        else
                        {
                            _allocationDbContext.PublishedResAlloc.Remove(item);
                        }
                    }
                }

                var unPublishedResAllocations = await _allocationDbContext.UnPublishedResAlloc
                    .Include(m => m.ResourceAllocationsDetails)
                    .Include(m => m.Requisition)
                    .Where(m =>
                             projectCodeList.Contains(m.Requisition.PipelineCode.ToLower())
                    )
                    .ToListAsync();
                if (unPublishedResAllocations != null)
                {
                    foreach (var item in unPublishedResAllocations)
                    {
                        int isAllocationStarted = DateTime.Compare(dateToday, item.StartDate.ToDateTime(TimeOnly.MinValue));
                        if (isAllocationStarted > 0)
                        {
                            item.EndDate = DateOnly.FromDateTime(dateToday);
                            _allocationDbContext.Update(item);
                        }
                        else
                        {
                            _allocationDbContext.UnPublishedResAlloc.Remove(item);
                        }
                    }
                }
                await _allocationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<SuspendAllocationResponse>> SuspendAllocationsDetails(List<KeyValuePair<string, string>> projectCodes)
        {
            try
            {
                List<SuspendAllocationResponse> response = new();
                var projectCodeList = projectCodes.Select(p => p.Key.ToLower().Trim()).ToList();
                DateTime dateToday = (DateOnly.FromDateTime(DateTime.UtcNow)).ToDateTime(TimeOnly.MinValue);

                var publishedResDetails = await _allocationDbContext.PublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Where(m => m.IsActive == true && projectCodeList.Contains(m.Requisition.PipelineCode.Trim().ToLower()))
                    .ToListAsync();
                if (publishedResDetails != null)
                {
                    foreach (var resource in publishedResDetails)
                    {
                        resource.IsActive = false;
                        if (resource.StartDate != null && resource.EndDate != null)
                        {
                            int isProjectStarted = DateTime.Compare(dateToday, resource.StartDate.ToDateTime(TimeOnly.MinValue));
                            if (isProjectStarted > 0)
                            {
                                resource.EndDate = DateOnly.FromDateTime(dateToday);
                                resource.IsActive = true;
                            }
                        }
                        response.Add(new SuspendAllocationResponse()
                        {
                            EmpEmail = resource.EmpEmail,
                            JobCode = resource.Requisition.JobCode,
                            PipelineCode = resource.Requisition.PipelineCode
                        });
                    }
                    _allocationDbContext.PublishedResAllocDetails.UpdateRange(publishedResDetails.ToArray());
                }

                var unPublishedResDetails = await _allocationDbContext.UnPublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Where(m => m.IsActive == true && projectCodeList.Contains(m.Requisition.PipelineCode.Trim().ToLower()))
                    .ToListAsync();
                if (unPublishedResDetails != null)
                {
                    foreach (var resource in unPublishedResDetails)
                    {
                        resource.IsActive = false;
                        if (resource.StartDate != null && resource.EndDate != null)
                        {
                            int isProjectStarted = DateTime.Compare(dateToday, resource.StartDate.ToDateTime(TimeOnly.MinValue));
                            if (isProjectStarted > 0)
                            {
                                resource.EndDate = DateOnly.FromDateTime(dateToday);
                                resource.IsActive = true;
                            }
                        }
                        response.Add(new SuspendAllocationResponse()
                        {
                            EmpEmail = resource.EmpEmail,
                            JobCode = resource.Requisition.JobCode,
                            PipelineCode = resource.Requisition.PipelineCode
                        });
                    }
                    _allocationDbContext.UnPublishedResAllocDetails.UpdateRange(unPublishedResDetails.ToArray());
                }

                await _allocationDbContext.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        //public async Task<ResourceAllocation> UpdateRollOverAllocation(ResourceAllocation resourceAllocation)
        //{
        //    return null;
        //}

        //Not in use
        //public async Task<double> GetAllProjectAllocationHoursByPipelineCode(string pipelineCode, string jobCode)
        //{
        //    var result = await _allocationDbContext.ResourceAllocationDetails.AsNoTracking()
        //        .Where(m => m.IsActive == true
        //        && m.PipelineCode == pipelineCode
        //         && (string.IsNullOrEmpty(m.JobCode) ||
        //        (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(m.JobCode) == Convert.ToString(jobCode)))
        //        //&& m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()
        //        ).SumAsync(a => a.TotalEffort);

        //    return result;
        //}
        //Old
        //public async Task<List<ProjectAllocatedHoursRatioDto>> GetAllProjectAllocationHoursByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes)
        //{
        //    var result = _allocationDbContext.ResourceAllocationDetails.AsNoTracking().AsEnumerable()
        //        .Where(m => m.IsActive == true
        //        &&
        //        pipelineCodes.AsEnumerable().Any(p => p.Key == m.PipelineCode && p.Value == m.JobCode)
        //        ).GroupBy(l => new KeyValuePair<string, string>(l.PipelineCode, l.JobCode))
        //        .Select(pc => new ProjectAllocatedHoursRatioDto
        //        {
        //            pipelineCode = pc.FirstOrDefault().PipelineCode,
        //            jobCode = pc.FirstOrDefault().JobCode,
        //            allocatedTotalHours = pc.Sum(a => Convert.ToDouble(a.TotalEffort)),
        //        }).ToList();
        //    return result;
        //}
        public async Task<List<ProjectAllocatedHoursRatioDto>> GetAllProjectAllocationHoursByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes)
        {

            var publishedAllocations = _allocationDbContext.PublishedResAllocDetails
              .Include(m => m.Requisition)
              .AsNoTracking().AsEnumerable()
               .Where(m =>
                  m.IsActive == true
                  && pipelineCodes.AsEnumerable()
                  .Any(p =>
                      p.Key == m.Requisition.PipelineCode
                      && ((p.Value == null && m.Requisition.JobCode == null) || (p.Value == m.Requisition.JobCode))
                  ))
              .GroupBy(l => new KeyValuePair<string, string>(l.Requisition.PipelineCode, l.Requisition.JobCode))
              .Select(pc => new ProjectAllocatedHoursRatioDto
              {
                  pipelineCode = pc.FirstOrDefault().Requisition.PipelineCode,
                  jobCode = pc.FirstOrDefault().Requisition.JobCode,
                  allocatedTotalHours = pc.Sum(a => Convert.ToDouble(a.TotalEffort)),
              })
              .ToList();

            

            List<ProjectAllocatedHoursRatioDto> result = new();
            foreach (KeyValuePair<string, string> item in pipelineCodes)
            {
                //var itemFromUnPublished = unpublishedAllocations
                //    .Where(m => m.pipelineCode == item.Key && m.jobCode == item.Value)
                //    .FirstOrDefault();

                var itemFromPublished = publishedAllocations
                    .Where(m => m.pipelineCode == item.Key && m.jobCode == item.Value)
                    .FirstOrDefault();
                Double totalAllocatedHours = 0;
                //if (itemFromUnPublished != null)
                //{
                //    totalAllocatedHours += itemFromUnPublished.allocatedTotalHours;
                //}
                if (itemFromPublished != null)
                {
                    totalAllocatedHours += itemFromPublished.allocatedTotalHours;
                }
                result.Add(new()
                {
                    pipelineCode = item.Key,
                    jobCode = item.Value,
                    allocatedTotalHours = totalAllocatedHours
                });
            }
            return result;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> GetApprovedAllocationByPipeLineCode(string pipelineCode, string? jobCode)
        {
            List<ResourceAllocationDetailsResponse> rads = new();
            var publishedDetails = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .Include(m => m.Skills)
                .Where(m =>
                        m.Requisition.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                        && (
                            (m.Requisition.JobCode == null && (jobCode == null || jobCode == "undefined" || jobCode == "null"))
                            || (m.Requisition.JobCode != null && jobCode != null && m.Requisition.JobCode.ToLower().Trim() == jobCode.ToLower().Trim())
                        )
                        && m.IsActive == true
                )
                .ToListAsync();
            var transformedPublishedDetails = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedDetails);
            rads.AddRange(transformedPublishedDetails);
            return rads.OrderByDescending(d => d.ModifiedAt).ToList();
        }
        public async Task<List<ResourceAllocationDetailsResponse>> GetActiveAllocationByPipeLineCode(string pipelineCode, string? jobCode)
        {

            List<ResourceAllocationDetailsResponse> rads = new();

            var publishedDetails = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .Include(m => m.Skills)
                .Where(m =>
                        m.Requisition.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                        && (
                            (m.Requisition.JobCode == null && (jobCode == null || jobCode == "undefined" || jobCode == "null"))
                            || (m.Requisition.JobCode != null && jobCode != null && m.Requisition.JobCode.ToLower().Trim() == jobCode.ToLower().Trim())
                        )
                        && m.IsActive == true
                )
                .ToListAsync();
            var transformedPublishedDetails = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedDetails);
            rads.AddRange(transformedPublishedDetails);

            var unPublishedDetails = await _allocationDbContext.UnPublishedResAllocDetails
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .Include(m => m.Skills)
                .Where(m =>
                        m.Requisition.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                        && (
                            (m.Requisition.JobCode == null && jobCode == null)
                            || (m.Requisition.JobCode != null && jobCode != null && m.Requisition.JobCode.ToLower().Trim() == jobCode.ToLower().Trim())
                        )
                        && m.IsActive == true
                )
                .ToListAsync();
            var transformedUnPublishedDetails = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(unPublishedDetails);
            rads.AddRange(transformedUnPublishedDetails);
            return rads.OrderByDescending(d => d.ModifiedAt).ToList();
        }
        public async Task<List<ResourceAllocationDetailsResponse>> GetActivePublishedAllocationByPipeLineCode(List<KeyValuePair<string, string?>> requests)
        {

            List<ResourceAllocationDetailsResponse> rads = new();

            //var publishedDetails = await _allocationDbContext.PublishedResAllocDetails
            //    .Include(m => m.Requisition)
            //    .Include(m => m.ResourceAllocations)
            //    .Include(m => m.Skills)
            //    .Where(m =>
            //            requests.Any(r =>
            //                ((string.IsNullOrEmpty(r.Key) && string.IsNullOrEmpty(m.Requisition.PipelineCode)) ||
            //                 (!string.IsNullOrEmpty(r.Key) && r.Key.ToLower().Trim() == m.Requisition.PipelineCode.ToLower().Trim())) &&
            //                ((string.IsNullOrEmpty(r.Value) && string.IsNullOrEmpty(m.Requisition.JobCode)) ||
            //                 (!string.IsNullOrEmpty(r.Value) && r.Value.ToLower().Trim() == m.Requisition.JobCode.ToLower().Trim()))
            //            ) && m.IsActive == true
            //    )
            //    .ToListAsync();
            //var publishedDetails = await _allocationDbContext.PublishedResAllocDetails
            //    .Include(m => m.Requisition)
            //    .Include(m => m.ResourceAllocations)
            //    .Include(m => m.Skills)
            //    .Where(m => m.IsActive)
            //    .Where(m => requests.Any(r =>
            //        (EF.Functions.ILike(m.Requisition.PipelineCode, r.Key + "%") || (string.IsNullOrEmpty(r.Key) && EF.Functions.ILike(m.Requisition.PipelineCode, "")))
            //        &&
            //        (EF.Functions.ILike(m.Requisition.JobCode, r.Value + "%") || (string.IsNullOrEmpty(r.Value) && EF.Functions.ILike(m.Requisition.JobCode, "")))
            //    ))
            //    .ToListAsync();

            foreach (var kvp in requests)
            {
                var rad = await GetApprovedAllocationByPipeLineCode(kvp.Key, kvp.Value);
                rads.AddRange(rad);
            }

            //Task.WaitAll(publishedDetails);
            return rads;
        }
        public async Task ReleaseRequisitionById(Guid Id, string finalRequisitionStatus, bool deactivateRequisition)
        {
            var requisition = await _allocationDbContext.Requisition
                .Where(m => m.Id == Id)
                .Include(m => m.RequisitionType)
                .FirstOrDefaultAsync();

            if (requisition == null)
            {
                throw new Exception("Requisition does not exist");
            }

            var demand = await _allocationDbContext.RequisitionDemand
                .Where(m => m.Id == requisition.RequisitionDemand)
                .FirstOrDefaultAsync();

            requisition.RequisitionStatus = finalRequisitionStatus;

            if (demand == null)
            {
                throw new Exception("Requisition Demand does not exist");
            }

            if (requisition.RequisitionType.Type == RequisitionTypeData.CreateRequisition || requisition.RequisitionType.Type == RequisitionTypeData.BulkRequisition)
            {
                demand.PendingDemands += 1;
                requisition.RequisitionStatus = RequisitionStatuses.PENDING;
            }
            else
            {
                requisition.IsActive = deactivateRequisition;
            }
            requisition.ModifiedAt = DateTime.UtcNow;
            _allocationDbContext.Update(demand);
            _allocationDbContext.Update(requisition);
            await _allocationDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeletePublishedAllocationDaysByDetailsId(Guid publishedDetailsId)
        {
            var PublishedResourceAllocations = await _allocationDbContext.PublishedResAlloc
                .Where(m => m.PublishedResAllocDetailsId == publishedDetailsId)
                .ToListAsync();

            if (PublishedResourceAllocations != null && PublishedResourceAllocations.Count > 0)
            {
                foreach (var PublishedResourceAllocationItem in PublishedResourceAllocations)
                {
                    var PublishedResAllocDays = await _allocationDbContext.PublishedResAllocDays
                        .Where(m => m.PublishedResAllocId == PublishedResourceAllocationItem.Id)
                        .ToListAsync();

                    if (PublishedResAllocDays != null && PublishedResAllocDays.Count > 0)
                    {
                        //Remove Days
                        _allocationDbContext.PublishedResAllocDays.RemoveRange(PublishedResAllocDays);
                    }
                }
            }
            await _allocationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeletePublishedResAllocDetailsByReqId(Guid requisitionId)
        {
            var details = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Skills)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();

            if (details != null)
            {
                _allocationDbContext.RemoveRange(details);
                await _allocationDbContext.SaveChangesAsync();
            }
        }
        public async Task DeletePublishedAllocationByDetails(PublishedResAllocDetails PublishedResourceAllocationDetails)
        {
            if (PublishedResourceAllocationDetails == null)
            {
                throw new Exception("Resource Allocation Details Does not Exist");
            }

            var PublishedResourceAllocations = await _allocationDbContext.PublishedResAlloc
               .Where(m => m.PublishedResAllocDetailsId == PublishedResourceAllocationDetails.Id)
               .ToListAsync();

            var skills = await _allocationDbContext.PublishedResAllocSkillEntity
                .Where(m => m.PublishedResAllocDetailsId == PublishedResourceAllocationDetails.Id)
                .ToListAsync();
            if (skills != null && skills.Count > 0)
            {
                _allocationDbContext.PublishedResAllocSkillEntity.RemoveRange(skills);
            }

            if (PublishedResourceAllocations != null && PublishedResourceAllocations.Count > 0)
            {
                foreach (var PublishedResourceAllocationItem in PublishedResourceAllocations)
                {
                    var PublishedResAllocDays = await _allocationDbContext.PublishedResAllocDays
                        .Where(m => m.PublishedResAllocId == PublishedResourceAllocationItem.Id)
                        .ToListAsync();
                    //Remove Days
                    _allocationDbContext.PublishedResAllocDays.RemoveRange(PublishedResAllocDays);
                    //Remove Allocation item
                    _allocationDbContext.PublishedResAlloc.Remove(PublishedResourceAllocationItem);
                }
            }

            PublishedResourceAllocationDetails.IsActive = false;
            PublishedResourceAllocationDetails.ModifiedAt = DateTime.UtcNow;

            // Mark details as in-active
            _allocationDbContext.PublishedResAllocDetails.Update(PublishedResourceAllocationDetails);
            await _allocationDbContext.SaveChangesAsync();
        }
        public async Task<bool> DeleteUnPublishedResAllocDaysByDetailsId(Guid unpublishedDetailsId)
        {
            var unPublishedResourceAllocations = await _allocationDbContext.UnPublishedResAlloc
                .Where(m => m.UnPublishedResAllocDetailsId == unpublishedDetailsId)
                .ToListAsync();

            if (unPublishedResourceAllocations != null && unPublishedResourceAllocations.Count > 0)
            {
                foreach (var unPublishedResourceAllocationItem in unPublishedResourceAllocations)
                {
                    var unPublishedResAllocDays = await _allocationDbContext.UnPublishedResAllocDays
                        .Where(m => m.UnPublishedResAllocId == unPublishedResourceAllocationItem.Id)
                        .ToListAsync();
                    if (unPublishedResAllocDays != null && unPublishedResAllocDays.Count > 0)
                    {
                        //Remove Days
                        _allocationDbContext.UnPublishedResAllocDays.RemoveRange(unPublishedResAllocDays);
                    }
                }
                await _allocationDbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<UnPublishedResAllocDetails> DeleteUnPublishedAllocationsByDetails(UnPublishedResAllocDetails unPublishedResourceAllocationDetails)
        {
            if (unPublishedResourceAllocationDetails == null)
            {
                throw new Exception("Resource Allocation Details Does not Exist");
            }

            await this.CleanUnPublishedAllocationByRequisitionId(null, unPublishedResourceAllocationDetails.Id, true);

            unPublishedResourceAllocationDetails.IsActive = false;
            unPublishedResourceAllocationDetails.ModifiedAt = DateTime.UtcNow;
            _allocationDbContext.UnPublishedResAllocDetails.Update(unPublishedResourceAllocationDetails);
            await _allocationDbContext.SaveChangesAsync();
            return unPublishedResourceAllocationDetails;
        }
        public async Task<ResourceAllocationDetailsResponse> ReleaseResourceByGuid(Guid guid, string ModifiedBy)
        {
            ResourceAllocationDetailsResponse resp = new();
            var unPublishedResourceAllocationDetails = await _allocationDbContext.UnPublishedResAllocDetails
                .Include(m => m.Skills)
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.Id == guid)
                .FirstOrDefaultAsync();

            if (unPublishedResourceAllocationDetails != null)
            {
                unPublishedResourceAllocationDetails.ModifiedBy = ModifiedBy;
                await DeleteUnPublishedAllocationsByDetails(unPublishedResourceAllocationDetails);
                if (unPublishedResourceAllocationDetails.ParentPublishedResAllocDetailsId == null)
                {
                    // If there is no existing approved published state, remove requisition 
                    await ReleaseRequisitionById(unPublishedResourceAllocationDetails.RequisitionId, RequisitionStatuses.RELEASED, false);
                }
            }
            else
            {
                var PublishedResourceAllocationDetails = await _allocationDbContext.PublishedResAllocDetails
                .Where(m => m.Id == guid)
                .Include(m => m.Skills)
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .FirstOrDefaultAsync();

                if (PublishedResourceAllocationDetails != null)
                {
                    PublishedResourceAllocationDetails.ModifiedBy = ModifiedBy;
                    //Remove Allocations
                    await DeletePublishedAllocationByDetails(PublishedResourceAllocationDetails);

                    if (PublishedResourceAllocationDetails.IsUpdated)
                    {
                        var updatedRecord = await _allocationDbContext.UnPublishedResAllocDetails
                            .Where(m => m.ParentPublishedResAllocDetailsId == PublishedResourceAllocationDetails.Id && m.IsActive == true)
                            .FirstOrDefaultAsync();


                        //If allocation was updated then update the requisition 
                        var requisitionItem = await _allocationDbContext.Requisition
                             .Where(m => m.Id == PublishedResourceAllocationDetails.RequisitionId && m.IsActive == true)
                             .FirstOrDefaultAsync();
                        if (requisitionItem != null && updatedRecord != null)
                        {
                            resp.PipelineCode = requisitionItem.PipelineCode;
                            resp.JobCode = string.IsNullOrEmpty(requisitionItem.JobCode) ? null : requisitionItem.JobCode;
                            requisitionItem.RequisitionStatus = RequisitionStatuses.ALLOCATED;
                            _allocationDbContext.Update(requisitionItem);
                            await _allocationDbContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        //Release Requisitions
                        await ReleaseRequisitionById(PublishedResourceAllocationDetails.RequisitionId, RequisitionStatuses.RELEASED, false);
                    }
                }
                else
                {
                    throw new Exception("Resource Allocation not found");
                }
            }
            return resp;
        }

        public async Task<ResourceAllocationDetailsResponse> ReleaseResourceActiveAllocation(Guid guid, string ModifiedBy)
        {
            ResourceAllocationDetailsResponse resp = new();
            var unPublishedResourceAllocationDetails = await _allocationDbContext.UnPublishedResAllocDetails
                .Include(m => m.Skills)
                .Include(m => m.Requisition)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.Id == guid)
                .FirstOrDefaultAsync();

            if (unPublishedResourceAllocationDetails != null)
            {
                unPublishedResourceAllocationDetails.ModifiedBy = ModifiedBy;
                await DeleteUnPublishedAllocationsByDetails(unPublishedResourceAllocationDetails);
                if (unPublishedResourceAllocationDetails.ParentPublishedResAllocDetailsId == null)
                {
                    // If there is no existing approved published state, remove requisition 
                    await ReleaseRequisitionById(unPublishedResourceAllocationDetails.RequisitionId, RequisitionStatuses.RELEASED, false);
                }
            }
            else
            {
                var publishedResourceAllocationDetails = await _allocationDbContext.PublishedResAllocDetails
                    .Include(m => m.Requisition)
                    .Where(m => m.Id == guid && m.IsActive == true)
                    .FirstOrDefaultAsync();

                if (publishedResourceAllocationDetails != null)
                {
                    resp.PipelineCode = publishedResourceAllocationDetails?.Requisition?.PipelineCode;
                    resp.JobCode = publishedResourceAllocationDetails?.Requisition?.JobCode;

                    var publishedResAllocs = await _allocationDbContext.PublishedResAlloc
                        .Where(m => m.PublishedResAllocDetailsId == publishedResourceAllocationDetails.Id)
                        .ToListAsync();

                    DateTime dateToday = (DateOnly.FromDateTime(DateTime.UtcNow)).ToDateTime(TimeOnly.MinValue);

                    var totalFinalHoursPending = 0;

                    foreach (var item in publishedResAllocs)
                    {
                        int isAllocationStarted = DateTime.Compare(dateToday, item.StartDate.ToDateTime(TimeOnly.MinValue));
                        if (isAllocationStarted > 0 || isAllocationStarted == 0)
                        {
                            item.EndDate = DateOnly.FromDateTime(dateToday);

                            var publishedDaysToRemove = await _allocationDbContext.PublishedResAllocDays
                                .Where(m => m.PublishedResAllocId == item.Id && m.AllocationDate > DateOnly.FromDateTime(DateTime.UtcNow))
                                .ToListAsync();
                            if (publishedDaysToRemove != null && publishedDaysToRemove.Count > 0)
                            {
                                _allocationDbContext.PublishedResAllocDays.RemoveRange(publishedDaysToRemove);
                                await _allocationDbContext.SaveChangesAsync();
                            }
                            var pendingDays = await _allocationDbContext.PublishedResAllocDays
                                .Where(m => m.PublishedResAllocId == item.Id)
                                .ToListAsync();

                            var totalPendingEffort = pendingDays.Sum(m => m.Efforts);
                            if (!item.IsPerDayAllocation)
                            {
                                item.Efforts = totalPendingEffort;
                            }
                            totalFinalHoursPending = (int)(totalFinalHoursPending + totalPendingEffort);

                            _allocationDbContext.Update(item);
                        }
                        else
                        {
                            _allocationDbContext.PublishedResAlloc.Remove(item);
                        }
                    }
                    if (publishedResourceAllocationDetails.StartDate != null && publishedResourceAllocationDetails.EndDate != null)
                    {
                        int isAllocationStarted = DateTime.Compare(dateToday, publishedResourceAllocationDetails.StartDate.ToDateTime(TimeOnly.MinValue));
                        if (isAllocationStarted > 0 || isAllocationStarted == 0)
                        {
                            publishedResourceAllocationDetails.EndDate = DateOnly.FromDateTime(dateToday);
                            publishedResourceAllocationDetails.ActualEndDate = DateOnly.FromDateTime(dateToday);
                            publishedResourceAllocationDetails.IsActive = true;
                            publishedResourceAllocationDetails.TotalEffort = totalFinalHoursPending;
                            var requisitionItem = await _allocationDbContext.Requisition
                                .Where(m => m.Id == publishedResourceAllocationDetails.RequisitionId)
                                .FirstOrDefaultAsync();
                            if (requisitionItem != null)
                            {
                                requisitionItem.EndDate = DateOnly.FromDateTime(dateToday);
                            }
                        }
                        else
                        {
                            publishedResourceAllocationDetails.IsActive = false;
                            await ReleaseRequisitionById(publishedResourceAllocationDetails.RequisitionId, RequisitionStatuses.RELEASED, false);
                        }
                    }

                    publishedResourceAllocationDetails.ModifiedBy = ModifiedBy;
                    publishedResourceAllocationDetails.ModifiedAt = DateTime.UtcNow;
                    _allocationDbContext.PublishedResAllocDetails.Update(publishedResourceAllocationDetails);
                    await _allocationDbContext.SaveChangesAsync();
                }
            }
            return resp;
        }


        //Old
        //public async Task<UnPublishedResAllocDetails> ReleaseResourceByGuid(Guid guid)
        //{
        //    try
        //    {

        //        var resourceAllocationDetail = await _allocationDbContext.UnPublishedResAllocDetails
        //                .Where(d => d.Id == guid && d.IsActive == true)
        //                .FirstOrDefaultAsync();

        //        if (resourceAllocationDetail == null)
        //        {
        //            throw new Exception("Resource Allocation Details Does not Exist");
        //        }
        //        var resourceAllocations = await _allocationDbContext.ResourceAllocation.Where(e => e.ResAllocDetailsId == resourceAllocationDetail.Id && e.IsActive == true).ToListAsync();
        //        foreach (var resourceAllocation in resourceAllocations)
        //        {
        //            int isResourceAllocation = DateTime.Compare(DateTime.Now, ((DateTime)resourceAllocation.ConfirmedAllocationStartDate).Date);
        //            if (isResourceAllocation > 0)
        //            {
        //                resourceAllocation.ConfirmedAllocationEndDate = DateTime.UtcNow;
        //            }
        //            else
        //            {
        //                resourceAllocation.IsActive = false;
        //            }
        //            _allocationDbContext.ResourceAllocation.Update(resourceAllocation);
        //        }
        //        resourceAllocationDetail.IsActive = false;
        //        var res = _allocationDbContext.ResourceAllocationDetails.Update(resourceAllocationDetail);
        //        var req = await _allocationDbContext.Requisition
        //             .Include(m => m.RequisitionTypes)
        //             .Where(i => i.RequisionId == resourceAllocationDetail.RequisitionId && i.IsActive == true)
        //             .ToListAsync();
        //        foreach (var requisition in req)
        //        {
        //            if (requisition.RequisitionTypes != null
        //                    && requisition.RequisitionTypes.Type != null
        //                    && (
        //                    requisition.RequisitionTypes.Type.Equals(RequisitionTypeData.CreateRequisition)
        //                    || requisition.RequisitionTypes.Type.Equals(RequisitionTypeData.BulkRequisition)
        //                    )
        //                )
        //            {
        //                requisition.RequisitionStatus = "Pending";
        //                _allocationDbContext.Requisition.Update(requisition);

        //                var requisitionDemand = _allocationDbContext.RequisitionDemand.Where(m => m.Id.Equals(requisition.RequisitionDemand)).FirstOrDefault();
        //                if (requisitionDemand != null)
        //                {
        //                    requisitionDemand.PendingDemands = requisitionDemand.PendingDemands + 1;
        //                    _allocationDbContext.RequisitionDemand.Update(requisitionDemand);
        //                }
        //            }
        //            else
        //            {
        //                requisition.RequisitionStatus = "Released";
        //                requisition.IsActive = false;
        //                _allocationDbContext.Requisition.Update(requisition);
        //            }
        //        }
        //        var resAllcoDays = await _allocationDbContext.ResourceAllocationDays.Where(i => i.ResAllocDetailsId == resourceAllocationDetail.Id && i.IsActive == true).ToListAsync();
        //        foreach (var resallcoDay in resAllcoDays)
        //        {
        //            resallcoDay.IsActive = false;
        //            _allocationDbContext.ResourceAllocationDays.Update(resallcoDay);
        //        }
        //        await _allocationDbContext.SaveChangesAsync();

        //        return res.Entity;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Something went wrong!!", ex);
        //    }
        //}
        public async Task<List<BudgetOverviewDto>> GetBudgetOverview(BudgetOverviewRequest request)
        {
            List<BudgetOverviewDto> budgetOverviewResult = new();
            foreach (var jobCode in request.JobCodes)
            {
                BudgetOverviewDto budgetOverview = new();
                var allocations = await _allocationDbContext.PublishedResAlloc.
                    Join(_allocationDbContext.PublishedResAllocDetails, a => a.PublishedResAllocDetailsId, d => d.Id, (a, d) => new { Allocation = a, Details = d })
                    .Join(_allocationDbContext.PublishedResAllocDetails, ad => ad.Details.RequisitionId, r => r.RequisitionId, (ad, r) => new { Details = ad, Requisition = r })
                    .Where((a => a.Details.Allocation.StartDate >= DateOnly.FromDateTime(request.StartDate) &&
                            a.Details.Allocation.EndDate <= DateOnly.FromDateTime(request.EndDate)
                            && (string.IsNullOrEmpty(a.Requisition.Requisition.JobCode) ||
                            (!string.IsNullOrEmpty(jobCode.Value) == true && Convert.ToString(a.Requisition.Requisition.JobCode).Trim().ToLower() == Convert.ToString(jobCode.Value).Trim().ToLower()))
                            //&& a.JobCode == jobCode.Value
                            && a.Requisition.Requisition.PipelineCode == jobCode.Key
                            && a.Requisition.Requisition.IsActive == true && Constants.ALLOCATION_ACCEPT_STATUS.Contains(a.Details.Details.AllocationStatus))).ToListAsync();
                var overAllProjectAllocatedhour = await GetOverAllProjectAllocatedHours(jobCode.Value, jobCode.Key);
                int noOfResource = allocations.DistinctBy(a => a.Details.Details.EmpEmail).Count();
                budgetOverview.StartDate = request.StartDate;
                budgetOverview.EndDate = request.EndDate;
                budgetOverview.JobCode = jobCode.Value;
                budgetOverview.PipelineCode = jobCode.Key;
                budgetOverview.TotalResourcesCount = noOfResource;
                budgetOverview.TotalAllocatedHours = overAllProjectAllocatedhour.TotalAllocatedHours;
                budgetOverviewResult.Add(budgetOverview);
            }

            return budgetOverviewResult;
        }
        public Task<List<ResourceAllocationDesignation>> GetDesignationBudget(string pipelineCode, string jobCode)
        {
            NpgsqlConnection conn = null;
            List<ResourceAllocationDesignation> result = new List<ResourceAllocationDesignation>();
            try
            {

                //string query = @" CALL  " + Constants.AllocationDesignationSP + " ('" + jobCode + "','" + pipelineCode + "'," + Constants.WorkingHourPerDay + ", '{}');";
                string query = Constants.AllocationDesignationSP;

                DataTable dt = new DataTable();
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();

                /*
                NpgsqlConnection conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new(query, conn);
                NpgsqlDataAdapter myAdapter = new(cmd);
                myAdapter.Fill(dt);
                myAdapter.Dispose();
                var data = dt.AsEnumerable().Select(e => e).ToList();
                var rowData = data[0].ItemArray[0];
                var result = JsonConvert.DeserializeObject<List<ResourceAllocationDesignation>>(rowData.ToString());
                return result;
                */

                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, jobCode);
                cmd.Parameters.AddWithValue("inpipelinecode", NpgsqlDbType.Text, pipelineCode);
                cmd.Parameters.AddWithValue("rateperhour", NpgsqlDbType.Double, Convert.ToDouble(Constants.WorkingHourPerDay));

                var outputResult = new NpgsqlParameter("response", NpgsqlDbType.Jsonb);
                outputResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputResult);

                conn.Open();
                cmd.ExecuteNonQuery();
                var jsonResult = cmd.Parameters["response"].Value.ToString();
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    result = JsonConvert.DeserializeObject<List<ResourceAllocationDesignation>>(jsonResult);
                }
                conn.Close();

                //Dictionary<string, int> allocationDictornay = new Dictionary<string, int>();

                //var designationGroupData = await _allocationDbContext.resourceallocationrequistionview.Where(m =>
                // m.JobCode == jobCode
                // && m.IsActive == true
                //     ).GroupBy(m => m.Designation)
                // .ToListAsync();

                //foreach (var group in designationGroupData)
                //{
                //    allocationDictornay.Add(group.Key, group.Sum(a => a.TotalEffort));
                //}
                //return allocationDictornay;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Task.FromResult<List<ResourceAllocationDesignation>>(result);
            //   List<DesignationBudget> budgetDesignationResult = new List<DesignationBudget>();

        }
        public Task<List<UpdateDesignationCost>> UpdateDesignationCost(List<UpdateDesignationCostDTO> request)
        {
            NpgsqlConnection conn = null;
            List<UpdateDesignationCost> result = new List<UpdateDesignationCost>();
            try
            {
                var serializeRequest = JsonConvert.SerializeObject(request);
                //string query = @" CALL  " + Constants.UpdateDesingationSP + " ('" + serializeRequest + "', '{}');";
                string query = Constants.UpdateDesingationSP;

                DataTable dt = new DataTable();
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);
                /*
                NpgsqlConnection conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(dt);
                myAdapter.Dispose();
                var data = dt.AsEnumerable().Select(e => e).ToList();
                var rowData = data[0].ItemArray[0];
                var result = JsonConvert.DeserializeObject<UpdateDesignationCost>(rowData.ToString());
                var listResult = new List<UpdateDesignationCost>();
                listResult.Add(result);
                return listResult;
                */

                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("emp_details", NpgsqlDbType.Jsonb, serializeRequest);

                var outputResult = new NpgsqlParameter("response", NpgsqlDbType.Jsonb);
                outputResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputResult);

                conn.Open();
                cmd.ExecuteNonQuery();
                var jsonResult = cmd.Parameters["response"].Value.ToString();
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    var temp = JsonConvert.DeserializeObject<UpdateDesignationCost>(jsonResult);
                    result.Add(temp);
                }
                conn.Close();


                //Dictionary<string, int> allocationDictornay = new Dictionary<string, int>();

                //var designationGroupData = await _allocationDbContext.resourceallocationrequistionview.Where(m =>
                // m.JobCode == jobCode
                // && m.IsActive == true
                //     ).GroupBy(m => m.Designation)
                // .ToListAsync();

                //foreach (var group in designationGroupData)
                //{
                //    allocationDictornay.Add(group.Key, group.Sum(a => a.TotalEffort));
                //}
                //return allocationDictornay;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Task.FromResult<List<UpdateDesignationCost>>(result);
            //   List<DesignationBudget> budgetDesignationResult = new List<DesignationBudget>();

        }
        //public async Task<List<BudgetOverviewDto>> GetAllocatedHoursByProject(BudgetOverviewRequest request)
        //{
        //    foreach (var jobCode in request.JobCodes)
        //    {
        //        var allocations = await _allocationDbContext.ResourceAllocation.Where((a => a.ConfirmedAllocationStartDate >= request.StartDate &&
        //          a.ConfirmedAllocationEndDate <= request.EndDate
        //          && a.JobCode == jobCode)).ToListAsync();
        //        foreach (var item in allocations)
        //        {

        //        }

        //    }

        //    return new List<BudgetOverviewDto>();
        //}

        //Not in use
        //public async Task<List<ResourceAllocatedHour>> GetResourceAlloctedHours(ResourceAllocatedHourRequest request)
        //{
        //    //Leave Details check 

        //    List<string> emailsList = request.EmpEmail.Select(s => s.ToLower().Trim()).ToList();
        //    var response = await _allocationDbContext.ResourceAllocationDetails
        //        .Where(m =>
        //                emailsList.ToArray().Any(p => m.EmpEmail.ToLower().Equals(p))
        //                && m.AllocationStartDate <= request.EndDate
        //                && m.AllocationEndDate >= request.StartDate
        //                && m.IsActive == true
        //            ).GroupBy(a => a.EmpEmail)
        //        .ToListAsync();
        //    List<ResourceAllocatedHour> resourceALlocationList = new List<ResourceAllocatedHour>();
        //    foreach (var resource in response)
        //    {
        //        ResourceAllocatedHour resourceALlocation = new ResourceAllocatedHour();
        //        int totalHours = 0;
        //        foreach (var item in resource)
        //        {
        //            totalHours += item.TotalEffort;
        //        }
        //        resourceALlocation.AllocatedHours = totalHours;
        //        resourceALlocationList.Add(resourceALlocation);
        //    }
        //    return resourceALlocationList;
        //}
        public async Task<OverAllProjectAllocatedHours> GetOverAllProjectAllocatedHours(string pipelineCode, string jobCode)
        {
            //Leave Details check 
            var response = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Requisition)
                .Where(m =>
                        m.Requisition.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                        && (String.IsNullOrEmpty(m.Requisition.JobCode) ||
                        (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(m.Requisition.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower()))
                        && m.IsActive == true
                 )
                .ToListAsync();

            //var response = await _allocationDbContext.ResourceAllocationDetails
            //    .Where(m =>
            //            m.PipelineCode == pipelineCode
            //            && (string.IsNullOrEmpty(m.JobCode) ||
            //            (!string.IsNullOrEmpty(jobCode) == true && Convert.ToString(m.JobCode).Trim().ToLower() == Convert.ToString(jobCode).Trim().ToLower()))
            //            //&& m.JobCode == jobCode
            //            && m.IsActive == true
            //        )
            //    .ToListAsync();

            Int64 totalHours = 0;
            OverAllProjectAllocatedHours projectAllocation = new();
            foreach (var resource in response)
            {
                totalHours += resource.TotalEffort;
            }
            projectAllocation.PipelineCode = pipelineCode;
            projectAllocation.JobCode = jobCode;
            projectAllocation.TotalAllocatedHours = (int)totalHours;
            return projectAllocation;
        }
        public Task<List<AllocationDayGroup>> ResourceAllocationDayGroup(string timeOption, string pipelineCode, string jobCode, DateTime startDate, DateTime endDate)
        {
            NpgsqlConnection conn = null;
            List<AllocationDayGroup> result = new List<AllocationDayGroup>();
            try
            {
                //string query = @"CALL public.sp_allocationday_view('daily', 'JC102', '01/10/2023', '17/01/2024',)";

                //string query = @" CALL  " + Constants.AllocationDayFunction + " ('" + timeOption + "','" + pipelineCode + "','" + jobCode + "','" + startDate + "','" + endDate + "', '{}');";
                string query = Constants.AllocationDayFunction;

                DataTable dt = new DataTable();
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);

                /*
                NpgsqlConnection conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(dt);
                myAdapter.Dispose();
                var data = dt.AsEnumerable().Select(e => e).ToList();
                var rowData = data[0].ItemArray[0];
                var result = JsonConvert.DeserializeObject<List<AllocationDayGroup>>(rowData.ToString());
                return result;
                */

                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("timeoptionname", NpgsqlDbType.Text, timeOption);
                cmd.Parameters.AddWithValue("inpipelinecode", NpgsqlDbType.Text, pipelineCode);
                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, jobCode);
                cmd.Parameters.AddWithValue("startdate", NpgsqlDbType.Date, startDate);
                cmd.Parameters.AddWithValue("enddate", NpgsqlDbType.Date, endDate);

                var outputResult = new NpgsqlParameter("response", NpgsqlDbType.Jsonb);
                outputResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputResult);

                conn.Open();
                cmd.ExecuteNonQuery();
                var jsonResult = cmd.Parameters["response"].Value.ToString();
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    result = JsonConvert.DeserializeObject<List<AllocationDayGroup>>(jsonResult);

                }
                conn.Close();

                //  var response = await _allocationDbContext.sp_allocationday_view.FromSqlRaw(query).ToListAsync();
                // return response;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Task.FromResult<List<AllocationDayGroup>>(result);
            // string query = @"select * from " + Constants.AllocationDayFunction + " ('" + timeOption + "','" + jobCode + "','" + startDate +"','" + endDate +"')";

        }

        public async Task<List<AllocationDayResourceGroup>> PublishedResouceAllocationDaysGroup()
        {
            var groupedData = await _allocationDbContext.PublishedResAllocDays.GroupBy(e => new { e.PipelineCode, e.JobCode }).Select(e => new
            {
                PipelineCode = e.Key.PipelineCode,
                JobCode = e.Key.JobCode,
                TotalAcualEfforts = e.Sum(i => i.ActualEffort)
            }).ToListAsync();
            List<AllocationDayResourceGroup> result = new();
            foreach (var group in groupedData)
            {
                result.Add(new()
                {
                    PipelineCode = group.PipelineCode,
                    JobCode = group.JobCode,
                    TotalActualEfforts = (double?)group.TotalAcualEfforts
                });
            }
            return result;
        }

        public Task<List<AllocationDayResourceGroup>> ResourceAllocationDayResourceGroup(DateTime startDate, DateTime endDate, string pipelineCode, string jobCode)
        {
            //app scan fix
            NpgsqlConnection conn = null;
            List<AllocationDayResourceGroup> result = new List<AllocationDayResourceGroup>();
            try
            {
                string query = Constants.AllocationDayResourceFunction;

                DataTable dt = new DataTable();
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);
                /*
                NpgsqlConnection conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(dt);
                myAdapter.Dispose();
                var data = dt.AsEnumerable().Select(e => e).ToList();
                var rowData = data[0].ItemArray[0];
                var result = JsonConvert.DeserializeObject<List<AllocationDayResourceGroup>>(rowData.ToString());
                return result;
                */

                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("inpipelinecode", NpgsqlDbType.Text, pipelineCode);
                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, jobCode);
                cmd.Parameters.AddWithValue("startdate", NpgsqlDbType.Date, startDate);
                cmd.Parameters.AddWithValue("enddate", NpgsqlDbType.Date, endDate);

                var outputResult = new NpgsqlParameter("response", NpgsqlDbType.Jsonb);
                outputResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputResult);

                conn.Open();
                cmd.ExecuteNonQuery();
                var jsonResult = cmd.Parameters["response"].Value.ToString();
                if (!string.IsNullOrEmpty(jsonResult))
                {
                    result = JsonConvert.DeserializeObject<List<AllocationDayResourceGroup>>(jsonResult);

                }
                conn.Close();

                //string query = @"select * from " + Constants.AllocationDayResourceFunction + " ('" + startDate + "','" + endDate + "','" + jobCode + "' )";
                //var response = await _allocationDbContext.sp_allocationday_resource_view.FromSqlRaw(query).ToListAsync();
                //return response;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return Task.FromResult<List<AllocationDayResourceGroup>>(result);
        }
        public async Task<List<JobAllocationMappingDTO>> GetJobAllocationMapping(List<DateTime> confirmedDate)
        {
            confirmedDate = confirmedDate.Where(a => a != null).Select(x => x.Date).ToList();
            //change
            List<string> allocationConfirmedStatuses = new()
            {
                WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE,
                WorkflowStatus.EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE,
                WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION,
                WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER,
                WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH
            };

             allocationConfirmedStatuses = allocationConfirmedStatuses.Select(a => a.ToLower().Trim()).ToList();

             var confirmedDateOnly = confirmedDate
            .Select(cd => DateOnly.FromDateTime(cd))
            .ToList();

            var responseIds = _allocationDbContext.PublishedResAllocDetails
                .Join(_allocationDbContext.PublishedResAllocDays,
                    pr => pr.RequisitionId,
                    prd => prd.RequisitionId,
                    (pr, prd) => new { pr, prd })
                .Where(x =>
                    x.pr.IsActive
                    && !string.IsNullOrEmpty(x.pr.AllocationStatus)
                    && allocationConfirmedStatuses.Any(ac => ac == x.pr.AllocationStatus.ToLower().Trim())
                    && confirmedDateOnly.Contains(x.prd.AllocationDate)   // ✅ now comparing DateOnly with DateOnly
                )
                .Select(x => x.pr.RequisitionId)
                .ToList();
            //Get all allocations which are confirmed on specified date
            //var response = await _allocationDbContext.PublishedResAllocDetails
            //    .Where(m =>
            //                m.IsActive == true
            //                && !string.IsNullOrEmpty(m.AllocationStatus) && allocationConfirmedStatuses.AsEnumerable().Any(ac => ac == m.AllocationStatus.ToLower().Trim())
            //                && m.ConfirmedAllocationDate != null && confirmedDate.AsEnumerable().Any(s => s.Date == m.ConfirmedAllocationDate.Date)
            //     )
            var response = await _allocationDbContext.PublishedResAllocDays
               .Where(m => responseIds != null && responseIds.Any(r => r == m.RequisitionId) 
                && confirmedDateOnly.Contains(m.AllocationDate)
                )
               .Include(m => m.Requisition)
               .Select(m => new JobAllocationMappingDTO
                {
                    PipelineCode = m.Requisition.PipelineCode,
                    PipelineName = m.Requisition.PipelineName,
                    JobCode = m.Requisition.JobCode,
                    JobName = m.Requisition.JobName,
                    EmpMID = m.EmailId.Contains(UserMidSplitter) ? m.EmailId.Split(UserMidSplitter, StringSplitOptions.None)[0].Trim() : m.EmailId,
                    EmpEmail = m.EmailId.Contains(UserMidSplitter) ? m.EmailId.Split(UserMidSplitter, StringSplitOptions.None)[1].Trim() : string.Empty,
                    AllocationConfirmationDate = m.AllocationDate != null ? m.AllocationDate.ToDateTime(TimeOnly.MinValue).Date : null
                })
               .ToListAsync();

            //Get all allocations which are confirmed on specified date
            //var response = _allocationDbContext.ResourceAllocationDetails.AsEnumerable().Where(x =>
            //x.IsActive == true
            //&& allocationConfirmedStatuses.Any(ac => string.Compare(ac, x.AllocationStatus, true) == 0)
            //&& confirmedDate.Any(s => x.ConfirmedAllocationDate.HasValue && s.Date == x.ConfirmedAllocationDate.Value.Date)
            //).Select(a => new JobAllocationMappingDTO
            //{
            //    PipelineCode = a.PipelineCode,
            //    PipelineName = a.PipelineName,
            //    JobCode = a.JobCode,
            //    JobName = a.JobName,
            //    EmpEmail = a.EmpEmail,//add email mid also to response object 
            //    AllocationConfirmationDate = a.ConfirmedAllocationDate.HasValue ? a.ConfirmedAllocationDate.Value.Date : null
            //}).ToList();

            return response;
        }
        public async Task<Boolean> UpdateAllocationWithNewJobCode(string pipelineCode, string jobCode, string new_pipelineCode, string new_JobCode, string new_JobName, string updatedBy)
        {
            NpgsqlConnection npgsqlConnection = null;
            try
            {
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();
                npgsqlConnection = new NpgsqlConnection(pgsqlConnection);
                using (NpgsqlCommand command = new NpgsqlCommand(Constants.sp_update_jobcode, npgsqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.pipeline_code_column_name, pipelineCode);
                    command.Parameters.AddWithValue(Constants.job_code_column_name, jobCode);
                    command.Parameters.AddWithValue(Constants.new_job_code_column_name, new_JobCode);
                    command.Parameters.AddWithValue(Constants.new_pipeline_code_column_name, new_pipelineCode);
                    command.Parameters.AddWithValue(Constants.new_job_name_column_name, new_JobName);
                    command.Parameters.AddWithValue(Constants.modified_by_column_name, updatedBy);
                    npgsqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                throw ;
            }
            finally
            {
                if (npgsqlConnection != null)
                {
                    npgsqlConnection.Close();
                }
            }
        }
        public async Task<List<UnPublishedResAllocSkillEntity>> UpdateUnPublishedResourceAllocationSkillEntity(List<SkillsEntities> finalSkills, Guid requisitionId, string userEmail, Guid unPublishedResAllocDetailsId)
        {
            var previousSkills = await _allocationDbContext.UnPublishedResAllocSkillEntity.Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();

            var prevNonMatchingSkills = previousSkills
                .Where(m =>
                    !finalSkills.Any(p =>
                        m.SkillName.Trim().ToLower() == p.SkillName.Trim().ToLower()
                        && m.SkillCode.ToLower().Trim() == p.SkillCode.Trim().ToLower())
                    )
                .ToList();
            _allocationDbContext.UnPublishedResAllocSkillEntity.RemoveRange(prevNonMatchingSkills);

            var newlyAddedSkills = finalSkills
                .Where(m =>
                    !previousSkills.Any(p =>
                        p.SkillName.Trim().ToLower() == m.SkillName.Trim().ToLower()
                        && p.SkillCode.ToLower().Trim() == m.SkillCode.Trim().ToLower())
                    )
                .ToList();
            foreach (var item in newlyAddedSkills)
            {
                var newSkills = new UnPublishedResAllocSkillEntity
                {
                    RequisitionId = requisitionId,
                    SkillName = item.SkillName,
                    SkillCode = item.SkillCode,
                    UnPublishedResAllocDetailsId = unPublishedResAllocDetailsId
                };
                _allocationDbContext.Set<UnPublishedResAllocSkillEntity>().Add(newSkills);
            }
            await _allocationDbContext.SaveChangesAsync();

            return await _allocationDbContext.UnPublishedResAllocSkillEntity
                .Where(m => m.RequisitionId == requisitionId)
                .ToListAsync();

        }
        //public async Task<ResourceAllocation> UpdateRollOverAllocation(ResourceAllocation resourceAllocation)
        //{
        //    return null;
        //}

        //public async Task<ResourceAllocation> UpdateRollOverAllocation1(List<ResourceAllocationDetails> resourceAllocationDetails)
        ////public async Task<ResourceAllocation> UpdateRollOverAllocation(ResourceAllocation resourceAllocation)
        //{
        //    return null;

        //    ////_allocationDbContext.Entry(resourceAllocation).Property(x => x.ConfirmedAllocationStartDate).IsModified = true;
        //    ////_allocationDbContext.Entry(resourceAllocation).Property(x => x.ConfirmedAllocationEndDate).IsModified = true;
        //    ////var entityToUpdate = _allocationDbContext.ResourceAllocation.Where(x => x.Id == resourceAllocation.Id).FirstOrDefault();

        //}
        public async Task<List<string>> DoesUserHaveAnyFutureOrOngoingAllocations(List<string> emails)
        {
            var allEmails = emails.Select(m => m.ToLower().Trim()).ToList();
            var statusesNotToValidate = new List<string> { AllocationStatuses.REJECTED.ToLower(), AllocationStatuses.DRAFT.ToLower() };

            var allocations = await _allocationDbContext.UnPublishedResAllocDetails
                .Where(m =>
                    m.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow)
                    && m.IsActive == true
                    && allEmails.Any(p => p == m.EmpEmail.ToLower().Trim())
                    && !statusesNotToValidate.Any(p => p == m.AllocationStatus.ToLower())
                    )
                .ToListAsync();
            return allocations
                .Distinct()
                .Select(m => m.EmpEmail).ToList();

        }
        public async Task<List<Guid>> GetAllDraftAllocationForEmployeeForRemoval(List<string> emails)
        {
            List<string> allEmails = emails.Select(m => m.ToLower().Trim()).ToList();
            var allocationsToRemove = await _allocationDbContext.UnPublishedResAllocDetails
                .Where(m =>
                        allEmails.Contains(m.EmpEmail.ToLower())
                        && m.AllocationStatus.ToLower().Trim() == AllocationStatuses.DRAFT
                        && m.IsActive == true
                      )
                .ToListAsync();
            return allocationsToRemove.Select(m => m.Id).ToList();
        }
        public async Task<UnPublishedResAllocDetails?> GetUnPublishedResAllocDetailsById(Guid id)
        {
            return await _allocationDbContext.UnPublishedResAllocDetails
                .Where(m =>
                        m.Id == id
                        && m.IsActive == true
                    )
                .FirstOrDefaultAsync();
        }

        public async Task<PublishedResAllocDetails> GetPublishedResAllocDetailsById(Guid id)
        {
            return await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Skills)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.IsActive == true && m.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<PublishedResAllocDetails> UpdatePublishedResAllocDetailsByEntity(PublishedResAllocDetails publishedResAllocDetails)
        {
            _allocationDbContext.Update(publishedResAllocDetails);
            await _allocationDbContext.SaveChangesAsync();
            return publishedResAllocDetails;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> GetAllAllocationByPipelineOrJobCode(string pipelineCode, string? jobCode)
        {
            var startOfDay = DateOnly.FromDateTime(DateTime.Today);
            var publishedResAllocDetails = await _allocationDbContext.PublishedResAllocDetails
                .Include(m => m.Requisition)
                .Include(m => m.Skills)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.EndDate >= startOfDay && m.Requisition.PipelineCode == pipelineCode
                            && m.IsActive == true
                            && ((jobCode == null && m.Requisition.JobCode == null)
                                || m.Requisition.JobCode == jobCode
                 ))
                .ToListAsync();

            var unPublishedResAllocDetails = await _allocationDbContext.UnPublishedResAllocDetails
                .Include(m => m.Requisition)
                .Include(m => m.Skills)
                .Include(m => m.ResourceAllocations)
                .Where(m => m.EndDate >= startOfDay && m.Requisition.PipelineCode == pipelineCode
                            && m.IsActive == true
                            && ((jobCode == null && m.Requisition.JobCode == null)
                                || m.Requisition.JobCode == jobCode
                 ))
                .ToListAsync();

            Task<List<ResourceAllocationDetailsResponse>> transformedPublishedResAllocDetails = TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(publishedResAllocDetails);
            Task<List<ResourceAllocationDetailsResponse>> transformedUnPublishedResAllocDetails = TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(unPublishedResAllocDetails);

            await Task.WhenAll(transformedPublishedResAllocDetails, transformedUnPublishedResAllocDetails);
            List<ResourceAllocationDetailsResponse> response = new();
            response.AddRange(transformedPublishedResAllocDetails.Result);
            response.AddRange(transformedUnPublishedResAllocDetails.Result);
            return response;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> UpdateResourcesAllocations(ResourceAllocationDetailsResponse[] allResourceAllocationDTO)
        {
            try
            {
                var startOfDay = DateOnly.FromDateTime(DateTime.Today);
                var previousDay = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

                foreach (var resourceAllocationDetail in allResourceAllocationDTO)
                {
                    var pubResAllocDetails = await _allocationDbContext.PublishedResAllocDetails.Where(m => resourceAllocationDetail.RequisitionId == m.RequisitionId && m.IsActive == true).FirstOrDefaultAsync();
                    if (pubResAllocDetails != null)
                    {
                        if (pubResAllocDetails.StartDate < startOfDay)
                        {
                            pubResAllocDetails.EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
                            pubResAllocDetails.TotalEffort = await CommonAllocationHelper.GetTotalAllocationByRequisition(_allocationDbContext, pubResAllocDetails.RequisitionId);
                        }

                        else
                        {
                            pubResAllocDetails.IsActive = false;

                        }
                        pubResAllocDetails.ActualStartDate = pubResAllocDetails.StartDate;
                        pubResAllocDetails.ActualEndDate = pubResAllocDetails.EndDate;
                        _allocationDbContext.Update(pubResAllocDetails);
                    }
                    var pubResRequisition = await _allocationDbContext.Requisition.Where(m => m.Id == resourceAllocationDetail.RequisitionId && m.IsActive == true).FirstOrDefaultAsync();
                    if (pubResRequisition != null)
                    {
                        if (pubResRequisition.StartDate < startOfDay)
                        {
                            pubResRequisition.EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
                            pubResRequisition.TotalHours = await CommonAllocationHelper.GetTotalAllocationByRequisition(_allocationDbContext, pubResRequisition.Id);

                        }
                        else
                            pubResRequisition.IsActive = false;
                        _allocationDbContext.Update(pubResRequisition);
                    }

                    if (resourceAllocationDetail.ResourceAllocations != null)
                    {
                        foreach (var resAllocation in resourceAllocationDetail.ResourceAllocations)
                        {
                            var lsPubResAlloc = await _allocationDbContext.PublishedResAlloc.Where(m => m.Id == resAllocation.Id).ToListAsync();
                            foreach (var pubResAlloc in lsPubResAlloc)
                            {
                                if (pubResAlloc.StartDate < startOfDay)
                                {
                                    pubResAlloc.EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
                                }
                                else
                                {
                                    _allocationDbContext.Remove(pubResAlloc);
                                }
                            }
                        }
                    }
                    /**************************** ResourceAllocationDaysTable items *********************/
                    var lstPubResAllocationDays = await _allocationDbContext.PublishedResAllocDays.Where(m => m.RequisitionId
                    == resourceAllocationDetail.RequisitionId && m.AllocationDate >= startOfDay).ToListAsync();
                    if (lstPubResAllocationDays != null && lstPubResAllocationDays.Count > 0)
                    {
                        _allocationDbContext.PublishedResAllocDays.RemoveRange(lstPubResAllocationDays);
                    }

                    /************************ Remove allUnblishedRecords ******************************/

                    var lstUnPubResAllocationDays = await _allocationDbContext.UnPublishedResAllocDays.Where(m => m.RequisitionId
                    == resourceAllocationDetail.RequisitionId && m.AllocationDate >= startOfDay).ToListAsync();
                    if (lstUnPubResAllocationDays != null && lstUnPubResAllocationDays.Count > 0)
                    {
                        _allocationDbContext.UnPublishedResAllocDays.RemoveRange(lstUnPubResAllocationDays);
                    }
                    var lstUnPubResAlloc = await _allocationDbContext.UnPublishedResAlloc.Where(m => m.RequisitionId == resourceAllocationDetail.RequisitionId).ToListAsync();
                    if (lstUnPubResAlloc != null && lstUnPubResAlloc.Count > 0)
                    {
                        _allocationDbContext.UnPublishedResAlloc.RemoveRange(lstUnPubResAlloc);
                    }
                    var lstUnPubResAllocDetails = await _allocationDbContext.UnPublishedResAllocDetails.Where(m => m.RequisitionId == resourceAllocationDetail.RequisitionId).ToListAsync();
                    if (lstUnPubResAllocDetails != null && lstUnPubResAllocDetails.Count > 0)
                    {
                        _allocationDbContext.UnPublishedResAllocDetails.RemoveRange(lstUnPubResAllocDetails);
                    }
                    var lstRequisitionPending = await _allocationDbContext.Requisition.Where(m => m.Id == resourceAllocationDetail.RequisitionId && m.RequisitionStatus.ToLower().Contains("pending")).ToListAsync();
                    if (lstUnPubResAllocDetails != null && lstUnPubResAllocDetails.Count > 0)
                    {
                        _allocationDbContext.UnPublishedResAllocDetails.RemoveRange(lstUnPubResAllocDetails);
                    }
                }
                await _allocationDbContext.SaveChangesAsync();
                return allResourceAllocationDTO?.ToList() ?? new List<ResourceAllocationDetailsResponse>();
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public async Task<List<ResourceAllocationDaysResponse>> GetResAllocDaysRespFromPubResAllocId(Guid resAllocId)
        {
            var publishedResAllocDays = await _allocationDbContext.PublishedResAllocDays
                .Where(m => m.PublishedResAllocId == resAllocId)
                .ToListAsync();
            if (publishedResAllocDays != null)
            {
                var response = await TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(publishedResAllocDays);
                return response;
            }

            var unPublishedResAllocDays = await _allocationDbContext.UnPublishedResAllocDays
                    .Where(m => m.UnPublishedResAllocId == resAllocId)
                    .ToListAsync();
            if (unPublishedResAllocDays != null)
            {
                var response = await TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(unPublishedResAllocDays);
                return response;
            }
            return null;
        }

        public async Task<List<GetUserAvailabilitiesForSystemSuggestionResponse>> GetAvailabilityForSystemSuggestion(
            List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion> users
            , Int64 required_hours
            , DateTime start_date
            , DateTime end_date
            , string job_code
            , string? pipeline_code
        )
        {
            List<GetUserAvailabilitiesForSystemSuggestionResponse> response = new() { };

            NpgsqlConnection npgsqlConnection = null;
            try
            {
                //create a constant file to store all static data like sp name with their params as well
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();

                npgsqlConnection = new NpgsqlConnection(pgsqlConnection);
                using (NpgsqlCommand command = new NpgsqlCommand("sp_availability_system_suggestion", npgsqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var userEmails = new NpgsqlParameter("users", NpgsqlDbType.Jsonb)
                    {
                        Value = users
                    };
                    command.Parameters.Add(userEmails);

                    command.Parameters.AddWithValue("required_hours", Convert.ToInt16(required_hours));
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true
                    };

                    var start_date_constraintParam = new NpgsqlParameter("start_date", NpgsqlDbType.Date)
                    {
                        Value = DateOnly.FromDateTime(start_date),
                    };
                    command.Parameters.Add(start_date_constraintParam);

                    var end_date_constraintParam1 = new NpgsqlParameter("end_date", NpgsqlDbType.Date)
                    {
                        Value = DateOnly.FromDateTime(end_date),
                    };
                    command.Parameters.Add(end_date_constraintParam1);

                    command.Parameters.AddWithValue("job_code", Convert.ToString(String.IsNullOrEmpty(job_code) ? "" : job_code));
                    command.Parameters.AddWithValue("pipeline_code", Convert.ToString(pipeline_code));

                    var outputResult = new NpgsqlParameter("var_resp", NpgsqlDbType.Json);
                    outputResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputResult);

                    npgsqlConnection.Open();
                    command.ExecuteNonQuery();
                    var jsonResult = command.Parameters["var_resp"].Value.ToString();

                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        JArray jsonArray = JArray.Parse(jsonResult);

                        if (jsonArray != null)
                        {
                            foreach (var json in jsonArray)
                            {
                                response.Add(JsonConvert.DeserializeObject<GetUserAvailabilitiesForSystemSuggestionResponse>(Convert.ToString(json)));
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
            return response;
        }

        //Modifed to check for allocation irrespective of the start and end dates
        public async Task<bool> IsUserAlreadyAllocatedForSameProjectInBetweenDates(string email, string pipelineCode, string? jobCode, DateOnly startDate, DateOnly endDate, Guid? requisitionIdToAvoid)
        {
            var publishedAllocs = _allocationDbContext.PublishedResAllocDays
                .Where(m =>
                    !String.IsNullOrEmpty(m.EmailId)
                    && m.EmailId.ToLower().Trim() == email.ToLower().Trim()
                    && m.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                    && (
                        (String.IsNullOrEmpty(m.JobCode) && (String.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null"))
                        || (!String.IsNullOrEmpty(m.JobCode) && !String.IsNullOrEmpty(jobCode) && m.JobCode.ToLower().Trim() == jobCode.ToLower().Trim())
                    )
                    //&& m.AllocationDate >= startDate
                    //&& m.AllocationDate <= endDate
                    && (
                        requisitionIdToAvoid == null
                        || (requisitionIdToAvoid != null && m.RequisitionId != requisitionIdToAvoid)
                    )
                 )
                .Any();
            if (publishedAllocs)
            {
                return publishedAllocs;
            }

            var unpublishedAllocs = _allocationDbContext.UnPublishedResAllocDays
                .Include(m => m.ResAlloc)
                .Include(m => m.ResAlloc.ResourceAllocationsDetails)
                .Where(m =>
                    !String.IsNullOrEmpty(m.EmailId)
                    && m.EmailId.ToLower().Trim() == email.ToLower().Trim()
                    && m.PipelineCode.ToLower().Trim() == pipelineCode.ToLower().Trim()
                    && (
                        (m.JobCode == null && (jobCode == null || jobCode == "undefined" || jobCode == "null"))
                        || (!String.IsNullOrEmpty(m.JobCode) && !String.IsNullOrEmpty(jobCode) && m.JobCode.ToLower().Trim() == jobCode.ToLower().Trim())
                    )
                    //&& m.AllocationDate >= startDate
                    //&& m.AllocationDate <= endDate
                    && m.ResAlloc.ResourceAllocationsDetails.AllocationStatus.ToLower() != AllocationStatuses.DRAFT.ToLower()
                 )
                .Any();
            return unpublishedAllocs;
        }

        private async Task<bool> CleanUnPublishedAllocationByRequisitionId(Guid? requisitionId, Guid? allocationDetailId, bool deleteSkill)
        {
            if (requisitionId != null)
            {
                var unpublishedDays = await _allocationDbContext.UnPublishedResAllocDays
                    .Where(m => m.RequisitionId == requisitionId)
                    .ToListAsync();

                _allocationDbContext.UnPublishedResAllocDays
                    .RemoveRange(unpublishedDays);
            }
            else if (allocationDetailId != null)
            {

                if (deleteSkill)
                {
                    var skillsToDelete = await _allocationDbContext.UnPublishedResAllocSkillEntity
                        .Where(m => m.UnPublishedResAllocDetailsId == allocationDetailId)
                        .ToListAsync();

                    _allocationDbContext.UnPublishedResAllocSkillEntity
                        .RemoveRange(skillsToDelete);
                }

                var unpublishedAlloc = await _allocationDbContext.UnPublishedResAlloc
                    .Include(m => m.ResourceAllocationsDays)
                    .Where(m => m.UnPublishedResAllocDetailsId == allocationDetailId)
                    .ToListAsync();

                _allocationDbContext.UnPublishedResAlloc
                    .RemoveRange(unpublishedAlloc);
            }

            await _allocationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> GetListOfAllocationByGuidHandler(List<Guid> guidRequest, bool? discardInactiveRecords = true)
        {
            var finalResponse = new List<ResourceAllocationDetailsResponse>();

            foreach (var guid in guidRequest)
            {
                try
                {
                    var unpublishedRecord = await _allocationDbContext.UnPublishedResAllocDetails
                        .Where(m => m.Id == guid
                                && (
                                    (discardInactiveRecords == true && m.IsActive == true)
                                    || discardInactiveRecords == false
                                ))
                        .Include(m => m.Requisition)
                        .Include(m => m.Requisition.RequisitionType)
                        .Include(m => m.ResourceAllocations)
                        .Include(m => m.Skills)
                         .FirstOrDefaultAsync();

                    if (unpublishedRecord != null)
                    {
                        var response = await TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<UnPublishedResAllocDetails> { unpublishedRecord });
                        if (response != null)
                        {
                            finalResponse.Add(response.FirstOrDefault());
                        }
                    }
                    else
                    {
                        var publishedRecord = await _allocationDbContext.PublishedResAllocDetails
                            .Where(m => m.Id == guid
                                    && (
                                        (discardInactiveRecords == true && m.IsActive == true)
                                        || discardInactiveRecords == false
                                    ))
                            .Include(m => m.Requisition)
                            .Include(m => m.Skills)
                             .FirstOrDefaultAsync();

                        if (publishedRecord != null)
                        {
                            var response = await TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(new List<PublishedResAllocDetails> { publishedRecord });
                            finalResponse.Add(response.FirstOrDefault());
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return finalResponse;
        }


        public async Task<List<DraftTimesheetResponse>> GetDraftTimesheet(List<DateTime> dates)
        {
            List<DateOnly> datesRequested = dates.Select(m => DateOnly.FromDateTime(m)).ToList();

            string activityId = _configuration.GetSection("MicroserviceApiSettings").GetSection("DraftTimesheetActivityId").Value;
            string groupActivityId = _configuration.GetSection("MicroserviceApiSettings").GetSection("DraftTimesheetGroupActivityId").Value;
            string narration = _configuration.GetSection("MicroserviceApiSettings").GetSection("DraftTimesheetNarration").Value;

            var allocationDates = await _allocationDbContext.PublishedResAllocDays
                .Where(m => datesRequested.Contains(m.AllocationDate))
                .OrderBy(m => m.AllocationDate)
                .ToListAsync();

            return allocationDates.Select(m => new DraftTimesheetResponse
            {
                PipelineCode = m.PipelineCode,
                JobCode = m.JobCode,
                EmployeeId = string.IsNullOrEmpty(m.EmailId) ? "" : m.EmailId.Split("__").FirstOrDefault() ?? "",
                EmployeeEmail = m.EmailId?.Split("__").Length > 1 ? m.EmailId.Split("__")[1] : "",
                AllocatedHours = (int)m.Efforts,
                AllocationDate = m.AllocationDate,
                JobId = m.JobId
                //ActivityId = string.IsNullOrEmpty(activityId) ? "" : activityId,
                //GroupActivityId = string.IsNullOrEmpty(groupActivityId) ? "" : groupActivityId,
                //Narration = string.IsNullOrEmpty(narration) ? "" : narration,
            })
            .ToList();
        }


        public async Task<List<TimesheetDaysReponseDTO>> GetTimesheetDaysReponse(string jobCode, string timeOption, DateTime startDate, DateTime endDate)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            List<TimesheetDaysReponseDTO> result = new();
            try
            {
                //string query = @" CALL  " + Constants.TimesheetSP + " ('" + timeOption + "','" + jobCode + "','" + startDate + "','" + endDate + "' ); FETCH ALL FROM \"rs_resultone\"";
                string query = @" CALL  " + "sp_timesheet_view" + "(@timeoptionname,@injobcode,@startdate,@enddate ); FETCH ALL FROM \"rs_resultone\"";

                DataSet ds = new DataSet();
                string? pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);
                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new(query, conn);

                cmd.Parameters.AddWithValue("timeoptionname", NpgsqlDbType.Text, timeOption);
                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, String.IsNullOrEmpty(jobCode) ? String.Empty : jobCode);
                cmd.Parameters.AddWithValue("startdate", NpgsqlDbType.Date, startDate);
                cmd.Parameters.AddWithValue("enddate", NpgsqlDbType.Date, endDate);

                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(ds);
                myAdapter.Dispose();

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[1].AsEnumerable().Select(wcgt => new TimesheetDaysReponseDTO
                    {
                        designation = wcgt.Field<string>("designation") + string.Empty,
                        monthname = wcgt.Field<DateTime>("monthname"),
                        totaltime = wcgt.Field<Int64>("totaltime"),
                        timesheetcost = wcgt.Field<Int64>("timesheetcost"),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return result;
        }

        public async Task<List<TimesheetResponseDTO>> GetProjectDesignationTimesheet(string jobCode)
        {
            var userGradeData = await _allocationDbContext.PublishedResAllocDays
                .Where(t =>
                    !String.IsNullOrEmpty(t.JobCode)
                    && !String.IsNullOrEmpty(jobCode)
                    && t.JobCode.ToLower() == jobCode.Trim().ToLower()
                )
                .Select(m => new TimesheetResponseDTO()
                {
                    Gradename = m.Requisition.Grade,
                    TimesheetCost = m.ActualEffort != null ? (int)m.ActualEffort * (int)m.RatePerHour : 0,
                    TimesheetHrs = m.ActualEffort != null ? (int)m.ActualEffort : 0,
                })
                .ToListAsync();

            var groupedResponse = userGradeData
                .GroupBy(u => u.Gradename)
                .ToList();
            List<TimesheetResponseDTO> result = new();

            if (groupedResponse.Count > 0)
            {
                foreach (var group in groupedResponse)
                {
                    result.Add(new()
                    {
                        Gradename = group.Key,
                        TimesheetCost = group.Sum(a => a.TimesheetCost),
                        TimesheetHrs = group.Sum(a => a.TimesheetHrs),
                    });
                }
            }
            return result;
        }

        public async Task<int> GetAllocationCount(string pipelinecode, string jobCode)
        {
            var alloc =  await _allocationDbContext.PublishedResAllocDetails
                .Include( a => a.Requisition)
                .Where(m =>
                    !String.IsNullOrEmpty(m.Requisition.JobCode)
                    && !String.IsNullOrEmpty(jobCode)
                    && m.Requisition.JobCode.ToLower().Trim() == jobCode.ToLower().Trim()
                    && m.Requisition.PipelineCode.ToLower().Trim() == pipelinecode.ToLower().Trim()
                )
                .ToListAsync();

            return alloc.Count();
        }

        public async Task<int> GetRequisitionCount(string pipelinecode, string jobCode)
        {
            var alloc = await _allocationDbContext.Requisition                
                .Where(m =>
                    !String.IsNullOrEmpty(m.JobCode)
                    && !String.IsNullOrEmpty(jobCode)
                    && m.JobCode.ToLower().Trim() == jobCode.ToLower().Trim()
                    && m.PipelineCode.ToLower().Trim() == pipelinecode.ToLower().Trim()
                )
                .ToListAsync();

            return alloc.Count();
        }

        public async Task<List<ResourceTimesheetDTO>> GetResourceTimesheetDataByJobCode(string jobCode, DateTime startDate, DateTime endDate)
        {
            var usersData = await _allocationDbContext.PublishedResAllocDays
                .Where(m =>
                    !String.IsNullOrEmpty(m.JobCode)
                    && !String.IsNullOrEmpty(jobCode)
                    && m.JobCode.ToLower().Trim() == jobCode.ToLower().Trim()
                    && m.AllocationDate >= DateOnly.FromDateTime(startDate)
                    && m.AllocationDate <= DateOnly.FromDateTime(endDate)
                )
                .ToListAsync();

            var groupedResponse = usersData
                .GroupBy(u => u.EmailId)
                .ToList();
            List<ResourceTimesheetDTO> result = new();

            if (groupedResponse.Count > 0)
            {
                foreach (var group in groupedResponse)
                {
                    result.Add(new()
                    {
                        email_id = group.Key,
                        timesheetcost = group.Sum(m => m.ActualEffort != null ? (int)m.ActualEffort * (int)m.RatePerHour : 0),
                        totaltime = group.Sum(m => m.ActualEffort != null ? (int)m.ActualEffort : 0)
                    });
                }
            }
            return result;
        }
    }
}
//update code

