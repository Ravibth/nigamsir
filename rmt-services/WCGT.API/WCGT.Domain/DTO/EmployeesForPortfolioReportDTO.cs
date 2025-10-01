using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;

namespace WCGT.Domain.DTO
{
    public class EmployeesForPortfolioReportDTO
    {
        public string Employee_mid {get;set;}
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string? Supercoach { get; set; }
        public string? Supercoach_mid { get; set; }
        public string? Cosupercoach { get; set; }
        public string? Cosupercoach_mid { get; set; }
        public string Officelocation  { get; set; }
        public string Availablevsallocated { get; set; }
        public int Allocatedhours { get; set; }

        public int Netavailablehours { get; set; }
        public string Clientgroup { get; set; }
        public string Client { get; set; }
        public string? Jobcode { get; set; }
        public string? Jobname { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Week { get; set; }
        public string Month { get; set; }
        public Dictionary<string, int> WeeklyHours { get; set; } = new();
        public string? Period { get; set; } // "W1", "M4" etc.
        public string? PeriodType { get; set; } // "Weekly" or "Monthly"
        public DateOnly? PeriodStart { get; set; }
        public DateOnly? PeriodEnd { get; set; } 


        //public EmployeesForPortfolioReportDTO Clone()
        //{
        //    return new EmployeesForPortfolioReportDTO
        //    {
        //        employee_mid = this.employee_mid,
        //        name = this.name,
        //        designation = this.designation,
        //        grade = this.grade,
        //        supercoach_mid = this.supercoach_mid,
        //        supercoach = this.supercoach,
        //        cosupercoach_mid = this.cosupercoach_mid,
        //        cosupercoach = this.cosupercoach,
        //        officelocation = this.officelocation,
        //        availablevsallocated = this.availablevsallocated,
        //        jobcode = this.jobcode,
        //        jobname = this.jobname,
        //        client = this.client,
        //        clientgroup = this.clientgroup,
        //       // week = new List<int>(this.week),
        //        WeeklyHours = new Dictionary<string, int>(this.WeeklyHours)
        //    };
        //}

    }
    public class EmployeeDailyAllocation
    {
        public string Employee { get; set; }
        public DateOnly Date { get; set; }
        public string Week { get; set; }
        public string Month { get; set; }
        public string JobCode { get; set; } // e.g., Leave, Holiday, or job code
        public int AllocatedHours { get; set; }
        public int Availability { get; set; }
        public int NetAvailability => Availability - AllocatedHours;
    }
    public class TempEmployeeAllocation
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string CoSuperCoachMid { get; set; }
        public string SuperCoachMid { get; set; }
        public string Location { get; set; }
        public DateOnly Date { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string PipelineName { get; set; }
        public long AllocationEfforts { get; set; }
        public string AvailablevsAllocated { get; set; }
        public string ClientGroup { get; set; }
        public string Client { get; set; }
        public bool IsAllocation { get; set; }
        public bool IsLeave { get; set; }
        public bool IsHoliday { get; set; }
        public long LeaveHours { get; set; }
        public long HolidayHours { get; set; }
        public string TimeBucket { get; set; }
        public string SupercoachName { get; set; }
        public string CoSupercoachName { get; set; }
    }


}


