using Microsoft.EntityFrameworkCore;
using Npgsql;
using RMT.Reports.Domain.Entities;
using RMT.Reports.Infrastructure.Data;
using RMT.Reports.Infrastructure.Infra.Request;
using RMT.Reports.Infrastructure.Infra.Response;
using System.Data.SqlTypes;
using System.Data;
using Newtonsoft.Json;
using RMT.Reports.Infrastructure.Repositories;
using NpgsqlTypes;
using Microsoft.Extensions.Configuration;
using RMT.Reports.Infrastructure.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Linq;

namespace RMT.Report.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        protected readonly ReportsDBContext _dbContext;
        string emailSeperator = "__";
        Int16 emailPartIndexMID = 0;
        Int16 emailPartIndexEmail = 1;

        string ConnStr_ReportDB1;

        public IConfiguration Configuration { get; }

        public ReportRepository(ReportsDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Configuration = configuration;

            ConnStr_ReportDB1 = configuration.GetConnectionString("ReportsDB");

        }

        public async Task<List<EmployeeAllocationTimeSheetEntity>> GetEmployeeAllocationTimeSheet(EmployeeAllocationTimeSheetEntity args)
        {

            Console.Write("this method has called ");
            var result = await _dbContext.EmployeeAllocationTimeSheet.FromSqlInterpolated($"SELECT * FROM employee_allocation_timesheet limit").ToListAsync();
            Console.Write("result fetched");

            return result;

        }

        public async Task<bool> RefreshEmployeeAllocationView()
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"REFRESH MATERIALIZED VIEW CONCURRENTLY employee_allocation_timesheet;");
            return true;
        }

        public async Task<bool> RefreshEmployeeWorkingDaysView()
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"REFRESH MATERIALIZED VIEW CONCURRENTLY employee_working_days;");
            return true;
        }

        public async Task<bool> RefreshProjectBudgetView()
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"REFRESH MATERIALIZED VIEW CONCURRENTLY project_budget;");
            return true;
        }

        public async Task<bool> RefreshMaterializedViews(List<string> views)
        {
            string connectionString = ConnStr_ReportDB1;
            DataTable table = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(connectionString))
                    {
                        command.Connection = connection;

                        command.CommandText = $"select refresh_materialized_views_function(ARRAY[{string.Join(", ", views.Select(v => $"'{v}'"))}])";

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return true;
        }

        public async Task<List<CapacityUtilizationOverviewResponseInfra>> GetCapacityUtilizationOverview(CapacityUtiliationOverviewRequestInfra args)
        {
            string connectionString = ConnStr_ReportDB1;//@$"Server=localhost;Database={DB_REPORT};UserId=postgres;Password={PASSWORD_DB};";
            DataTable table = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(connectionString))
                    {
                        command.Connection = connection;
                        string whereClause = " WHERE ";
                        if (args.BusinessUnit != null && args.BusinessUnit.Count > 0)
                        {
                            SqlServerInClauseParam<string> BusinessUnit = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.BusinessUnit.Select(a => a.ToLower().Trim()).ToList(), "business_unit");
                            whereClause += " TRIM(LOWER(business_unit)) IN ( " + BusinessUnit.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(BusinessUnit.Params());
                        }

                        if (args.Competency != null && args.Competency.Count > 0)
                        {
                            SqlServerInClauseParam<string> Competency = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Competency.Select(a => a.ToLower().Trim()).ToList(), "competency");
                            whereClause += " TRIM(LOWER(competency)) IN ( " + Competency.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Competency.Params());
                        }

                        if (args.Location != null && args.Location.Count > 0)
                        {
                            SqlServerInClauseParam<string> Location = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Location.Select(a => a.ToLower().Trim()).ToList(), "location");
                            whereClause += " TRIM(LOWER(location)) IN ( " + Location.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Location.Params());
                        }

                        if (args.Designation != null && args.Designation.Count > 0)
                        {
                            SqlServerInClauseParam<string> Designation = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Designation.Select(a => a.ToLower().Trim()).ToList(), "designation_name");
                            whereClause += " TRIM(LOWER(designation_name)) IN ( " + Designation.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Designation.Params());
                        }

                        bool checkEmployeeData = false;

                        //if (args.Offering != null && args.Offering.Count > 0)
                        //{
                        //    checkEmployeeData = true;
                        //}

                        //if (args.Solution != null && args.Solution.Count > 0)
                        //{
                        //    checkEmployeeData = true;
                        //}

                        //if (args.CheckEmpMids && checkEmployeeData)
                        //{
                        //    List<string> distinctEmpIds = args.EmpMids != null ?
                        //        args.EmpMids.Distinct().ToList<string>() : new List<string>();

                        //    if (distinctEmpIds != null && distinctEmpIds.Count > 0)
                        //    {
                        //        SqlServerInClauseParam<string> empids = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, distinctEmpIds.Select(a => a.ToLower().Trim()).ToList(), "empids");
                        //        whereClause += " TRIM(LOWER(employee_mid)) IN ( " + empids.ParamsString() + " ) AND ";
                        //        command.Parameters.AddRange(empids.Params());
                        //    }
                        //    else
                        //    {
                        //        //Universal false condition to show no records
                        //        whereClause += " 1=2 AND ";
                        //    }
                        //}

                        whereClause += " working_date between @start_date ::Date and @end_date ::Date ";
                        command.Parameters.AddWithValue("@start_date", args.StartDate);
                        command.Parameters.AddWithValue("@end_date", args.EndDate);

                        //string sql = $@"select competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable,0))  job_chargeable, SUM(COALESCE(job_non_chargeable,0))  job_non_chargeable, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability from employee_allocation_timesheet {whereClause} group by business_unit, competency";
                        //string sql = $@"select designation_name, department, location, email_id,employee_name, competency, business_unit, leave_hrs, SUM(capacity * hourly_rate)  capacity_cost,SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(actual_cost,0))  actual_cost,SUM(COALESCE(allocated_cost,0))  allocated_cost,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable_hours,0))  job_chargeable_hours, SUM(COALESCE(job_non_chargeable_hours,0))  job_non_chargeable_hours,SUM(COALESCE(job_chargeable_cost,0))  job_chargeable_cost, SUM(COALESCE(job_non_chargeable_cost,0))  job_non_chargeable_cost, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability , SUM((COALESCE(capacity,0) - COALESCE(allocation_hours,0)) * hourly_rate) as availability_cost  from employee_allocation_timesheet {whereClause} group by business_unit, leave_hrs, competency, email_id , employee_name,location , department , designation_name";
                        /*string sql = $@"select designation_name, grade, department, location, employee_mid, email_id, employee_name, competency, business_unit, SUM(capacity * hourly_rate)  capacity_cost,
                            SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(actual_cost,0))  actual_cost,SUM(COALESCE(allocated_cost,0))  allocated_cost,SUM(COALESCE(capacity,0))  capacity,
                            SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(allocated_chargable_hr,0))  allocated_chargable_hr, 
                            SUM(COALESCE(allocated_chargable_cost,0))  allocated_chargable_cost, SUM(COALESCE(allocated_non_chargable_hr,0))  allocated_non_chargable_hr, 
                            SUM(COALESCE(allocated_non_chargable_cost,0))  allocated_non_chargable_cost,
                            CASE
	                            WHEN (SUM(COALESCE(capacity,0)) ) > 0 THEN ROUND(SUM(COALESCE(allocation_hours,0)) * 100 /SUM(COALESCE(capacity,0)),2)
	                            ELSE 0
                            END AS allocation_percent,
                            CASE
	                            WHEN (SUM(COALESCE(capacity,0)) ) > 0 THEN ROUND(SUM(COALESCE(allocated_chargable_hr,0)) * 100 /SUM(COALESCE(capacity,0)),2)
	                            ELSE 0
                            END AS allocation_chargability_percent,
                            CASE
	                            WHEN (SUM(COALESCE(capacity,0)) ) > 0 THEN ROUND(SUM(COALESCE(job_chargeable_hours,0)) * 100 /SUM(COALESCE(capacity,0)),2)
	                            ELSE 0
                            END AS actual_chargability_percent,
                            SUM(COALESCE(leave_hrs,0))  leave_hrs,
                            SUM(COALESCE(net_leaves,0))  net_leaves,
                            SUM(COALESCE(job_chargeable_hours,0))  job_chargeable_hours,
                            SUM(COALESCE(job_non_chargeable_hours,0))  job_non_chargeable_hours,SUM(COALESCE(job_chargeable_cost,0))  job_chargeable_cost, 
                            SUM(COALESCE(job_non_chargeable_cost,0))  job_non_chargeable_cost, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability , 
                            SUM((COALESCE(capacity,0) - COALESCE(allocation_hours,0)) * hourly_rate) as availability_cost  
                            from (
                                select employee_mid,
                                    employee_code,
                                    employee_name,
                                    email_id,
                                    department,
                                    location,
                                    working_date,
                                    designation_name,
                                    grade,
                                    business_unit,
                                    competency,
                                    hourly_rate,
                                    allocation_date ,
			
			                        --sum(allocation_hours) as allocation_hours
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(allocation_hours, 0)) then max(capacity)
                                        else SUM(COALESCE(allocation_hours, 0))
                                    end as allocation_hours ,
			
			                        --sum(allocated_cost) as allocated_cost
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(allocation_hours, 0)) then max(capacity) * hourly_rate
                                        else SUM(COALESCE(allocation_hours, 0)) * hourly_rate
                                    end as allocated_cost,
			
			                        --sum(allocated_chargable_hr) as allocated_chargable_hr
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(allocated_chargable_hr, 0)) then max(capacity)
                                        else SUM(COALESCE(allocated_chargable_hr, 0))
                                    end as allocated_chargable_hr,
			
			                        --sum(allocated_chargable_cost) as allocated_chargable_cost
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(allocated_chargable_hr, 0)) then max(capacity) * hourly_rate
                                        else SUM(COALESCE(allocated_chargable_hr, 0)) * hourly_rate
                                    end as allocated_chargable_cost,
			
			                        --sum(allocated_non_chargable_hr) as allocated_non_chargable_hr
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
                                        and (max(capacity) - sum(allocated_chargable_hr)) < 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
                                        and (max(capacity) - sum(allocated_chargable_hr)) >= 0 then (max(capacity) - sum(allocated_chargable_hr))
                                        else SUM(COALESCE(allocated_non_chargable_hr, 0))
                                    end as allocated_non_chargable_hr,
			
			                        --sum(allocated_non_chargable_cost) as allocated_non_chargable_cost
                                    case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
                                        and (max(capacity) - sum(allocated_chargable_hr)) < 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
                                        and (max(capacity) - sum(allocated_chargable_hr)) >= 0 then (max(capacity) - sum(allocated_chargable_hr)) * hourly_rate
                                        else SUM(COALESCE(allocated_non_chargable_hr, 0)) * hourly_rate
                                    end as allocated_non_chargable_cost,
			
                                    sum(actual_log_hours) as actual_log_hours,
			
                                    -- sum(job_chargeable_hours) as job_chargeable_hours,
			                        case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(job_chargeable_hours, 0)) then max(capacity)
                                        else SUM(COALESCE(job_chargeable_hours, 0))
                                    end as job_chargeable_hours,
			
                                    -- sum(job_chargeable_cost) as job_chargeable_cost,
			                        case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and max(capacity) < SUM(COALESCE(job_chargeable_hours, 0)) then max(capacity) * hourly_rate
                                        else SUM(COALESCE(job_chargeable_hours, 0)) * hourly_rate
                                    end as job_chargeable_cost,
			
                                    -- sum(job_non_chargeable_cost) as job_non_chargeable_cost,
			                        case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
                                        and (max(capacity) - sum(job_chargeable_hours)) < 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
                                        and (max(capacity) - sum(job_chargeable_hours)) >= 0 then (max(capacity) - sum(job_chargeable_hours)) * hourly_rate
                                        else SUM(COALESCE(job_non_chargeable_hours, 0)) * hourly_rate
                                    end as job_non_chargeable_cost,
			
                                    -- sum(job_non_chargeable_hours) as job_non_chargeable_hours,
			                        case
                                        WHEN max(capacity) = 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
                                        and (max(capacity) - sum(job_chargeable_hours)) < 0 then 0
                                        when max(capacity) > 0
                                        and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
                                        and (max(capacity) - sum(job_chargeable_hours)) >= 0 then (max(capacity) - sum(job_chargeable_hours))
                                        else SUM(COALESCE(job_non_chargeable_hours, 0))
                                    end as job_non_chargeable_hours,
			
                                    sum(actual_cost) as actual_cost,
                                    holiday_date,
                                    location_name,
                                    holiday_type,
                                    emp_mid,
                                    leave_start_date,
                                    leave_end_date,
                                    capacity,
                                    leave_hrs,
                                    net_leaves
                                from employee_allocation_timesheet
                               {whereClause}
                                group by employee_mid,
                                    employee_code,
                                    employee_name,
                                    email_id,
                                    department,
                                    location,
                                    working_date,
                                    designation_name,
                                    grade,
                                    business_unit,
                                    competency,
                                    hourly_rate,
                                    allocation_date,
                                    competency_id,
                                    competency_name,
                                    emp_mid,
                                    holiday_date,
                                    location_name,
                                    holiday_type,
                                    emp_mid,
                                    leave_start_date,
                                    leave_end_date,
                                    capacity,
                                    leave_hrs,
                                    net_leaves
                            ) et1
                        group by business_unit, competency, employee_mid, email_id , employee_name,location , department , designation_name, grade
                        order by ""employee_mid"" asc
                                ";*/

                        string sql = $@"
                         select 
                             designation_name, 
                             grade, 
                             department, 
                             location, 
                             et1.employee_mid, 
                             et1.email_id, 
                             employee_name, 
                             competency, 
                             business_unit, 
                             MAX(et1.supercoach_mid) AS supercoach_mid,
                             MAX(et1.supercoach_name) AS supercoach_name,
                             MAX(et1.csc_mid) AS csc_mid,
                             MAX(et1.csc_name) AS csc_name,
                             SUM(capacity * hourly_rate)  capacity_cost,
                             SUM(COALESCE(actual_log_hours,0))  actual_log_hours,
                             SUM(COALESCE(actual_cost,0))  actual_cost,
                             SUM(COALESCE(allocated_cost,0))  allocated_cost,
                             SUM(COALESCE(capacity,0))  capacity,
                             SUM(COALESCE(allocation_hours,0))  allocation_hours, 
                             SUM(COALESCE(allocated_chargable_hr,0))  allocated_chargable_hr, 
                             SUM(COALESCE(allocated_chargable_cost,0))  allocated_chargable_cost, 
                             SUM(COALESCE(allocated_non_chargable_hr,0))  allocated_non_chargable_hr, 
                             SUM(COALESCE(allocated_non_chargable_cost,0))  allocated_non_chargable_cost,
                             STRING_AGG(DISTINCT es.skills, ', ') AS skills,
                             CASE
                                 WHEN SUM(COALESCE(capacity,0)) > 0 
                                     THEN ROUND(SUM(COALESCE(allocation_hours,0)) * 100.0 / SUM(COALESCE(capacity,0)), 2)
                                 ELSE 0
                             END AS allocation_percent,
                             CASE
                                 WHEN SUM(COALESCE(capacity,0)) > 0 
                                     THEN ROUND(SUM(COALESCE(allocated_chargable_hr,0)) * 100.0 / SUM(COALESCE(capacity,0)), 2)
                                 ELSE 0
                             END AS allocation_chargability_percent,
                             CASE
                                 WHEN SUM(COALESCE(capacity,0)) > 0 
                                     THEN ROUND(SUM(COALESCE(job_chargeable_hours,0)) * 100.0 / SUM(COALESCE(capacity,0)), 2)
                                 ELSE 0
                             END AS actual_chargability_percent,
                             SUM(COALESCE(leave_hrs,0))  leave_hrs,
                             SUM(COALESCE(net_leaves,0))  net_leaves,
                             SUM(COALESCE(job_chargeable_hours,0))  job_chargeable_hours,
                             SUM(COALESCE(job_non_chargeable_hours,0))  job_non_chargeable_hours,
                             SUM(COALESCE(job_chargeable_cost,0))  job_chargeable_cost, 
                             SUM(COALESCE(job_non_chargeable_cost,0))  job_non_chargeable_cost, 
                             SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability, 
                             SUM((COALESCE(capacity,0) - COALESCE(allocation_hours,0)) * hourly_rate) as availability_cost  
                         from (
                             select 
                                 et.employee_mid,
                                 et.employee_code,
                                 et.employee_name,
                                 et.email_id,
                                 et.department,
                                 et.location,
                                 et.working_date,
                                 et.designation_name,
                                 et.grade,
                                 et.business_unit,
                                 et.competency,
                                 et.hourly_rate,
                                 et.allocation_date,
                                 -- allocation_hours capped by capacity
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(allocation_hours,0)) 
                                         then max(capacity)
                                     else SUM(COALESCE(allocation_hours,0))
                                 end as allocation_hours,
                                 -- allocated_cost capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(allocation_hours,0)) 
                                         then max(capacity) * hourly_rate
                                     else SUM(COALESCE(allocation_hours,0)) * hourly_rate
                                 end as allocated_cost,
                                 -- allocated_chargable_hr capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(allocated_chargable_hr,0)) 
                                         then max(capacity)
                                     else SUM(COALESCE(allocated_chargable_hr,0))
                                 end as allocated_chargable_hr,
                                 -- allocated_chargable_cost capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(allocated_chargable_hr,0)) 
                                         then max(capacity) * hourly_rate
                                     else SUM(COALESCE(allocated_chargable_hr,0)) * hourly_rate
                                 end as allocated_chargable_cost,
                                 -- allocated_non_chargable_hr capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr,0)) 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) >= 0 
                                         then (max(capacity) - SUM(allocated_chargable_hr))
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr,0)) 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < 0 
                                         then 0
                                     else SUM(COALESCE(allocated_non_chargable_hr,0))
                                 end as allocated_non_chargable_hr,
                                 -- allocated_non_chargable_cost capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr,0)) 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) >= 0 
                                         then (max(capacity) - SUM(allocated_chargable_hr)) * hourly_rate
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr,0)) 
                                          and (max(capacity) - SUM(allocated_chargable_hr)) < 0 
                                         then 0
                                     else SUM(COALESCE(allocated_non_chargable_hr,0)) * hourly_rate
                                 end as allocated_non_chargable_cost,
                                 SUM(actual_log_hours) as actual_log_hours,
                                 -- job_chargeable_hours capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(job_chargeable_hours,0)) 
                                         then max(capacity)
                                     else SUM(COALESCE(job_chargeable_hours,0))
                                 end as job_chargeable_hours,
                                 -- job_chargeable_cost capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 and max(capacity) < SUM(COALESCE(job_chargeable_hours,0)) 
                                         then max(capacity) * hourly_rate
                                     else SUM(COALESCE(job_chargeable_hours,0)) * hourly_rate
                                 end as job_chargeable_cost,
                                 -- job_non_chargeable_hours capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours,0)) 
                                          and (max(capacity) - SUM(job_chargeable_hours)) >= 0 
                                         then (max(capacity) - SUM(job_chargeable_hours))
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours,0)) 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < 0 
                                         then 0
                                     else SUM(COALESCE(job_non_chargeable_hours,0))
                                 end as job_non_chargeable_hours,
                                 -- job_non_chargeable_cost capped
                                 case
                                     when max(capacity) = 0 then 0
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours,0)) 
                                          and (max(capacity) - SUM(job_chargeable_hours)) >= 0 
                                         then (max(capacity) - SUM(job_chargeable_hours)) * hourly_rate
                                     when max(capacity) > 0 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours,0)) 
                                          and (max(capacity) - SUM(job_chargeable_hours)) < 0 
                                         then 0
                                     else SUM(COALESCE(job_non_chargeable_hours,0)) * hourly_rate
                                 end as job_non_chargeable_cost,
                                 SUM(actual_cost) as actual_cost,
                                 et.holiday_date,
                                 et.location_name,
                                 et.holiday_type,
                                 et.emp_mid,
                                 et.leave_start_date,
                                 et.leave_end_date,
                                 et.capacity,
                                 et.leave_hrs,
                                 et.net_leaves,
                                 ev.supercoach_mid,
                                 ev.supercoach_name,
                                 ev.csc_mid,
                                 ev.csc_name
                             from employee_allocation_timesheet et
                             LEFT JOIN employee_view ev ON trim(lower(et.employee_mid)) = trim(lower(ev.employee_mid))
                             {whereClause}
                             group by 
                                 et.employee_mid,
                                 et.employee_code,
                                 et.employee_name,
                                 et.email_id,
                                 et.department,
                                 et.location,
                                 et.working_date,
                                 et.designation_name,
                                 et.grade,
                                 et.business_unit,
                                 et.competency,
                                 et.hourly_rate,
                                 et.allocation_date,
                                 et.competency_id,
                                 et.competency_name,
                                 et.emp_mid,
                                 et.holiday_date,
                                 et.location_name,
                                 et.holiday_type,
                                 et.leave_start_date,
                                 et.leave_end_date,
                                 et.capacity,
                                 et.leave_hrs,
                                 et.net_leaves,
                                 ev.supercoach_mid,
                                 ev.supercoach_name,
                                 ev.csc_mid,
                                 ev.csc_name
                         ) et1 LEFT JOIN employee_skill es 
                         ON es.employee_mid = et1.employee_mid
                         group by 
                            et1.business_unit, 
                            et1.competency, 
                            et1.employee_mid, 
                            et1.email_id, 
                            et1.employee_name,
                            et1.location, 
                            et1.department, 
                            et1.designation_name, 
                            et1.grade
                         order by et1.employee_mid asc
                         ";

                        command.CommandText = sql;

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            string tableString = JsonConvert.SerializeObject(table);
            List<CapacityUtilizationOverviewResponseInfra> result = JsonConvert.DeserializeObject<List<CapacityUtilizationOverviewResponseInfra>>(tableString);
            var resultWithSkills = result.Select(x => x.skills != null);
            return result;
            //return null;
        }

        public async Task<List<ScheduledVsActualVarianceChartResponseInfra>> GetScheduledVsActualVarianceChart02(ScheduledVsActualVarianceChartRequestInfra args)
        {
            string connectionString = ConnStr_ReportDB1; // Your connection string
            DataTable table = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    // Build the SQL command to call the stored function
                    string sql = @"
                SELECT * FROM sp_fetch_capacity_utilization01(
                    @start_date_param,
                    @end_date_param,
                    @business_units_param,
                    @competencies_param,
                    @locations_param,
                    @designations_param
                );
            ";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@start_date_param", new DateOnly(args.StartDate.Year, args.StartDate.Month, args.StartDate.Day));
                        command.Parameters.AddWithValue("@end_date_param", new DateOnly(args.EndDate.Year, args.EndDate.Month, args.EndDate.Day));

                        // Convert lists to comma-separated strings for the function parameters
                        command.Parameters.AddWithValue("@business_units_param", args.BusinessUnit != null ? string.Join(",,", args.BusinessUnit) : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@competencies_param", args.Competency != null ? string.Join(",,", args.Competency) : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@locations_param", args.Location != null ? string.Join(",,", args.Location) : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@designations_param", args.Designation != null ? string.Join(",,", args.Designation) : (object)DBNull.Value);

                        // Execute the command and fill the DataTable
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        DateTime dt = DateTime.Now;
                        adapter.Fill(table);
                        DateTime dt2 = DateTime.Now;
                        //    int sec = Convert.ToInt16(dt2 - dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    throw;
                }
                finally
                {
                    if (connection != null)
                        await connection.CloseAsync();
                }
            }

            // Convert the DataTable to a list of objects
            string tableString = JsonConvert.SerializeObject(table);
            List<ScheduledVsActualVarianceChartResponseInfra> result = JsonConvert.DeserializeObject<List<ScheduledVsActualVarianceChartResponseInfra>>(tableString);

            return result;
        }

        public async Task<List<ScheduledVsActualVarianceChartResponseInfra>> GetScheduledVsActualVarianceChart(ScheduledVsActualVarianceChartRequestInfra args)
        {

            //string sql = $@"select to_char(working_date, 'YYYY MM') date, competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable,0))  job_chargeable, SUM(COALESCE(job_non_chargeable,0))  job_non_chargeable, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability from employee_allocation_timesheet where working_date between @start_date ::Date and @end_date ::Date group by business_unit, competency, date";
            //var result =  _dbContext.Database.SqlQueryRaw<Object>($"select competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable,0))  job_chargeable, SUM(COALESCE(job_non_chargeable,0))  job_non_chargeable, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability from employee_allocation_timesheet group by business_unit, competency");
            string connectionString = ConnStr_ReportDB1;// @$"Server=localhost;Database={DB_REPORT};UserId=postgres;Password={PASSWORD_DB};";
            DataTable table = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(connectionString))
                    {
                        command.Connection = connection;

                        string whereClause = " WHERE ";
                        if (args.BusinessUnit != null && args.BusinessUnit.Count > 0)
                        {
                            SqlServerInClauseParam<string> BusinessUnit = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.BusinessUnit.Select(a => a.ToLower().Trim()).ToList(), "business_unit");
                            whereClause += " TRIM(LOWER(business_unit)) IN ( " + BusinessUnit.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(BusinessUnit.Params());
                        }

                        if (args.Competency != null && args.Competency.Count > 0)
                        {
                            SqlServerInClauseParam<string> Competency = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Competency.Select(a => a.ToLower().Trim()).ToList(), "competency");
                            whereClause += " TRIM(LOWER(competency)) IN ( " + Competency.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Competency.Params());
                        }

                        if (args.Location != null && args.Location.Count > 0)
                        {
                            SqlServerInClauseParam<string> Location = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Location.Select(a => a.ToLower().Trim()).ToList(), "location");
                            whereClause += " TRIM(LOWER(location)) IN ( " + Location.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Location.Params());
                        }

                        if (args.Designation != null && args.Designation.Count > 0)
                        {
                            SqlServerInClauseParam<string> Designation = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Designation.Select(a => a.ToLower().Trim()).ToList(), "designation_name");
                            whereClause += " TRIM(LOWER(designation_name)) IN ( " + Designation.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Designation.Params());
                        }

                        bool checkEmployeeData = false;

                        if (args.Offering != null && args.Offering.Count > 0)
                        {
                            checkEmployeeData = true;
                        }

                        if (args.Solution != null && args.Solution.Count > 0)
                        {
                            checkEmployeeData = true;
                        }

                        if (args.CheckEmpMids && checkEmployeeData && string.IsNullOrEmpty(args.EmailId))
                        {
                            List<string> distinctEmpIds = args.EmpMids != null ?
                                args.EmpMids.Distinct().ToList<string>() : new List<string>();

                            if (distinctEmpIds != null && distinctEmpIds.Count > 0)
                            {
                                SqlServerInClauseParam<string> empids = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, distinctEmpIds.Select(a => a.ToLower().Trim()).ToList(), "empids");
                                whereClause += " TRIM(LOWER(et.employee_code)) IN ( " + empids.ParamsString() + " ) AND ";
                                command.Parameters.AddRange(empids.Params());
                            }
                            //else
                            //{
                            //    //Universal false condition to show no records
                            //    whereClause += " 1=2 AND ";
                            //}
                        }

                        if (!string.IsNullOrEmpty(args.EmailId))
                        {
                            whereClause += " ( TRIM(LOWER(et.email_id)) = @emailId or TRIM(LOWER(et.email_id)) = @splitEmailId ) AND ";
                            string splitEmailId = GetEmailIdPartByIndex(args.EmailId.ToLower().Trim(), emailPartIndexEmail);
                            command.Parameters.AddWithValue("@emailId", args.EmailId.ToLower().Trim());
                            command.Parameters.AddWithValue("@splitEmailId", splitEmailId);

                        }

                        whereClause += " et.working_date between @start_date ::Date and @end_date ::Date ";

                        command.Parameters.AddWithValue("@start_date", args.StartDate);
                        command.Parameters.AddWithValue("@end_date", args.EndDate);

                      

                        string sql = $@"
