using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RMT.Allocation.Infrastructure.Helpers
{
    public class CommonAllocationHelper
    {
        //TODO
        public static async Task<List<PublishedResAllocDays>> GetUserPerDayPublishAllocationsByEmailAndDates(AllocationDbContext _allocationDbContext, List<string> emails, DateTime startdate, DateTime enddate)
        {
            List<string> statusForAllocatedEmployees = new() { 
                                                Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER, 
                                                Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH, 
                                                Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE, 
                                                Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION };
            List<string> AllocatedEmployeeStatus = statusForAllocatedEmployees.Select(d => d.ToLower().Trim()).ToList();
            List<string> emailsList = emails.Select(s => s.ToLower().Trim()).ToList();
            var allocationDetails = await (from publishedResAllocDays in _allocationDbContext.PublishedResAllocDays
                                           join resourceAllocated in _allocationDbContext.PublishedResAlloc
                                               on publishedResAllocDays.RequisitionId equals resourceAllocated.RequisitionId
                                           join resourceAllocDetails in _allocationDbContext.PublishedResAllocDetails
                                               on resourceAllocated.RequisitionId equals resourceAllocDetails.RequisitionId
                                           where
                                               emailsList.ToArray().Any(p => publishedResAllocDays.EmailId.ToLower().Equals(p))
                                               && resourceAllocDetails.IsActive == true
                                               && resourceAllocDetails.AllocationStatus != "Draft"
                                           select publishedResAllocDays).ToListAsync();
            allocationDetails = allocationDetails.DistinctBy(m => m.Id).ToList();
            return allocationDetails;
        }
        //TODO
        public static async Task<List<UnPublishedResAllocDays>> GetUserPerDayUnPublishAllocationsByEmailAndDates(AllocationDbContext _allocationDbContext, List<string> emails, DateTime startdate, DateTime enddate)
        {
            try
            {
                List<string> statusForAllocatedEmployees = new()
                {
                    Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER
                    , Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH
                    , Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE
                    , Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION
                };

                List<string> AllocatedEmployeeStatus = statusForAllocatedEmployees.Select(d => d.ToLower().Trim()).ToList();
                List<string> emailsList = emails.Select(s => s.ToLower().Trim()).ToList();
                var allocationDetails = await (from publishedResAllocDays in _allocationDbContext.UnPublishedResAllocDays
                                               join resourceAllocated in _allocationDbContext.UnPublishedResAlloc
                                                   on publishedResAllocDays.RequisitionId equals resourceAllocated.RequisitionId
                                               join resourceAllocDetails in _allocationDbContext.UnPublishedResAllocDetails
                                                   on resourceAllocated.RequisitionId equals resourceAllocDetails.RequisitionId
                                               join requesitions in _allocationDbContext.Requisition
                                                   on resourceAllocated.RequisitionId equals requesitions.Id
                                               where
                                                   emailsList.ToArray().Any(p => publishedResAllocDays.EmailId.ToLower().Equals(p))
                                                   && resourceAllocDetails.IsActive == true
                                                   && resourceAllocDetails.AllocationStatus != "Draft"
                                               select publishedResAllocDays).ToListAsync();
                allocationDetails = allocationDetails.DistinctBy(m => m.Id).ToList();
                return allocationDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //checked
        public static async Task<List<PublishedResAllocDays>> GetUserPerDayPublishAllocationsByDates(AllocationDbContext _allocationDbContext, DateTime startdate, DateTime enddate)
        {
            List<string> statusForAllocatedEmployees = new() { 
                                                    Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER, 
                                                    Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH, 
                                                    Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE, 
                                                    Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION };
            List<string> AllocatedEmployeeStatus = statusForAllocatedEmployees.Select(d => d.ToLower().Trim()).ToList();
            var allocationDetails = await (from publishedResAllocDays in _allocationDbContext.PublishedResAllocDays
                                           join resourceAllocated in _allocationDbContext.PublishedResAlloc
                                               on publishedResAllocDays.RequisitionId equals resourceAllocated.RequisitionId
                                           join resourceAllocDetails in _allocationDbContext.PublishedResAllocDetails
                                               on resourceAllocated.RequisitionId equals resourceAllocDetails.RequisitionId
                                           where
                                               resourceAllocDetails.IsActive == true
                                               && resourceAllocDetails.AllocationStatus != "Draft"
                                           select publishedResAllocDays).ToListAsync();
            return allocationDetails;
        }
        //checked
        public static async Task<List<UnPublishedResAllocDays>> GetUserPerDayUnPublishAllocationsByDates(AllocationDbContext _allocationDbContext, DateTime startdate, DateTime enddate)
        {
            List<string> statusForAllocatedEmployees = new() { 
                                                        Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER, 
                                                        Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH, 
                                                        Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE, 
                                                        Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION };
            List<string> AllocatedEmployeeStatus = statusForAllocatedEmployees.Select(d => d.ToLower().Trim()).ToList();
            var allocationDetails = await (from publishedResAllocDays in _allocationDbContext.UnPublishedResAllocDays
                                           join resourceAllocated in _allocationDbContext.UnPublishedResAlloc
                                               on publishedResAllocDays.RequisitionId equals resourceAllocated.RequisitionId
                                           join resourceAllocDetails in _allocationDbContext.UnPublishedResAllocDetails
                                               on resourceAllocated.RequisitionId equals resourceAllocDetails.RequisitionId
                                           where
                                               resourceAllocDetails.IsActive == true
                                               && resourceAllocDetails.AllocationStatus != "Draft"
                                           select publishedResAllocDays).ToListAsync();

            return allocationDetails;
        }

        public static async Task<long> GetTotalAllocationByRequisition(AllocationDbContext _allocationDbContext, Guid requisitionId)
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Today);
            long totalHours = 0;
            var allocation_per_days = await _allocationDbContext.PublishedResAllocDays
                .Where(m => m.AllocationDate < todayDate && m.RequisitionId == requisitionId).ToListAsync();
            if (allocation_per_days != null && allocation_per_days.Count > 0)
            {
                totalHours = allocation_per_days.Sum(m => m.Efforts);
            }
            else
            {
                var unpublish_allocation_per_days = await _allocationDbContext.UnPublishedResAllocDays
                  .Where(m => m.AllocationDate < todayDate && m.RequisitionId == requisitionId).ToListAsync();
                if (unpublish_allocation_per_days != null && unpublish_allocation_per_days.Count > 0)
                {
                    totalHours = unpublish_allocation_per_days.Sum(m => m.Efforts);
                }
            }
            return totalHours;
        }
    }
}
