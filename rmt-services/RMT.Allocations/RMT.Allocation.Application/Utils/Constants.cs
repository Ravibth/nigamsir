using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Utils
{
    public enum UserRolesMaster
    {
        Employee,
        ResourceRequestor,
        Delegate,
        Admin,
        Reviewer,
        Leaders,
        SystemAdmin,
        CEOCOO
    }
    public class Constants
    {
        //public const string Employee = "Employee";
        //public const string ResourceRequestor = "Resource Requestor";
        //public const string Delegate = "Delegate";
        //public const string Admin = "Admin";
        //public const string Reviewer = "Reviewer";
        //public const string Leaders = "Leaders";
        //public const string SystemAdmin = "System Admin";
        public const int WorkingHours = 8;

        public static string Bearer { get; internal set; }

        public const  string chargeableType = "Chargeable";
        public const string nonChargeableType = "NonChargeable";
    }
    public static class ESystemSuggestionParameters
    {
        public const string availability = "availability";
        public const string marketplace = "marketplace";
        public const string sorting = "sorting";
        
    }

    public static class EAllocationType
    {
        public const string SYSTEM_SUGGESTED_ALLOCATION = "SYSTEM_SUGGESTED_ALLOCATION";
        public const string NAME_ALLOCATION = "NAME_ALLOCATION";
        public const string SAME_TEAM_ALLOCATION = "SAME_TEAM_ALLOCATION";
        public const string BULK_ALLOCATION = "BULK_ALLOCATION";
        public const string UPDATE_ALLOCATION = "UPDATE_ALLOCATION";
    }

    public static class EAllocationsBreakupVariables
    {
        public const string ConfirmedAllocationStartDate = "confirmedAllocationStartDate";
        public const string ConfirmedAllocationEndDate = "confirmedAllocationEndDate";
        public const string ConfirmedPerDayHours = "confirmedPerDayHours";
        public const string PerDayAllocation = "perDayAllocation";
    }
}