WITH daily_metrics AS (
    SELECT 
        et.employee_mid,
        MAX(et.employee_name) AS employee_name,
        MAX(et.email_id) AS email_id,
        MAX(et.department) AS department,
        MAX(et.location) AS location,
        TO_CHAR(et.working_date, 'YYYY MM') AS month_year,
        MAX(et.designation_name) AS designation_name,
        MAX(et.grade) AS grade,
        MAX(et.business_unit) AS business_unit,
        MAX(et.competency) AS competency,
        MAX(et.hourly_rate) AS hourly_rate,
        MAX(ev.supercoach_mid) AS supercoach_mid,
        MAX(ev.supercoach_name) AS supercoach_name,
        MAX(ev.csc_mid) AS csc_mid,
        MAX(ev.csc_name) AS csc_name,
        SUM(et.capacity) AS capacity,
        SUM(COALESCE(et.actual_log_hours, 0)) AS actual_log_hours,
        SUM(COALESCE(et.allocation_hours, 0)) AS allocation_hours,
        SUM(COALESCE(et.allocated_chargable_hr, 0)) AS allocated_chargable_hr,
        SUM(COALESCE(et.allocated_non_chargable_hr, 0)) AS allocated_non_chargable_hr,
        SUM(COALESCE(et.job_chargeable_hours, 0)) AS job_chargeable_hours,
        SUM(COALESCE(et.job_non_chargeable_hours, 0)) AS job_non_chargeable_hours,
        SUM(COALESCE(et.leave_hrs, 0)) AS leave_hrs,
        SUM(COALESCE(et.net_leaves, 0)) AS net_leaves,
        SUM(LEAST(et.capacity, COALESCE(et.allocation_hours, 0))) AS capped_allocation_hours,
        SUM(LEAST(et.capacity, COALESCE(et.allocated_chargable_hr, 0))) AS capped_chargable_hr,
        SUM(LEAST(et.capacity, COALESCE(et.job_chargeable_hours, 0))) AS capped_job_chargeable_hours
    FROM employee_allocation_timesheet et
    LEFT JOIN employee_view ev ON et.employee_mid = ev.employee_mid
     {whereClause}
    GROUP BY 
        et.employee_mid,
        TO_CHAR(et.working_date, 'YYYY MM')
)
SELECT 
    month_year as date,
    designation_name,
    grade,
    department,
    location,
    employee_mid,
    email_id,
    employee_name,
    supercoach_mid,
    supercoach_name,
    csc_mid,
    csc_name,
    competency,
    business_unit,
    actual_log_hours,
    capacity * hourly_rate AS capacity_cost,
    capped_allocation_hours * hourly_rate AS allocated_cost,
    allocation_hours,
    capped_chargable_hr AS allocated_chargable_hr,
    capped_chargable_hr * hourly_rate AS allocated_chargable_cost,
    allocated_non_chargable_hr,
    allocated_non_chargable_hr * hourly_rate AS allocated_non_chargable_cost,
    CASE 
        WHEN capacity > 0 THEN ROUND(allocation_hours * 100.0 / capacity, 2)
        ELSE 0
    END AS allocation_percent,
    CASE 
        WHEN capacity > 0 THEN ROUND(capped_chargable_hr * 100.0 / capacity, 2)
        ELSE 0
    END AS allocation_chargability_percent,
    CASE 
        WHEN capacity > 0 THEN ROUND(capped_job_chargeable_hours * 100.0 / capacity, 2)
        ELSE 0
    END AS actual_chargability_percent,
    leave_hrs,
    net_leaves,
    capped_job_chargeable_hours AS job_chargeable_hours,
    capacity,
    job_non_chargeable_hours,
    actual_log_hours * hourly_rate AS job_chargeable_cost,
    job_non_chargeable_hours * hourly_rate AS job_non_chargeable_cost,
    capacity - allocation_hours AS availability
