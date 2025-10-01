using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.DTO;
using WCGT.Domain.DTOs;
using WCGT.Domain.Entities;

namespace WCGT.Domain.IRepositories
{
    public interface IWcgtDataRepository
    {
        Task<WCGTDataLog> AddWCGTDataLogEntry(WCGTDataLog input);

        Task<List<string>> GetProjectBudgetByModifiedDateRange(DateTime startDate, DateTime endDate);

        Task<Client> UpdateClient(Client clients);

        Task<Pipeline> UpdatePipeline(Pipeline pipelines);

        Task<Job> UpdateJob(Job jobs);

        Task<Employee> UpdateEmployee(Employee employees);

        Task<Competency> UpdateCompetency(Competency competency);

        Task<Designation> UpdateDesignation(Designation designations);

        Task<BUTreeMapping> UpdateBUTreeMapping(BUTreeMapping buTreeMappings);

        Task<BUEfficiencyLeaderDTO> UpdateBUEfficiencyLeader(BUEfficiencyLeaderDTO buEfficiencyLeader);

        Task<Holiday> UpdateHoliday(Holiday holiday);

        Task<Leave> UpdateLeave(Leave leave);

        Task<Location> UpdateLocation(Location location);

        Task<ClientLegalEntity> UpdateClientLegalEntity(ClientLegalEntity clientLegalEntity);

        Task<SectorIndustry> UpdateSectorIndustry(SectorIndustry sectorIndustry);

        Task<Budget> UpdateBudget(Budget input);

        Task<RateDesignationMaster> UpdateRateDesignationMaster(RateDesignationMaster input);

        Task<List<Client>> GetAllClients();
        Task<List<GetJobCodeClientDTO>> GetAllClientsByJobCode(List<string> jobcodes);

        Task<List<Employee>> GetAllEmployees();

        Task<List<Competency>> GetCompetencies(string? CompetencyId, string? CompetencyName, string? CompetencyLeaderMID, string? BuId, Boolean? isactive);

        Task<Employee?> GetEmployeeByParam(string? emp_mid, string? emp_emailid);
        Task<List<Employee>> GetEmployeeBySuperCoachOrCSC(string? emp_mid);
        Task<List<SuperCoach>> GetAllSuperCoach();
        Task<List<SuperCoach>> GetAllCoSuperCoach();
        Task<List<Designation>> GetAllDesignations();

        Task<List<BUTreeMapping>> GetAllBUTreeMappings();

        Task<List<Holiday>> GetAllHolidays();

        Task<List<Leave>> GetAllLeaves();

        Task<List<Leave>> GetAllLeavesByParam(List<string>? emp_emailid, DateTime? created_at);

        Task<List<Leave>> GetAllLeavesByCreateDate(DateTime created_at);

        Task<List<Location>> GetAllLocations();

        Task<List<ClientLegalEntity>> GetAllClientLegalEntities();

        Task<List<SectorIndustry>> GetAllSectorIndustries();

        Task<List<Pipeline>> GetAllPipelines();

        Task<List<Holiday>> GetAllHolidaysByParams(List<HolidayParamsDTO> holidayRequest);

        Task<List<Job>> GetAllJobs();
        Task<List<Job>> GetJobs();
        

        Task<Job?> GetJobByJobCode(string? pipelineCode, string jobCode);

        Task<List<TimesheetDesignationResponse>> GetProjectDesignationTimesheet(string jobCode);

        List<WcgtTimesheetGroup> GetProjectGroupTimesheet(string jobCode, string timeOption, DateTime startDate, DateTime endDate);

        List<WcgtResoureTimesheetGroup> GetResourceGroupTimesheet(string jobCode, DateTime startDate, DateTime endDate);

        Task<List<DesignationGradeView>> GetRateByDesignation(List<GetRateDesignationRequestDTO> designations);

        Task<List<Employee>> GetResignedAndAbscondedUsers(List<string> emails);

        Task<List<BUTreeMapping>> GetAllBUTreeMappingsByMID(string? mid);

        Task<List<GetUserLeaveHolidayResponseWithUserMaster>> GetUserLeavesHolidays(string designation, DateTime start_date, DateTime end_date);

        Task<List<LeaveReport>> GetLeavesInfoByStartDateEndDateAndEmails(List<string> emp_mid, DateTime start_date, DateTime end_date);
        Task<List<string>> GetCompetencyByBUIDs(List<string> buids);
        Task<List<Employee>> GetEmployeesByBUAndCompetencies(List<string> buids, List<string> competencyIds);
        Task<List<EmployeeLeavesHolidayAndAvailabity>> GetEmployeeAvailabilityWithLeavesAndHolidays(DateTime startDate, DateTime endDate, List<string> grade ,
                    List<string> designation , List<string> userEmpMids , List<string> supercoach , List<string> cosupercoach , List<string> clientname , List<string> clientgroupname,
                    List<string> business_unit_ids, List<string> competency_ids);
        Task<List<Employee>> GetEmployeeBySuperCoachOrCSCList(List<string> emp_mid);
    }
}
