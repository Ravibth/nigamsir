using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Infrastructure.Util
{
    public class AddAllocationHistory
    {

        public static async Task<Boolean> AddAllocationHistoryTables(AllocationDbContext _allocationDbContext, ResourceAllocationDetails? _resAllocationDetails)
        {
            var result = await UpdateAllAllocastionHistor(_allocationDbContext, _resAllocationDetails);
            if (result)
            {
                Task<Boolean> _addResourceAllocationDetailsHistory = addResourceAllocationDetailsHistory(_allocationDbContext, _resAllocationDetails);
                Task<Boolean> _addResourceAllocationHistory = addResourceAllocationsHistory(_allocationDbContext, _resAllocationDetails);
                Task<Boolean> _addResourceAllocationDaysHistory = addResourceAllocationDaysHistory(_allocationDbContext, _resAllocationDetails);
                await Task.WhenAll(_addResourceAllocationHistory, _addResourceAllocationDetailsHistory, _addResourceAllocationDaysHistory);

                Boolean result1 = _addResourceAllocationDetailsHistory.Result;
                Boolean result2 = _addResourceAllocationHistory.Result;
                Boolean result3 = _addResourceAllocationDaysHistory.Result;
                if (result1 && result2 && result3)
                {
                    await _allocationDbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Add Resource Allocation in ResourceAllocationDetailsHistory Table
        /// </summary>
        /// <param name="_allocationDbContext">AllocationDBContext</param>
        /// <param name="_resAllocationDetails">ResourceAllocationDetails</param>
        /// <returns>true/false</returns>

        public static async Task<Boolean> addResourceAllocationDetailsHistory(AllocationDbContext _allocationDbContext, ResourceAllocationDetails? _resAllocationDetails)
        {
            var resAllocationDetailsHistory = new ResourceAllocationDetailsHistory
            {
                AllocationEndDate = _resAllocationDetails.AllocationEndDate,
                AllocationStatus = _resAllocationDetails.AllocationStatus,
                AllocationStartDate = _resAllocationDetails.AllocationStartDate,
                CreatedBy = _resAllocationDetails.CreatedBy,
                CreatedDate = _resAllocationDetails.CreatedDate,
                Description = _resAllocationDetails.Description,
                EmpEmail = _resAllocationDetails.EmpEmail,
                EmpName = _resAllocationDetails.EmpName,
                IsActive = _resAllocationDetails.IsActive,
                IsContinuousAllocation = _resAllocationDetails.IsContinuousAllocation,
                JobCode = _resAllocationDetails.JobCode,
                JobName = _resAllocationDetails.JobName,
                PipelineCode = _resAllocationDetails.PipelineCode,
                //ProjectCode = _resAllocationDetails.ProjectCode,
                RecordType = _resAllocationDetails.RecordType,
                PipelineName = _resAllocationDetails.PipelineName,
                ModifiedBy = _resAllocationDetails.ModifiedBy,
                ModifiedDate = _resAllocationDetails.ModifiedDate,
                RequisitionId = _resAllocationDetails.RequisitionId,
                ResourceAllocation = _resAllocationDetails.ResourceAllocation,
                Requisitions = _resAllocationDetails.Requisitions,
                ResourceAllocationDetailId = _resAllocationDetails.guid,
                //ResourceAllocationDetailsSkills = _resAllocationDetails.ResourceAllocationDetailsSkills,
                SuspendedAt = _resAllocationDetails.SuspendedAt,
                TotalEffort = _resAllocationDetails.TotalEffort
            };
            await _allocationDbContext.Set<ResourceAllocationDetailsHistory>().AddAsync(resAllocationDetailsHistory);
            return true;
        }

        /// <summary>
        /// Add Resource Allocation in ResourceAllocationHistory Table
        /// </summary>
        /// <param name="_allocationDbContext">AllocationDBContext</param>
        /// <param name="_resAllocationDetails">ResourceAllocationDetails</param>
        /// <returns>true/false</returns>

        public static async Task<Boolean> addResourceAllocationsHistory(AllocationDbContext _allocationDbContext, ResourceAllocationDetails? _resAllocationDetails)
        {

            var _resAllocations = _allocationDbContext.ResourceAllocation.Where(a => a.RequisitionId == _resAllocationDetails.RequisitionId
              && a.IsActive == true).ToList();
            if (_resAllocations != null)
            {
                foreach (var _resAllocation in _resAllocations)
                {
                    var _resourceAllocation = new ResourceAllocationHistory
                    {
                        AllocationStatus = _resAllocation.AllocationStatus,
                        ClientName = _resAllocation.ClientName,
                        ConfirmedAllocationEndDate = _resAllocation.ConfirmedAllocationEndDate,
                        ConfirmedAllocationStartDate = _resAllocation.ConfirmedAllocationStartDate,
                        ConfirmedPerDayHours = _resAllocation.ConfirmedPerDayHours,
                        CreatedBy = _resAllocation.CreatedBy,
                        CreatedDate = _resAllocation.CreatedDate,
                        EmpEmail = _resAllocation.EmpEmail,
                        EmpName = _resAllocation.EmpName,
                        IsActive = _resAllocation.IsActive,
                        isPerDayHourAllocation = _resAllocation.isPerDayHourAllocation,
                        isPublish = _resAllocation.isPublish,
                        JobCode = _resAllocation.JobCode,
                        JobName = _resAllocation.JobName,
                        ModifiedBy = _resAllocation.ModifiedBy,
                        ModifiedDate = _resAllocation.ModifiedDate,
                        PipelineCode = _resAllocation.PipelineCode,
                        PipelineName = _resAllocation.PipelineName,
                        //ProjectCode = _resAllocation.ProjectCode,
                        RecordType = _resAllocation.RecordType,
                        Requisitions = _resAllocation.Requisitions,
                        RequisitionId = _resAllocation.RequisitionId,
                        ResAllocDetailsId = _resAllocation.ResAllocDetailsId,
                        ResourceAllocationDetailGuid = _resAllocationDetails.guid,
                        ResourceAllocationDetails = _resAllocation.ResourceAllocationDetails,
                        //Skills = _resAllocation.Skills,
                        SuspendedAt = _resAllocation.SuspendedAt,
                        TotalWorkingDays = _resAllocation.TotalWorkingDays,
                    };
                    await _allocationDbContext.Set<ResourceAllocationHistory>().AddAsync(_resourceAllocation);
                }
            }
            return true;
        }
        /// <summary>
        /// Add Resource Allocation in ResourceAllocationDaysHistory Table
        /// </summary>
        /// <param name="_allocationDbContext">AllocationDBContext</param>
        /// <param name="_resAllocationDetails">ResourceAllocationDetails</param>
        /// <returns>true/false</returns>
        public static async Task<Boolean> addResourceAllocationDaysHistory(AllocationDbContext _allocationDbContext, ResourceAllocationDetails? _resAllocationDetails)
        {
            var _resAllocationsDays = _allocationDbContext.ResourceAllocationDays.Where(a => a.RequisitionId == _resAllocationDetails.RequisitionId
               && a.IsActive == true).ToList();
            if (_resAllocationDetails != null)
            {
                foreach (var _resAllocationDay in _resAllocationsDays)
                {
                    var _resourceAllocationDay = new ResourceAllocationDaysHistory
                    {
                        ConfirmedAllocationStartDate = _resAllocationDay.ConfirmedAllocationStartDate,
                        ConfirmedPerDayHours = _resAllocationDay.ConfirmedPerDayHours,
                        CreatedBy = _resAllocationDay.CreatedBy,
                        CreatedDate = _resAllocationDay.CreatedDate,
                        EmpEmail = _resAllocationDay.EmpEmail,
                        EmpName = _resAllocationDay.EmpName,
                        IsActive = _resAllocationDay.IsActive,
                        isPerDayHourAllocation = _resAllocationDay.isPerDayHourAllocation,
                        JobCode = _resAllocationDay.JobCode,
                        JobName = _resAllocationDay.JobName,
                        ModifiedBy = _resAllocationDay.ModifiedBy,
                        ModifiedDate = _resAllocationDay.ModifiedDate,
                        PipelineCode = _resAllocationDay.PipelineCode,
                        PipelineName = _resAllocationDay.PipelineName,
                        //ProjectCode= _resAllocationDay.ProjectCode,
                        RequisitionId = _resAllocationDay.RequisitionId,
                        ResAllocDetails = _resAllocationDay.ResAllocDetails,
                        Requisitions = _resAllocationDay.Requisitions,
                        ResAllocDetailsId = _resAllocationDay.ResAllocDetailsId,
                        ResAlloctionDetails = _resAllocationDay.ResAlloctionDetails,
                        ResAlloctionId = _resAllocationDay.ResAlloctionId,
                        ResourceAllocationDetailGuid = _resAllocationDetails.guid,
                        SuspendedAt = _resAllocationDay.SuspendedAt
                    };
                    await _allocationDbContext.Set<ResourceAllocationDaysHistory>().AddAsync(_resourceAllocationDay);
                }
            }
            return true;
        }
        /// <summary>
        /// Deactive All Requistion related item from History table.
        /// </summary>
        /// <param name="_allocationDbContext">AllocationDBContext</param>
        /// <param name="_resAllocationDetails">ResourceAllocationDetails</param>
        /// <returns>true/false</returns>
        public static async Task<Boolean> UpdateAllAllocastionHistor(AllocationDbContext _allocationDbContext, ResourceAllocationDetails? _resAllocationDetails)
        {
            //******************* ResourceAllocationDetailsHistory Table **************************/
            var _resourceAllocationDetailsHistory = _allocationDbContext.ResourceAllocationDetailsHistory.Where(a => a.RequisitionId == _resAllocationDetails.RequisitionId
              && a.IsActive == true).ToList();
            _resourceAllocationDetailsHistory.ForEach(item => item.IsActive = false);
            _resourceAllocationDetailsHistory.ForEach(item => item.IsActive = false);

            //******************* ResourceAllocationHistory Table **************************/

            var _resourceAllocationHistory = _allocationDbContext.ResourceAllocationHistory.Where(a => a.RequisitionId == _resAllocationDetails.RequisitionId
             && a.IsActive == true).ToList();
            _resourceAllocationHistory.ForEach(item => item.IsActive = false);

            //******************* ResourceAllocationDaysHistory Table **************************/
            var _resourceAllocationDaysHistory = _allocationDbContext.ResourceAllocationDaysHistory.Where(a => a.RequisitionId == _resAllocationDetails.RequisitionId
             && a.IsActive == true).ToList();
            _resourceAllocationDaysHistory.ForEach(item => item.IsActive = false);

            _allocationDbContext.ResourceAllocationDetailsHistory.UpdateRange(_resourceAllocationDetailsHistory);
            _allocationDbContext.ResourceAllocationHistory.UpdateRange(_resourceAllocationHistory);
            _allocationDbContext.ResourceAllocationDaysHistory.UpdateRange(_resourceAllocationDaysHistory);

            await _allocationDbContext.SaveChangesAsync();
            return true;
        }

        public static async Task<Boolean> RejectedAllocationByWorkflow(AllocationDbContext _allocationDbContext, Guid requisitionGuid)
        {
            if (requisitionGuid != null)
            {
                var _resAllocationDetails = _allocationDbContext.ResourceAllocationDetails.Where(a => a.guid == requisitionGuid && a.IsActive == true).FirstOrDefault();
                if (_resAllocationDetails != null)
                {
                    Task<Boolean> _addResourceAllocation = addResourceAllocationDetails(_allocationDbContext, _resAllocationDetails.RequisitionId);
                    Task<Boolean> _addResourceAllocations = addResourceAllocation(_allocationDbContext, _resAllocationDetails.RequisitionId);
                    Task<Boolean> _addResourceAllocationDays = addResourceAllocationDays(_allocationDbContext, _resAllocationDetails.RequisitionId);
                    await Task.WhenAll(_addResourceAllocation, _addResourceAllocations, _addResourceAllocationDays);

                    Boolean result1 = _addResourceAllocation.Result;
                    Boolean result2 = _addResourceAllocations.Result;
                    Boolean result3 = _addResourceAllocationDays.Result;
                    if (result1 && result2 && result3)
                    {
                        await _allocationDbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public static async Task<Boolean> addResourceAllocationDetails(AllocationDbContext _allocationDbContext, long requisitionId)
        {

            var _resAllocationDetails = _allocationDbContext.ResourceAllocationDetails.Where(a => a.RequisitionId == requisitionId
              && a.IsActive == true).FirstOrDefault();
            if (_resAllocationDetails != null)
            {
                _resAllocationDetails.IsActive = true;
                _allocationDbContext.ResourceAllocationDetails.UpdateRange(_resAllocationDetails);
                //await _allocationDbContext.SaveChangesAsync();
            }
            var _resAllocationDetailsHistory = _allocationDbContext.ResourceAllocationDetailsHistory.Where(a => a.RequisitionId == requisitionId
               && a.IsActive == true).FirstOrDefault();
            if (_resAllocationDetailsHistory != null)
            {
                var resAllocationDetails = new ResourceAllocationDetails
                {
                    AllocationEndDate = _resAllocationDetailsHistory.AllocationEndDate,
                    AllocationStatus = _resAllocationDetailsHistory.AllocationStatus,
                    AllocationStartDate = _resAllocationDetailsHistory.AllocationStartDate,
                    CreatedBy = _resAllocationDetailsHistory.CreatedBy,
                    CreatedDate = _resAllocationDetailsHistory.CreatedDate,
                    Description = _resAllocationDetailsHistory.Description,
                    EmpEmail = _resAllocationDetailsHistory.EmpEmail,
                    EmpName = _resAllocationDetailsHistory.EmpName,
                    IsActive = _resAllocationDetailsHistory.IsActive,
                    IsContinuousAllocation = _resAllocationDetailsHistory.IsContinuousAllocation,
                    JobCode = _resAllocationDetailsHistory.JobCode,
                    JobName = _resAllocationDetailsHistory.JobName,
                    PipelineCode = _resAllocationDetailsHistory.PipelineCode,
                    //ProjectCode = _resAllocationDetailsHistory.ProjectCode,
                    RecordType = _resAllocationDetailsHistory.RecordType,
                    PipelineName = _resAllocationDetailsHistory.PipelineName,
                    ModifiedBy = _resAllocationDetailsHistory.ModifiedBy,
                    ModifiedDate = _resAllocationDetailsHistory.ModifiedDate,
                    RequisitionId = _resAllocationDetailsHistory.RequisitionId,
                    ResourceAllocation = _resAllocationDetailsHistory.ResourceAllocation,
                    Requisitions = _resAllocationDetailsHistory.Requisitions,
                    //ResourceAllocationDetailsSkills = _resAllocationDetailsHistory.ResourceAllocationDetailsSkills,
                    SuspendedAt = _resAllocationDetailsHistory.SuspendedAt,
                    TotalEffort = _resAllocationDetailsHistory.TotalEffort
                };
                await _allocationDbContext.Set<ResourceAllocationDetails>().AddAsync(resAllocationDetails);
            }
            return true;
        }
        public static async Task<Boolean> addResourceAllocation(AllocationDbContext _allocationDbContext, long requisitionId)
        {
            var _resAllocations = _allocationDbContext.ResourceAllocationHistory.Where(a => a.RequisitionId == requisitionId
               && a.IsActive == true).ToList();

            var _resAllocationList = _allocationDbContext.ResourceAllocation.Where(a => a.RequisitionId == requisitionId
              && a.IsActive == true).ToList();

            if (_resAllocationList != null)
            {
                _resAllocationList.ForEach(item => item.IsActive = false);
                _allocationDbContext.ResourceAllocation.UpdateRange(_resAllocationList);
                //await _allocationDbContext.SaveChangesAsync();
            }

            if (_resAllocations != null)
            {
                foreach (var _resAllocation in _resAllocations)
                {
                    var _resourceAllocation = new ResourceAllocation
                    {
                        AllocationStatus = _resAllocation.AllocationStatus,
                        ClientName = _resAllocation.ClientName,
                        ConfirmedAllocationEndDate = _resAllocation.ConfirmedAllocationEndDate,
                        ConfirmedAllocationStartDate = _resAllocation.ConfirmedAllocationStartDate,
                        ConfirmedPerDayHours = _resAllocation.ConfirmedPerDayHours,
                        CreatedBy = _resAllocation.CreatedBy,
                        CreatedDate = _resAllocation.CreatedDate,
                        EmpEmail = _resAllocation.EmpEmail,
                        EmpName = _resAllocation.EmpName,
                        IsActive = _resAllocation.IsActive,
                        isPerDayHourAllocation = _resAllocation.isPerDayHourAllocation,
                        isPublish = _resAllocation.isPublish,
                        JobCode = _resAllocation.JobCode,
                        JobName = _resAllocation.JobName,
                        ModifiedBy = _resAllocation.ModifiedBy,
                        ModifiedDate = _resAllocation.ModifiedDate,
                        PipelineCode = _resAllocation.PipelineCode,
                        PipelineName = _resAllocation.PipelineName,
                        //ProjectCode = _resAllocation.ProjectCode,
                        RecordType = _resAllocation.RecordType,
                        Requisitions = _resAllocation.Requisitions,
                        RequisitionId = _resAllocation.RequisitionId,
                        ResAllocDetailsId = _resAllocation.ResAllocDetailsId,
                        ResourceAllocationDetails = _resAllocation.ResourceAllocationDetails,
                        //Skills = _resAllocation.Skills,
                        SuspendedAt = _resAllocation.SuspendedAt,
                        TotalWorkingDays = _resAllocation.TotalWorkingDays,
                    };
                    await _allocationDbContext.Set<ResourceAllocation>().AddAsync(_resourceAllocation);
                }
            }
            return true;
        }
        public static async Task<Boolean> addResourceAllocationDays(AllocationDbContext _allocationDbContext, long requisitionId)
        {

            var _resAllocationsDaysHistory = _allocationDbContext.ResourceAllocationDaysHistory.Where(a => a.RequisitionId == requisitionId
               && a.IsActive == true).ToList();
            var _resAllocationsDays = _allocationDbContext.ResourceAllocationDays.Where(a => a.RequisitionId == requisitionId
             && a.IsActive == true).ToList();
            if (_resAllocationsDays != null)
            {
                _resAllocationsDays.ForEach(item => item.IsActive = false);
                _allocationDbContext.ResourceAllocationDays.UpdateRange(_resAllocationsDays);
                //await _allocationDbContext.SaveChangesAsync();
            }

            if (_resAllocationsDaysHistory != null)
            {
                foreach (var _resAllocationDay in _resAllocationsDaysHistory)
                {
                    var _resourceAllocationDay = new ResAllocationDays
                    {
                        ConfirmedAllocationStartDate = _resAllocationDay.ConfirmedAllocationStartDate,
                        ConfirmedPerDayHours = _resAllocationDay.ConfirmedPerDayHours,
                        CreatedBy = _resAllocationDay.CreatedBy,
                        CreatedDate = _resAllocationDay.CreatedDate,
                        EmpEmail = _resAllocationDay.EmpEmail,
                        EmpName = _resAllocationDay.EmpName,
                        IsActive = _resAllocationDay.IsActive,
                        isPerDayHourAllocation = _resAllocationDay.isPerDayHourAllocation,
                        JobCode = _resAllocationDay.JobCode,
                        JobName = _resAllocationDay.JobName,
                        ModifiedBy = _resAllocationDay.ModifiedBy,
                        ModifiedDate = _resAllocationDay.ModifiedDate,
                        PipelineCode = _resAllocationDay.PipelineCode,
                        PipelineName = _resAllocationDay.PipelineName,
                        //ProjectCode = _resAllocationDay.ProjectCode,
                        RequisitionId = _resAllocationDay.RequisitionId,
                        ResAllocDetails = _resAllocationDay.ResAllocDetails,
                        Requisitions = _resAllocationDay.Requisitions,
                        ResAllocDetailsId = _resAllocationDay.ResAllocDetailsId,
                        ResAlloctionDetails = _resAllocationDay.ResAlloctionDetails,
                        ResAlloctionId = _resAllocationDay.ResAlloctionId,
                        SuspendedAt = _resAllocationDay.SuspendedAt
                    };
                    await _allocationDbContext.Set<ResAllocationDays>().AddAsync(_resourceAllocationDay);
                }
            }
            return true;
        }
    }
}
//updated code