FROM daily_metrics
ORDER BY employee_mid ASC, month_year DESC;
";

                        command.CommandText = sql;

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            string tableString = JsonConvert.SerializeObject(table);
            List<ScheduledVsActualVarianceChartResponseInfra> result = JsonConvert.DeserializeObject<List<ScheduledVsActualVarianceChartResponseInfra>>(tableString);
            //return result;
            return result;
        }

      

        public async Task<List<SummaryStatisticsChartResponseInfra>> GetSummaryStatisticsChart(SummaryStatisticsChartRequestInfra args)
        {
            //string sql = $@"select to_char(working_date, 'YYYY MM') date, competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable,0))  job_chargeable, SUM(COALESCE(job_non_chargeable,0))  job_non_chargeable, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability from employee_allocation_timesheet where working_date between @start_date ::Date and @end_date ::Date group by business_unit, competency, date";
            //var result =  _dbContext.Database.SqlQueryRaw<Object>($"select competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable,0))  job_chargeable, SUM(COALESCE(job_non_chargeable,0))  job_non_chargeable, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability from employee_allocation_timesheet group by business_unit, competency");
            string connectionString = ConnStr_ReportDB1;//@$"Server=localhost;Database={DB_REPORT};UserId=postgres;Password={PASSWORD_DB};";
            DataTable table = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(connectionString))
                    {
                        string whereClause = " WHERE ";
                        if (args.BusinessUnit != null && args.BusinessUnit.Count > 0)
                        {
                            SqlServerInClauseParam<string> BusinessUnit = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.BusinessUnit, "email");
                            whereClause += " business_unit IN ( " + BusinessUnit.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(BusinessUnit.Params());

                        }
                        if (args.Competency != null && args.Competency.Count > 0)
                        {
                            SqlServerInClauseParam<string> Competency = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Competency, "competency");
                            whereClause += " competency IN ( " + Competency.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Competency.Params());

                        }
                        if (args.Location != null && args.Location.Count > 0)
                        {
                            SqlServerInClauseParam<string> Location = new SqlServerInClauseParam<string>(NpgsqlDbType.Text, args.Location, "location");
                            whereClause += " location IN ( " + Location.ParamsString() + " ) AND ";
                            command.Parameters.AddRange(Location.Params());

                        }
                        if (!string.IsNullOrEmpty(args.EmailId))
                        {
                            whereClause += " ( TRIM(LOWER(email_id)) = @emailId or TRIM(LOWER(email_id)) = @splitEmailId ) AND ";
                            string splitEmailId = GetEmailIdPartByIndex(args.EmailId.ToLower().Trim(), emailPartIndexEmail);
                            command.Parameters.AddWithValue("@emailId", args.EmailId.ToLower().Trim());
                            command.Parameters.AddWithValue("@splitEmailId", splitEmailId);

                        }
                        whereClause += " working_date between @start_date ::Date and @end_date ::Date ";

                        command.Connection = connection;

                        command.Parameters.AddWithValue("@start_date", args.StartDate);
                        command.Parameters.AddWithValue("@end_date", args.EndDate);

                        //string sql = $@"select to_char(working_date, 'YYYY MM') date, designation_name, department, location, email_id, employee_name,leave_hrs,competency, business_unit, SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(capacity * hourly_rate)  capacity_cost, SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(job_chargeable_hours,0))  job_chargeable_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(job_non_chargeable_hours,0))  job_non_chargeable_hours,SUM(COALESCE(job_chargeable_cost,0))  job_chargeable_cost, SUM(COALESCE(job_non_chargeable_cost,0))  job_non_chargeable_cost, SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability, pipeline_code as pipeline_code, job_code as job_code from employee_allocation_timesheet {whereClause} group by business_unit, competency, email_id ,employee_name, location , department , designation_name, date, pipeline_code, job_code,leave_hrs";
                        string sql = $@"
                                        select to_char(working_date, 'YYYY MM') date, designation_name, grade, department, location, email_id, employee_name,competency, business_unit, 
SUM(COALESCE(actual_log_hours,0))  actual_log_hours,SUM(capacity * hourly_rate)  capacity_cost, 
SUM(COALESCE(allocation_hours,0))  allocation_hours, SUM(COALESCE(allocated_chargable_hr,0))  allocated_chargable_hr, 
SUM(COALESCE(allocated_chargable_cost,0))  allocated_chargable_cost, SUM(COALESCE(allocated_non_chargable_hr,0))  allocated_non_chargable_hr, 
SUM(COALESCE(allocated_non_chargable_cost,0))  allocated_non_chargable_cost, 
SUM(COALESCE(job_chargeable_hours,0))  job_chargeable_hours,SUM(COALESCE(capacity,0))  capacity, SUM(COALESCE(job_non_chargeable_hours,0))  job_non_chargeable_hours,
SUM(COALESCE(job_chargeable_cost,0))  job_chargeable_cost, SUM(COALESCE(job_non_chargeable_cost,0))  job_non_chargeable_cost, 
SUM(COALESCE(capacity,0) - COALESCE(allocation_hours,0)) as availability, max(pipeline_code_count) as pipeline_code_count, max(job_code_count) as job_code_count 
from 
 (select employee_mid,
     employee_code,
     employee_name,
     email_id,
     department,
     location,
     working_date,
     designation_name,
     grade,
     business_unit,
     competency,
     hourly_rate,
     allocation_date ,
		
		--sum(allocation_hours) as allocation_hours
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(allocation_hours, 0)) then max(capacity)
         else SUM(COALESCE(allocation_hours, 0))
     end as allocation_hours ,
		
		--sum(allocated_cost) as allocated_cost
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(allocation_hours, 0)) then max(capacity) * hourly_rate
         else SUM(COALESCE(allocation_hours, 0)) * hourly_rate
     end as allocated_cost,
		
		--sum(allocated_chargable_hr) as allocated_chargable_hr
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(allocated_chargable_hr, 0)) then max(capacity)
         else SUM(COALESCE(allocated_chargable_hr, 0))
     end as allocated_chargable_hr,
		
		--sum(allocated_chargable_cost) as allocated_chargable_cost
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(allocated_chargable_hr, 0)) then max(capacity) * hourly_rate
         else SUM(COALESCE(allocated_chargable_hr, 0)) * hourly_rate
     end as allocated_chargable_cost,
		
		--sum(allocated_non_chargable_hr) as allocated_non_chargable_hr
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
         and (max(capacity) - sum(allocated_chargable_hr)) < 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
         and (max(capacity) - sum(allocated_chargable_hr)) >= 0 then (max(capacity) - sum(allocated_chargable_hr))
         else SUM(COALESCE(allocated_non_chargable_hr, 0))
     end as allocated_non_chargable_hr,
		
		--sum(allocated_non_chargable_cost) as allocated_non_chargable_cost
     case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
         and (max(capacity) - sum(allocated_chargable_hr)) < 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(allocated_chargable_hr)) < SUM(COALESCE(allocated_non_chargable_hr, 0))
         and (max(capacity) - sum(allocated_chargable_hr)) >= 0 then (max(capacity) - sum(allocated_chargable_hr)) * hourly_rate
         else SUM(COALESCE(allocated_non_chargable_hr, 0)) * hourly_rate
     end as allocated_non_chargable_cost,
		
     sum(actual_log_hours) as actual_log_hours,
		
     -- sum(job_chargeable_hours) as job_chargeable_hours,
		case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(job_chargeable_hours, 0)) then max(capacity)
         else SUM(COALESCE(job_chargeable_hours, 0))
     end as job_chargeable_hours,
		
     -- sum(job_chargeable_cost) as job_chargeable_cost,
		case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and max(capacity) < SUM(COALESCE(job_chargeable_hours, 0)) then max(capacity) * hourly_rate
         else SUM(COALESCE(job_chargeable_hours, 0)) * hourly_rate
     end as job_chargeable_cost,
		
     -- sum(job_non_chargeable_cost) as job_non_chargeable_cost,
		case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
         and (max(capacity) - sum(job_chargeable_hours)) < 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
         and (max(capacity) - sum(job_chargeable_hours)) >= 0 then (max(capacity) - sum(job_chargeable_hours)) * hourly_rate
         else SUM(COALESCE(job_non_chargeable_hours, 0)) * hourly_rate
     end as job_non_chargeable_cost,
		
     -- sum(job_non_chargeable_hours) as job_non_chargeable_hours,
		case
         WHEN max(capacity) = 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
         and (max(capacity) - sum(job_chargeable_hours)) < 0 then 0
         when max(capacity) > 0
         and (max(capacity) - sum(job_chargeable_hours)) < SUM(COALESCE(job_non_chargeable_hours, 0))
         and (max(capacity) - sum(job_chargeable_hours)) >= 0 then (max(capacity) - sum(job_chargeable_hours))
         else SUM(COALESCE(job_non_chargeable_hours, 0))
     end as job_non_chargeable_hours,
		
     sum(actual_cost) as actual_cost,
     holiday_date,
     location_name,
     holiday_type,
     emp_mid,
     leave_start_date,
     leave_end_date,
     capacity,
     leave_hrs,
     net_leaves
	 ,count(pipeline_code) as pipeline_code_count
	 ,count(job_code) as job_code_count
 from employee_allocation_timesheet
{whereClause}
 group by employee_mid,
     employee_code,
     employee_name,
     email_id,
     department,
     location,
     working_date,
     designation_name,
     grade,
     business_unit,
     competency,
     hourly_rate,
     allocation_date,
     competency_id,
     competency_name,
     emp_mid,
     holiday_date,
     location_name,
     holiday_type,
     emp_mid,
     leave_start_date,
     leave_end_date,
     capacity,
     leave_hrs,
     net_leaves
) et1
group by business_unit, competency, email_id ,employee_name, location , 
department , designation_name, grade, date
order by ""date"" desc
                                    ";
                        command.CommandText = sql;

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        adapter.Fill(table);
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            string tableString = JsonConvert.SerializeObject(table);
            List<SummaryStatisticsChartResponseInfra> result = JsonConvert.DeserializeObject<List<SummaryStatisticsChartResponseInfra>>(tableString);

            return result;
        }


     

        private string GetEmailIdPartByIndex(string input, int index)
        {
            string output = input;

            if (!string.IsNullOrEmpty(input) && input.Contains(emailSeperator))
            {
                var ar = input.Split(emailSeperator);
                if (ar!.Length > index)
                {
                    output = ar[index];
                }
            }

            return output.ToLower().Trim();
        }

    }
}