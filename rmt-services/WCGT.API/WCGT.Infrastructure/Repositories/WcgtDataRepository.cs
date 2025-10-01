using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using Npgsql.Internal;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using WCGT.Domain.DTO;
using WCGT.Domain.DTOs;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.Data;

namespace WCGT.Infrastructure.Repositories
{
    public class WcgtDataRepository : IWcgtDataRepository
    {
        private readonly WcgtDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public WcgtDataRepository(WcgtDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<List<string>> GetProjectBudgetByModifiedDateRange(DateTime startDate, DateTime endDate)
        {
            var _records = _dbContext.Budget
                .Where(x => x.IsActive == true
                && !string.IsNullOrEmpty(x.JobCode)
                && ((x.ModifiedAt.HasValue && (x.ModifiedAt.Value.Date >= startDate.Date && x.ModifiedAt.Value.Date <= endDate.Date))
                || (x.CreatedAt.HasValue && (x.CreatedAt.Value.Date >= startDate.Date && x.CreatedAt.Value.Date <= endDate.Date)))
                )
                .Select(a => a.JobCode).Distinct().ToList();

            return _records;

        }

        #region Update Methods

        public async Task<Client> UpdateClient(Client input)
        {
            var _record = _dbContext.Clients.Where(x => x.client_id.ToLower() == input.client_id.ToLower()).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Client>().AddAsync(input);
            }
            else
            {
                _record.client_group_code = input.client_group_code;
                _record.client_group_name = input.client_group_name;
                _record.job_client = input.job_client;
                _record.legal_entity = input.legal_entity;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;

                _dbContext.Clients.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Budget> UpdateBudget(Budget input)
        {

            #region Validate input dto

            var designationDbData = await _dbContext.Designations
                .Where(l => l.grade != null && input.Grade != null && l.grade.ToLower() == input.Grade.ToLower())
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(input.Grade) || designationDbData == null)
            {
                throw new Exception("Invalid grade");
            }

            //For Non Chargable Job Pipeline code is empty so using JobCode as PipelineCode
            if (string.IsNullOrEmpty(input.PipelineCode) && !string.IsNullOrEmpty(input.JobCode))
            {
                input.PipelineCode = input.JobCode;
            }
            else if (string.IsNullOrEmpty(input.PipelineCode) && string.IsNullOrEmpty(input.JobCode))
            {
                throw new Exception("Invalid PipelineCode and JobCode");
            }

            #endregion

            var _record = _dbContext.Budget
                .Where(x =>
                    x.PipelineCode.ToLower() == input.PipelineCode.ToLower()
                    && x.JobCode != null && input.JobCode != null
                    && x.JobCode.ToLower() == input.JobCode.ToLower()
                    && x.Grade != null
                    && input.Grade != null
                    && x.Grade.ToLower() == input.Grade.ToLower()
                 )
                .FirstOrDefault();

            if (_record == null)
            {
                input.CreatedAt = DateTime.UtcNow;
                input.ModifiedAt = DateTime.UtcNow;
                input.IsActive = true;
                await _dbContext.Set<Budget>().AddAsync(input);
            }
            else
            {
                _record.PipelineCode = input.PipelineCode;
                _record.JobCode = input.JobCode;
                _record.Grade = designationDbData.grade + string.Empty;
                _record.Hour = input.Hour;
                _record.Rate = input.Rate;
                _record.IsActive = true;
                _record.ModifiedAt = DateTime.UtcNow;
                _record.ModifiedBy = input.ModifiedBy;

                _dbContext.Budget.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<RateDesignationMaster> UpdateRateDesignationMaster(RateDesignationMaster input)
        {
            #region Validate input dto

            if (input.grade != null && !_dbContext.Designations.Where(l => !string.IsNullOrEmpty(l.grade) && l.grade.ToLower() == input.grade.ToLower()).Any())
            {
                throw new Exception("Invalid grade");
            }
            if (input.CompetencyId != null && !_dbContext.Competencies.Where(l => l.CompetencyId != null && input.CompetencyId != null && l.CompetencyId.ToLower() == input.CompetencyId.ToLower()).Any())
            {
                throw new Exception("Invalid CompetencyId");
            }

            #endregion

            RateDesignationMaster? _record = _dbContext.RateDesignationMaster
                .Where(x =>
                    x.grade != null
                    && input.grade != null
                    && x.grade.ToLower() == input.grade.ToLower()
                    && x.CompetencyId != null
                    && input.CompetencyId != null
                    && x.CompetencyId.ToLower() == input.CompetencyId.ToLower()
                )
                .FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<RateDesignationMaster>().AddAsync(input);
            }
            else
            {
                _record.grade = input.grade + string.Empty;
                _record.CompetencyId = String.IsNullOrEmpty(input.CompetencyId) ? String.Empty : input.CompetencyId;
                _record.RatePerHour = input.RatePerHour;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;

                _dbContext.RateDesignationMaster.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Pipeline> UpdatePipeline(Pipeline input)
        {
            #region Validate input dto

            if (input.location_id != null && !_dbContext.Locations.Where(l => l.location_id.ToLower() == input.location_id.ToLower()).Any())
            {
                throw new Exception("Invalid location_id");
            }
            if (input.emp_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.emp_mid.ToLower()).Any())
            {
                throw new Exception("Invalid emp_mid");
            }
            if (input.emp_location_id != null && !_dbContext.Locations.Where(l => l.location_id.ToLower() == input.emp_location_id.ToLower()).Any())
            {
                throw new Exception("Invalid emp_location_id");
            }
            if (input.client_service_partner_id != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.client_service_partner_id.ToLower()).Any())
            {
                throw new Exception("Invalid client_service_partner_id");
            }
            if (input.industry_id != null && !_dbContext.SectorIndustrys.Where(l => l.industry_id != null && input.industry_id != null && l.industry_id.ToLower() == input.industry_id.ToLower()).Any())
            {
                throw new Exception("Invalid industry_id");
            }
            if (input.client_group_code != null && !_dbContext.Clients.Where(l => l.client_group_code != null && input.client_group_code != null && l.client_group_code.ToLower() == input.client_group_code.ToLower()).Any())
            {
                throw new Exception("Invalid client_group_code");
            }
            if (input.client_id != null && !_dbContext.Clients.Where(l => l.client_id.ToLower() == input.client_id.ToLower()).Any())
            {
                throw new Exception("Invalid client_id");
            }
            if (input.bu_id != null && !_dbContext.BUTreeMappings.Where(l => l.bu_id != null && input.bu_id != null && l.bu_id.ToLower() == input.bu_id.ToLower()).Any())
            {
                throw new Exception("Invalid bu_id");
            }
            if (input.offering_id != null && !_dbContext.BUTreeMappings.Where(l => l.offering_id != null && input.offering_id != null && l.offering_id.ToLower() == input.offering_id.ToLower()).Any())
            {
                throw new Exception("Invalid offering_id");
            }
            if (input.solution_id != null && !_dbContext.BUTreeMappings.Where(l => l.solution_id != null && input.solution_id != null && l.solution_id.ToLower() == input.solution_id.ToLower()).Any())
            {
                throw new Exception("Invalid solution_id");
            }
            if (input.sub_industry_id != null && !_dbContext.SectorIndustrys.Where(l => l.sub_industry_id != null && input.sub_industry_id != null && l.sub_industry_id.ToLower() == input.sub_industry_id.ToLower()).Any())
            {
                throw new Exception("Invalid sub_industry_id");
            }

            //Validate the roles
            if (input.pipeline_roles != null)
            {
                foreach (var _role in input.pipeline_roles)
                {
                    if (_role.user_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == _role.user_mid.ToLower()).Any())
                    {
                        throw new Exception("Invalid Role user_mid");
                    }
                }
            }

            #endregion

            var _record = _dbContext.Pipelines.Where(x => x.pipeline_id != null && input.pipeline_id != null && x.pipeline_id.ToLower() == input.pipeline_id.ToLower()).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                var _entity1 = _dbContext.Set<Pipeline>().AddAsync(input);
                _dbContext.SaveChanges();
            }
            else
            {
                _record.pipeline_id = input.pipeline_id;
                _record.pipeline_code = input.pipeline_code;
                _record.pipeline_name = input.pipeline_name;
                _record.project_code = input.project_code;
                _record.project_name = input.project_name;
                _record.location_id = input.location_id;
                _record.expected = input.expected;
                _record.win_probablity = input.win_probablity;
                _record.won_reason = input.won_reason;

                _record.job_id = input.job_id;
                _record.emp_mid = input.emp_mid;
                _record.emp_name = input.emp_name;
                _record.emp_location_id = input.emp_location_id;
                _record.emp_location_name = input.emp_location_name;

                _record.creation_date = input.creation_date;
                _record.won_date = input.won_date;
                _record.recurring = input.recurring;
                _record.industry_id = input.industry_id;
                _record.pipeline_status = input.pipeline_status;
                _record.pipeline_description = input.pipeline_description;
                _record.nrccstatus = input.nrccstatus;
                _record.finalproposedfee = input.finalproposedfee;
                _record.finalproposedope = input.finalproposedope;
                _record.won_expected_recovery = input.won_expected_recovery;
                _record.contact_name = input.contact_name;
                _record.client_group_code = input.client_group_code;
                _record.client_id = input.client_id;
                _record.client_service_partner_id = input.client_service_partner_id;

                _record.isactive = input.isactive;

                _record.start_date = input.start_date;
                _record.end_date = input.end_date;
                _record.sub_industry_id = input.sub_industry_id;
                _record.bu_id = input.bu_id;
                _record.offering_id = input.offering_id;
                _record.solution_id = input.solution_id;

                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Pipelines.Update(_record);
            }

            Pipeline? _currententity = _dbContext.Pipelines.Where(x => x.pipeline_id != null && input.pipeline_id != null && x.pipeline_id.ToLower() == input.pipeline_id.ToLower()).FirstOrDefault();

            if (input.pipeline_roles != null && _currententity != null)
            {
                foreach (var _role in input.pipeline_roles)
                {
                    _role.pipeline_id = _currententity.pipeline_id;

                    var pr_record = _dbContext.PipelineRoles.Where(x =>
                                    x.user_mid != null && _role.user_mid != null && x.user_mid.ToLower() == _role.user_mid.ToLower()
                                    && x.user_role != null && _role.user_role != null && x.user_role.ToLower() == _role.user_role.ToLower()
                                    && x.pipeline_id == _role.pipeline_id
                                    ).FirstOrDefault();
                    if (pr_record == null)
                    {
                        _role.createdat = DateTime.UtcNow;
                        _role.modifiedat = DateTime.UtcNow;
                        var pr_entity = await _dbContext.Set<PipelineRole>().AddAsync(_role);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        pr_record.user_empname = _role.user_empname;
                        pr_record.user_emailid = _role.user_emailid;
                        pr_record.user_role = _role.user_role;
                        pr_record.isactive = _role.isactive;
                        pr_record.modifiedat = DateTime.UtcNow;
                        pr_record.modifiedby = _role.modifiedby;
                        _dbContext.PipelineRoles.Update(pr_record);
                    }
                }
            }

            _dbContext.SaveChanges();
            return input;
        }

        public async Task<Job> UpdateJob(Job input)
        {
            #region Validate input dto

            Pipeline? pipelineObject = null;
            if (input.pipeline_id != null)
            {
                pipelineObject = _dbContext.Pipelines.Where(l => l.pipeline_id != null && input.pipeline_id != null && l.pipeline_id.ToLower() == input.pipeline_id.ToLower()).FirstOrDefault();
            }

            if (input.job_id == null)
            {
                throw new Exception("Invalid job_id");
            }
            if (input.pipeline_id != null && input.parent_job_id != null)
            {
                throw new Exception("Invalid data. pipeline_id and parent_job_id both value can not be provided at the same time.");
            }
            if (input.pipeline_id != null && pipelineObject == null)
            {
                throw new Exception("Invalid pipeline_id");
            }
            else if (input.pipeline_id != null && pipelineObject != null && String.IsNullOrEmpty(pipelineObject.pipeline_status))
            {
                throw new Exception("Invalid pipeline_status");
            }
            if (input.parent_job_id != null && !_dbContext.Jobs.Where(l => l.job_id != null && input.parent_job_id != null && l.job_id.ToLower() == input.parent_job_id.ToLower()).Any())
            {
                throw new Exception("Invalid parent_job_id");
            }
            if (input.offering_id != null && !_dbContext.BUTreeMappings.Where(l => l.offering_id != null && input.offering_id != null && l.offering_id.ToLower() == input.offering_id.ToLower()).Any())
            {
                throw new Exception("Invalid offering_id");
            }
            if (input.solution_id != null && !_dbContext.BUTreeMappings.Where(l => l.solution_id != null && input.solution_id != null && l.solution_id.ToLower() == input.solution_id.ToLower()).Any())
            {
                throw new Exception("Invalid solution_id");
            }
            if (input.job_location_id != null && !_dbContext.Locations.Where(l => l.location_id != null && input.job_location_id != null && l.location_id.ToLower() == input.job_location_id.ToLower()).Any())
            {
                throw new Exception("Invalid job_location_id");
            }
            if (input.bu_id != null && !_dbContext.BUTreeMappings.Where(l => l.bu_id != null && input.bu_id != null && l.bu_id.ToLower() == input.bu_id.ToLower()).Any())
            {
                throw new Exception("Invalid bu_id");
            }
            if (input.industry_id != null && !_dbContext.SectorIndustrys.Where(l => l.industry_id != null && input.industry_id != null && l.industry_id.ToLower() == input.industry_id.ToLower()).Any())
            {
                throw new Exception("Invalid industry_id");
            }
            if (input.sub_industry_id != null && !_dbContext.SectorIndustrys.Where(l => l.sub_industry_id != null && input.sub_industry_id != null && l.sub_industry_id.ToLower() == input.sub_industry_id.ToLower()).Any())
            {
                throw new Exception("Invalid sub_industry_id");
            }
            if (input.job_client != null && !_dbContext.Clients.Where(l => l.client_id != null && input.job_client != null && l.client_id.ToLower() == input.job_client.ToLower()).Any())
            {
                throw new Exception("Invalid job_client");
            }

            //Validate the roles
            if (input.job_roles != null)
            {
                foreach (var _role in input.job_roles)
                {
                    if (_role.user_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == _role.user_mid.ToLower()).Any())
                    {
                        throw new Exception("Invalid Role user_mid");
                    }
                }
            }

            //Get Pipeline_id based on parent job code
            if (input.parent_job_id != null)
            {
                var parentRecord = _dbContext.Jobs.Where(a => a.job_id != null && input.parent_job_id != null && a.job_id.ToLower() == input.parent_job_id.ToLower()).Select(x => x.pipeline_id).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(parentRecord))
                {
                    input.pipeline_id = parentRecord;
                }
                else
                {
                    throw new Exception("Invalid parent_job_id");
                }
            }

            if (pipelineObject != null)
            {
                //Inherit the values from pipeline data
                input.industry_id = pipelineObject.industry_id;

                //Inherit the values from pipeline data
                input.sub_industry_id = pipelineObject.sub_industry_id;

                //Inherit the values from pipeline data
                input.pipeline_status = pipelineObject.pipeline_status;
            }
            else if (input.industry_id == null)
            {
                throw new Exception("Invalid industry_id");
            }
            else if (input.sub_industry_id == null)
            {
                throw new Exception("Invalid sub_industry_id");
            }
            else
            {
            }

            #endregion

            var _record = _dbContext.Jobs.Where(x => x.job_id != null && input.job_id != null && x.job_id.ToLower() == input.job_id.ToLower()).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                var _entity1 = _dbContext.Set<Job>().AddAsync(input);
                _dbContext.SaveChanges();
            }
            else
            {
                _record.job_code = input.job_code;
                _record.pipeline_id = input.pipeline_id;
                _record.pipeline_code = input.pipeline_code;
                _record.parent_job_id = input.parent_job_id;
                _record.job_name = input.job_name;
                _record.job_description = input.job_description;
                _record.asst_incharge = input.asst_incharge;
                _record.entity = input.entity;
                _record.job_client = input.job_client;
                _record.market = input.market;
                _record.sub_market = input.sub_market;
                _record.job_location_id = input.job_location_id;
                _record.billing_currency = input.billing_currency;
                _record.is_chargeable = input.is_chargeable;
                _record.remarks = input.remarks;
                _record.start_date = input.start_date;
                _record.end_date = input.end_date;
                _record.isactive = input.isactive;
                _record.closed_job = input.closed_job;
                _record.jobBudgetValue = input.jobBudgetValue;
                _record.agreedJobFee = input.agreedJobFee != null ? input.agreedJobFee : 0;
                _record.industry_id = input.industry_id;
                _record.sub_industry_id = input.sub_industry_id;
                _record.recurring_type = input.recurring_type;
                _record.bu_id = input.bu_id;
                _record.offering_id = input.offering_id;
                _record.solution_id = input.solution_id;
                _record.pipeline_status = input.pipeline_status;
                _record.created_date = input.created_date;
                _record.updated_date = input.updated_date;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Jobs.Update(_record);
            }

            Job? _entity = _dbContext.Jobs.Where(x => x.job_id != null && input.job_id != null && x.job_id.ToLower() == input.job_id.ToLower()).FirstOrDefault();

            if (input.job_roles != null && _entity != null)
            {
                foreach (var _role in input.job_roles)
                {
                    _role.job_id = _entity.job_id;

                    var pr_record = _dbContext.JobRoles.Where(x =>
                                    x.user_mid != null && _role.user_mid != null && x.user_mid.ToLower() == _role.user_mid.ToLower()
                                    && x.user_role != null && _role.user_role != null && x.user_role.ToLower() == _role.user_role.ToLower()
                                    && x.job_id == _role.job_id
                                    ).FirstOrDefault();
                    if (pr_record == null)
                    {
                        _role.createdat = DateTime.UtcNow;
                        _role.modifiedat = DateTime.UtcNow;
                        var pr_entity = await _dbContext.Set<JobRole>().AddAsync(_role);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        pr_record.user_empname = _role.user_empname;
                        pr_record.user_emailid = _role.user_emailid;
                        pr_record.user_role = _role.user_role;
                        pr_record.isactive = _role.isactive;
                        pr_record.modifiedat = DateTime.UtcNow;
                        pr_record.modifiedby = _role.modifiedby;
                        _dbContext.JobRoles.Update(pr_record);
                    }
                }
            }

            _dbContext.SaveChanges();
            return input;
        }

        public async Task<Employee> UpdateEmployee(Employee input)
        {
            #region Validations            
            if (input.designation_id != null && !_dbContext.Designations.Where(l => l.designation_id != null && input.designation_id != null && l.designation_id.ToLower() == input.designation_id.ToLower()).Any())
            {
                throw new Exception("invalid designation_id");
            }
            if (input.location_id != null && !_dbContext.Locations.Where(l => l.location_id != null && input.location_id != null && l.location_id.ToLower() == input.location_id.ToLower()).Any())
            {
                throw new Exception("invalid location_id");
            }

            if (input.business_unit_id != null && !_dbContext.BUTreeMappings.Where(l => l.bu_id != null && input.business_unit_id != null && l.bu_id.ToLower() == input.business_unit_id.ToLower()).Any())
            {
                throw new Exception("invalid business_unit_id");
            }
            if (input.CompetencyId != null && !_dbContext.Competencies.Where(l => l.CompetencyId != null && input.CompetencyId != null && l.CompetencyId.ToLower() == input.CompetencyId.ToLower()).Any())
            {
                throw new Exception("invalid CompetencyId");
            }

            if ((string.IsNullOrEmpty(input.supercoach_mid) && string.IsNullOrEmpty(input.reporting_partner_mid)))
            {
                throw new Exception("invalid supercoach_mid/reporting_partner_mid");
            }

            //if (input.Qualifications != null && input.Qualifications.Any(q =>
            //    string.IsNullOrWhiteSpace(q.qualification) ||
            //    string.IsNullOrWhiteSpace(q.institution_name_location) ||
            //    string.IsNullOrWhiteSpace(q.month_year_of_passing) ||
            //    string.IsNullOrWhiteSpace(q.area_of_specialisation)))
            //{
            //    throw new Exception("Invalid qualification: all fields must be provided.");
            //}

            //if (input.Language != null && input.Language.Any(q =>
            //   string.IsNullOrWhiteSpace(q.language_name) ||
            //   string.IsNullOrWhiteSpace(q.read) ||
            //   string.IsNullOrWhiteSpace(q.write) ||
            //   string.IsNullOrWhiteSpace(q.speak)))
            //{
            //    throw new Exception("Invalid Language: all fields must be provided.");
            //}

            //if (input.PastEmploymentDetails != null && input.PastEmploymentDetails.Any(q =>
            //   string.IsNullOrWhiteSpace(q.name_of_employer) ||
            //   string.IsNullOrWhiteSpace(q.last_designation_held) ||
            //   string.IsNullOrWhiteSpace(q.from) ||
            //   string.IsNullOrWhiteSpace(q.to)))
            //{
            //    throw new Exception("Invalid PastEmploymentDetails: all fields must be provided.");
            //}
            #endregion

            var _record = _dbContext.Employees
                            .Include(e => e.Qualifications)
                            .Include(e => e.PastEmploymentDetails)
                            .Include(e => e.Language)
                            .FirstOrDefault(x => x.employee_mid.ToLower() == input.employee_mid.ToLower());

            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Employee>().AddAsync(input);
            }
            else
            {                
                _record.company_name = input.company_name;
                _record.employee_code = input.employee_code;
                _record.first_name = input.first_name;
                _record.middle_name = input.middle_name;
                _record.last_name = input.last_name;
                _record.name = input.name;
                _record.designation_id = input.designation_id;
                _record.department = input.department;
                _record.location_id = input.location_id;
                _record.email_id = input.email_id;
                _record.joining_date = input.joining_date;
                _record.reporting_partner_mid = input.reporting_partner_mid;
                _record.group_head_mid = input.group_head_mid;
                _record.business_unit_id = input.business_unit_id;
                _record.CompetencyId = input.CompetencyId;
                _record.business_unit_id = input.business_unit_id;
                _record.specical_day = input.specical_day;
                _record.birthday = input.birthday;
                _record.isactive = input.isactive;
                _record.supercoach_mid = input.supercoach_mid;
                _record.resignation_date = input.resignation_date;
                _record.proposed_lwd = input.proposed_lwd;
                _record.employee_status = input.employee_status;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;

                UpdateCollection(_record.Qualifications, input.Qualifications, _dbContext.Qualifications);
                UpdateCollection(_record.PastEmploymentDetails, input.PastEmploymentDetails, _dbContext.PastEmploymentDetails);
                UpdateCollection(_record.Language, input.Language, _dbContext.Language);

                            
            }

            //save birthday and special day as leave for employee
            string leaveDateFormat = "yyyy-MM-dd HH:mm:ss";
            string leaveIdPlaceHolder = "{0}_{1}_{2}";
            if (input.birthday != null)
            {
                string leaveName = "Birthday";
                var leaveRecord = _dbContext.Leaves.Where(x => !string.IsNullOrEmpty(x.emp_mid) && x.emp_mid.ToLower() == input.employee_mid.ToLower()
                && x.leave_start_date == input.birthday && x.leave_end_date == input.birthday && x.leave_type_name == leaveName).OrderByDescending(x => x.createdat).FirstOrDefault();

                if (leaveRecord == null)
                {
                    //Added logic to add birthday and special day as leave 
                    Leave _empBirthdayLeave = new Leave();
                    _empBirthdayLeave.unique_leave_id = string.Format(leaveIdPlaceHolder, input.employee_mid, leaveName, DateTime.UtcNow.ToString(leaveDateFormat));
                    _empBirthdayLeave.leave_id = string.Format(leaveIdPlaceHolder, input.employee_mid, leaveName, DateTime.UtcNow.ToString(leaveDateFormat));
                    _empBirthdayLeave.location_id = input.location_id;
                    _empBirthdayLeave.leave_start_date = input.birthday;
                    _empBirthdayLeave.leave_end_date = input.birthday;
                    _empBirthdayLeave.applied_days = 1;
                    _empBirthdayLeave.leave_type_name = leaveName;
                    _empBirthdayLeave.emp_mid = input.employee_mid;
                    _empBirthdayLeave.emp_name = input.name;
                    _empBirthdayLeave.emp_email = input.email_id;
                    _empBirthdayLeave.isactive = input.isactive;
                    _empBirthdayLeave.createdby = input.createdby;
                    _empBirthdayLeave.modifiedby = input.modifiedby;
                    _empBirthdayLeave.createdat = DateTime.UtcNow;
                    _empBirthdayLeave.modifiedat = DateTime.UtcNow;
                    await _dbContext.Set<Leave>().AddAsync(_empBirthdayLeave);
                }
            }

            if (input.specical_day != null)
            {
                string leaveName = "SpecialDay";
                var leaveRecord = _dbContext.Leaves.Where(x => !string.IsNullOrEmpty(x.emp_mid) && x.emp_mid.ToLower() == input.employee_mid.ToLower()
                && x.leave_start_date == input.specical_day && x.leave_end_date == input.specical_day && x.leave_type_name == leaveName).OrderByDescending(x => x.createdat).FirstOrDefault();

                if (leaveRecord == null)
                {
                    Leave _empSpecialLeave = new Leave();
                    _empSpecialLeave.unique_leave_id = string.Format(leaveIdPlaceHolder, input.employee_mid, leaveName, DateTime.UtcNow.ToString(leaveDateFormat));
                    _empSpecialLeave.leave_id = string.Format(leaveIdPlaceHolder, input.employee_mid, leaveName, DateTime.UtcNow.ToString(leaveDateFormat));
                    _empSpecialLeave.location_id = input.location_id;
                    _empSpecialLeave.leave_start_date = input.specical_day;
                    _empSpecialLeave.leave_end_date = input.specical_day;
                    _empSpecialLeave.applied_days = 1;
                    _empSpecialLeave.leave_type_name = leaveName;
                    _empSpecialLeave.emp_mid = input.employee_mid;
                    _empSpecialLeave.emp_name = input.name;
                    _empSpecialLeave.emp_email = input.email_id;
                    _empSpecialLeave.isactive = input.isactive;
                    _empSpecialLeave.createdat = input.createdat;
                    _empSpecialLeave.modifiedat = input.modifiedat;
                    _empSpecialLeave.createdby = input.createdby;
                    _empSpecialLeave.modifiedby = input.modifiedby;
                    _empSpecialLeave.createdat = DateTime.UtcNow;
                    await _dbContext.Set<Leave>().AddAsync(_empSpecialLeave);
                }
            }

            await _dbContext.SaveChangesAsync();
            return input;
        }

       public static void UpdateCollection<T>(ICollection<T> recordCollection, ICollection<T> inputCollection, DbSet<T> dbSet)
    where T : class
        {
            // Remove old items if they exist
            if (recordCollection.Count > 0)
            {
                dbSet.RemoveRange(recordCollection);
                recordCollection.Clear();
            }

            // Add new items if they exist
            if (inputCollection != null && inputCollection.Count > 0)
            {
                foreach (var item in inputCollection)
                {
                    recordCollection.Add(item);
                }
                dbSet.AddRange(inputCollection);
            }
        }

        public async Task<Competency> UpdateCompetency(Competency input)
        {
            if (input.BuId != null && !_dbContext.BUTreeMappings.Where(l => l.bu_id != null && input.BuId != null && l.bu_id.ToLower().Trim() == input.BuId.ToLower().Trim()).Any())
            {
                throw new Exception("invalid BuId");
            }
            if (input.CompetencyLeaderMID != null && !_dbContext.Employees.Where(l => l.employee_mid != null && input.CompetencyLeaderMID != null && l.employee_mid.ToLower().Trim() == input.CompetencyLeaderMID.ToLower().Trim()).Any())
            {
                throw new Exception("invalid CompetencyLeaderMID");
            }

            var _record = _dbContext.Competencies.Where(x => x.CompetencyId.ToLower().Trim() == input.CompetencyId.ToLower().Trim()).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Competency>().AddAsync(input);
            }
            else
            {
                _record.CompetencyName = input.CompetencyName;
                _record.CompetencyLeaderMID = input.CompetencyLeaderMID;
                _record.BuId = input.BuId;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Competencies.Update(_record);
            }

            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Designation> UpdateDesignation(Designation input)
        {
            var _record = _dbContext.Designations.Where(x => x.designation_id.ToLower() == input.designation_id.ToLower()).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Designation>().AddAsync(input);
            }
            else
            {
                _record.designation_name = input.designation_name;
                _record.grade = input.grade;
                _record.description = input.description;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Designations.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<BUTreeMapping> UpdateBUTreeMapping(BUTreeMapping input)
        {
            if (input.bu_leader_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.bu_leader_mid.ToLower()).Any())
            {
                throw new Exception("invalid bu_leader_mid");
            }
            if (input.offering_leader_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.offering_leader_mid.ToLower()).Any())
            {
                throw new Exception("invalid offering_leader_mid");
            }
            if (input.solution_leader_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.solution_leader_mid.ToLower()).Any())
            {
                throw new Exception("invalid solution_leader_mid");
            }

            var _record = _dbContext.BUTreeMappings.Where(x =>
                            x.bu_id != null && input.bu_id != null && x.bu_id.ToLower() == input.bu_id.ToLower()
                            && x.offering_id != null && input.offering_id != null && x.offering_id.ToLower().Trim() == input.offering_id.ToLower().Trim()
                            && x.solution_id != null && input.solution_id != null && x.solution_id.ToLower().Trim() == input.solution_id.ToLower().Trim()
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<BUTreeMapping>().AddAsync(input);
            }
            else
            {
                _record.bu = input.bu;
                _record.bu_id = input.bu_id;
                _record.offering = input.offering;
                _record.offering_id = input.offering_id;
                _record.solution = input.solution;
                _record.solution_id = input.solution_id;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _record.bu_leader_mid = input.bu_leader_mid;
                _record.offering_leader_mid = input.offering_leader_mid;
                _record.solution_leader_mid = input.solution_leader_mid;
                _dbContext.BUTreeMappings.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<BUEfficiencyLeaderDTO> UpdateBUEfficiencyLeader(BUEfficiencyLeaderDTO input)
        {
            if (input.bu_id != null && !_dbContext.BUTreeMappings.Where(l => l.bu_id != null && l.bu_id.ToLower() == input.bu_id.ToLower()).Any())
            {
                throw new Exception("invalid bu_id");
            }
            if (input.bu_efficiency_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.bu_efficiency_mid.ToLower()).Any())
            {
                throw new Exception("invalid bu_efficiency_mid");
            }

            var _records = _dbContext.BUTreeMappings.Where(x =>
                            x.bu_id != null && input.bu_id != null && x.bu_id.ToLower() == input.bu_id.ToLower()
                            ).ToList();

            if (_records != null)
            {
                foreach (var _record in _records)
                {
                    _record.bu_efficiency_leader_mid = input.bu_efficiency_mid;
                    _record.modifiedat = DateTime.UtcNow;
                    _dbContext.BUTreeMappings.Update(_record);
                }
            }

            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Holiday> UpdateHoliday(Holiday input)
        {
            if (input.location_id != null && !_dbContext.Locations.Where(l => l.location_id.ToLower() == input.location_id.ToLower()).Any())
            {
                throw new Exception("Invalid location_id");   //invalid location id 
            }

            var _record = _dbContext.Holidays.Where(x =>
                            x.holiday_name != null && input.holiday_name != null && x.holiday_name.ToLower() == input.holiday_name.ToLower()
                            && x.location_id != null && input.location_id != null && x.location_id.ToLower() == input.location_id.ToLower()
                            && x.holiday_date == input.holiday_date
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Holiday>().AddAsync(input);
            }
            else
            {
                _record.holiday_name = input.holiday_name;
                _record.holiday_type = input.holiday_type;
                _record.location_id = input.location_id;
                _record.location_name = input.location_name;
                _record.holiday_date = input.holiday_date;
                _record.cr_date = input.cr_date;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Holidays.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Leave> UpdateLeave(Leave input)
        {

            if (input.location_id != null && !_dbContext.Locations.Where(l => l.location_id.ToLower() == input.location_id.ToLower()).Any())
            {
                throw new Exception("Invalid location_id");
            }

            if (input.emp_mid != null && !_dbContext.Employees.Where(l => l.employee_mid.ToLower() == input.emp_mid.ToLower()).Any())
            {
                throw new Exception("Invalid emp_mid");
            }

            var _record = _dbContext.Leaves.Where(x =>
                           x.unique_leave_id != null && input.unique_leave_id != null && x.unique_leave_id.ToLower().Trim() == input.unique_leave_id.ToLower().Trim()
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Leave>().AddAsync(input);
            }
            else
            {
                _record.unique_leave_id = input.unique_leave_id;
                _record.leave_id = input.leave_id;
                _record.location_id = input.location_id;
                _record.location_name = input.location_name;
                _record.leave_start_date = input.leave_start_date;
                _record.leave_end_date = input.leave_end_date;
                //_record.start_date_half = input.start_date_half;
                _record.start_date_half = input.start_date_half;
                _record.end_date_half = input.end_date_half;
                //_record.end_date_half = input.end_date_half;
                _record.applied_days = input.applied_days;
                _record.revoked_days = input.revoked_days;
                _record.revoked_from_date = input.revoked_from_date;
                _record.revoked_to_date = input.revoked_to_date;
                _record.comp_name = input.comp_name;
                _record.leave_type_name = input.leave_type_name;
                _record.emp_mid = input.emp_mid;
                _record.emp_name = input.emp_name;
                _record.emp_email = input.emp_email;
                _record.leave_status_name = input.leave_status_name;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Leaves.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<Location> UpdateLocation(Location input)
        {
            var _record = _dbContext.Locations.Where(x =>
                            x.location_id.ToLower() == input.location_id.ToLower()
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<Location>().AddAsync(input);
            }
            else
            {
                _record.location_mid = input.location_mid;
                _record.location_name = input.location_name;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.Locations.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<ClientLegalEntity> UpdateClientLegalEntity(ClientLegalEntity input)
        {
            var _record = _dbContext.ClientLegalEntitys.Where(x =>
                            x.par_aid.ToLower() == input.par_aid.ToLower()
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<ClientLegalEntity>().AddAsync(input);
            }
            else
            {
                _record.para_desc = input.para_desc;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.ClientLegalEntitys.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<SectorIndustry> UpdateSectorIndustry(SectorIndustry input)
        {
            var _record = _dbContext.SectorIndustrys.Where(x =>
                            x.industry_id != null && input.industry_id != null && x.industry_id.ToLower() == input.industry_id.ToLower()
                            && x.sub_industry_id != null && input.sub_industry_id != null && x.sub_industry_id.ToLower() == input.sub_industry_id.ToLower()
                            ).FirstOrDefault();
            if (_record == null)
            {
                input.createdat = DateTime.UtcNow;
                input.modifiedat = DateTime.UtcNow;
                await _dbContext.Set<SectorIndustry>().AddAsync(input);
            }
            else
            {
                _record.industry_id = input.industry_id;
                _record.industry_name = input.industry_name;
                _record.sub_industry_id = input.sub_industry_id;
                _record.sub_industry_name = input.sub_industry_name;
                _record.isactive = input.isactive;
                _record.modifiedat = DateTime.UtcNow;
                _record.modifiedby = input.modifiedby;
                _dbContext.SectorIndustrys.Update(_record);
            }
            await _dbContext.SaveChangesAsync();
            return input;
        }

        public async Task<WCGTDataLog> AddWCGTDataLogEntry(WCGTDataLog input)
        {
            input.createdat = DateTime.UtcNow;
            input.modifiedat = DateTime.UtcNow;
            await _dbContext.Set<WCGTDataLog>().AddAsync(input);
            _dbContext.SaveChanges();
            return input;
        }

        #endregion


        #region GetAll Methods

        public async Task<List<Client>> GetAllClients()
        {
            return await _dbContext.Clients.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<GetJobCodeClientDTO>> GetAllClientsByJobCode(List<string> jobCodes)
        {
            var result = await _dbContext.Jobs
                .Where(job => jobCodes.Contains(job.job_code)) // filter by job codes
                .Join(_dbContext.Clients,
                    job => job.job_client,                  // join on job_client
                    client => client.client_id,             // with client's client_id
                    (job, client) => new GetJobCodeClientDTO
                    {
                        job_code = job.job_code,
                        client_id = client.client_id,
                        job_client = client.job_client,
                        client_group_code = client.client_group_code,
                        client_group_name = client.client_group_name,
                        legal_entity = client.legal_entity,
                        isactive = client.isactive,
                        createdat = client.createdat,
                        modifiedat = client.modifiedat,
                        createdby = client.createdby,
                        modifiedby = client.modifiedby
                    })
                .Distinct()
                .ToListAsync();

            return result;
        }


        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _dbContext.Employees.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<Competency>> GetCompetencies(string? CompetencyId, string? CompetencyName, string? CompetencyLeaderMID, string? BuId, Boolean? isactive)
        {
            var result = await _dbContext.Competencies.Where(a =>
                (string.IsNullOrEmpty(CompetencyId) == true || string.IsNullOrEmpty(CompetencyId) == false && a.CompetencyId.ToLower().Trim() == CompetencyId.ToLower().Trim())
                && (string.IsNullOrEmpty(CompetencyName) == true || string.IsNullOrEmpty(CompetencyName) == false && a.CompetencyName.ToLower().Trim() == CompetencyName.ToLower().Trim())
                && (string.IsNullOrEmpty(CompetencyLeaderMID) == true || string.IsNullOrEmpty(CompetencyLeaderMID) == false && a.CompetencyLeaderMID.ToLower().Trim() == CompetencyLeaderMID.ToLower().Trim())
                && (string.IsNullOrEmpty(BuId) == true || string.IsNullOrEmpty(BuId) == false && a.BuId.ToLower().Trim() == BuId.ToLower().Trim())
                && (isactive == null || a.isactive == isactive)
            ).ToListAsync();

            return result;
        }

        public async Task<Employee?> GetEmployeeByParam(string? emp_mid, string? emp_emailid)
        {
            if (!string.IsNullOrWhiteSpace(emp_mid))
                return await _dbContext.Employees.Where(a => a.isactive == true
               && a.employee_mid.ToLower() == emp_mid.ToLower()).FirstOrDefaultAsync();

            else if (!string.IsNullOrWhiteSpace(emp_emailid))
                return await _dbContext.Employees.Where(a => a.isactive == true
              && a.email_id != null && a.email_id.ToLower() == emp_emailid.ToLower()).FirstOrDefaultAsync();
            else
                return null;

        }

        public async Task<List<Employee>> GetEmployeeBySuperCoachOrCSC(string emp_mid)
        {
            var lowerEmpMid = emp_mid?.ToLower();
            var employees = await _dbContext.Employees
                .Where(a => a.isactive == true &&
                    (
                        (a.supercoach_mid != null && a.supercoach_mid.ToLower() == lowerEmpMid) ||
                        (a.reporting_partner_mid != null && a.reporting_partner_mid.ToLower() == lowerEmpMid)
                    )
                ).ToListAsync();

            return employees!;


        }

        public async Task<List<Employee>> GetEmployeeBySuperCoachOrCSCList(List<string> emp_mid)
        {
            emp_mid = emp_mid.ConvertAll(mid => mid.ToLower());
            var employees = await _dbContext.Employees
                .Where(a => a.isactive == true &&
                    (
                        (a.supercoach_mid != null && emp_mid.Contains(a.supercoach_mid.ToLower()))
                    )
                ).ToListAsync();

            return employees!;


        }

        public async Task<List<SuperCoach>> GetAllSuperCoach()
        {
            return await _dbContext.Employees
                .Where(e1 => e1.isactive == true && !string.IsNullOrEmpty(e1.supercoach_mid))
                .Join(
                    _dbContext.Employees,
                    e1 => e1.supercoach_mid,
                    e2 => e2.employee_mid,
                    (e1, e2) => new SuperCoach
                    {
                        employee_mid = e2.employee_mid,
                        company_name = e2.company_name,
                        employee_code = e2.employee_code,
                        name = e2.name,
                        designation_id = e2.designation_id,
                        email_id = e2.email_id
                    })
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<SuperCoach>> GetAllCoSuperCoach()
        { 
            return await _dbContext.Employees
               .Where(e1 => e1.isactive == true && !string.IsNullOrEmpty(e1.reporting_partner_mid))
               .Join(
                   _dbContext.Employees,
                   e1 => e1.reporting_partner_mid,
                   e2 => e2.employee_mid,
                   (e1, e2) => new SuperCoach
                   {
                       employee_mid = e2.employee_mid,
                       company_name = e2.company_name,
                       employee_code = e2.employee_code,
                       name = e2.name,
                       designation_id = e2.designation_id,
                       email_id = e2.email_id
                   })
               .Distinct()
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<List<EmployeeLeavesHolidayAndAvailabity>> GetEmployeeAvailabilityWithLeavesAndHolidays(DateTime startDate, DateTime endDate, List<string> grade,
                    List<string> designation, List<string> userEmpMids, List<string> supercoach, List<string> cosupercoach, List<string> clientname, List<string> clientgroupname,
                    List<string> business_unit_ids, List<string> competency_ids
            )
        {
            string result = string.Empty;
            NpgsqlConnection npgsqlConnection = null;
            string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();

            npgsqlConnection = new NpgsqlConnection(pgsqlConnection);

            List<EmployeeLeavesHolidayAndAvailabity> employeeAvailablityInfo = new List<EmployeeLeavesHolidayAndAvailabity>();
            using (NpgsqlCommand command = new NpgsqlCommand("sp_getuserwithleavesholidaysandavailability_3", npgsqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                var pref_weightage_constraintParam = new NpgsqlParameter("start_date", NpgsqlDbType.Date)
                {
                    Value = DateOnly.FromDateTime(startDate),
                };
                command.Parameters.Add(pref_weightage_constraintParam);

                var pref_weightage_constraintParam1 = new NpgsqlParameter("end_date", NpgsqlDbType.Date)
                {
                    Value = DateOnly.FromDateTime(endDate),
                };
                command.Parameters.Add(pref_weightage_constraintParam1);

                var employee_list = new NpgsqlParameter("emp_mids", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = userEmpMids
                };
                command.Parameters.Add(employee_list);

                var grade_list = new NpgsqlParameter("grade_filter", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = grade
                };
                command.Parameters.Add(grade_list);
                var designation_list = new NpgsqlParameter("designation", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = designation
                };
                command.Parameters.Add(designation_list);
                var supercoachMid = new NpgsqlParameter("super_coach_mid", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = supercoach
                };
                command.Parameters.Add(supercoachMid);
                var coSupercoachMid = new NpgsqlParameter("co_super_coach_mid", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = cosupercoach
                };
                command.Parameters.Add(coSupercoachMid);
                var bu_ids = new NpgsqlParameter("bu_ids", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = business_unit_ids
                };
                command.Parameters.Add(bu_ids);
                var comp_ids = new NpgsqlParameter("competency_ids", NpgsqlDbType.Text | NpgsqlDbType.Array)
                {
                    Value = competency_ids
                };
                command.Parameters.Add(comp_ids);


                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                var outputResult = new NpgsqlParameter("var_resp", NpgsqlDbType.Json);
                outputResult.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputResult);

                npgsqlConnection.Open();
                command.ExecuteNonQuery();
                var jsonResult = command.Parameters["var_resp"].Value.ToString();

                if (!string.IsNullOrEmpty(jsonResult))
                {
                    JArray jsonArray = JArray.Parse(jsonResult);
                    employeeAvailablityInfo = JsonConvert.DeserializeObject<List<EmployeeLeavesHolidayAndAvailabity>>(jsonResult);
                    result = jsonResult;
                }

                npgsqlConnection.Close();
            }
            return employeeAvailablityInfo != null && employeeAvailablityInfo.Count > 0 ? employeeAvailablityInfo : new();
        }
       
        public async Task<List<Designation>> GetAllDesignations()
        {
            return await _dbContext.Designations
                .Where(a => a.isactive == true)
                .OrderBy(a => a.designation_name)
                .ToListAsync();
        }

        public async Task<List<BUTreeMapping>> GetAllBUTreeMappings()
        {
            return await _dbContext.BUTreeMappings.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<BUTreeMapping>> GetAllBUTreeMappingsByMID(string? mid)
        {
            return await _dbContext.BUTreeMappings.Where(a => a.isactive == true && ((a.bu_leader_mid != null && a.bu_leader_mid.Equals(mid))
                    || (a.bu_efficiency_leader_mid != null && a.bu_efficiency_leader_mid.Equals(mid))
                    || (a.offering_leader_mid != null && a.offering_leader_mid.Equals(mid))
                    || (a.solution_leader_mid != null && a.solution_leader_mid.Equals(mid))
                    )).ToListAsync();
        }

        public async Task<List<Holiday>> GetAllHolidays()
        {
            return await _dbContext.Holidays.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<Holiday>> GetAllHolidaysByParams(List<HolidayParamsDTO> holidayRequest)
        {
            var potentialHolidays = await _dbContext.Holidays
             .Where(h => h.location_name != null && holidayRequest.Select(param => param.LocationName.ToLower().Trim())
                                       .Contains(h.location_name.ToLower().Trim()) && h.isactive == true)
             .ToListAsync();

            var holidays = potentialHolidays
                .Where(h => h.location_name != null && holidayRequest.Any(param =>
                    h.location_name.ToLower().Trim() == param.LocationName.ToLower().Trim() &&
                    h.holiday_date >= DateOnly.FromDateTime(param.HolidayStartDate)))
                .ToList();
            return holidays;
        }

        public async Task<List<Leave>> GetAllLeaves()
        {
            return await _dbContext.Leaves.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<Leave>> GetAllLeavesByParam(List<string>? emp_emailid, DateTime? created_at)
        {
            List<Leave> leavesResult = new List<Leave>();
            var leaves = await _dbContext.Leaves.Where(a => a.isactive == true).ToListAsync();
            leavesResult = leaves;
            if (emp_emailid != null)
            {
                emp_emailid = emp_emailid.Select(s => s.Replace('"', ' ').ToLower().Trim()).ToList();
                if (emp_emailid.Count > 0)
                {
                    var response = leavesResult.Where(a => a.emp_mid != null && a.emp_email != null && a.isactive == true && emp_emailid.Any(x => x.ToLower() == a.emp_mid.ToLower() + "__" + a.emp_email.ToLower())).ToList();
                    leavesResult = response;
                }
            }
            if (created_at != null)
            {
                var response = leavesResult.Where(a => a.createdat != null && a.isactive == true && created_at.Value.Date.Equals(a.createdat.Value.Date)).ToList();
                leavesResult = response;
            }
            return leavesResult;
        }

        public async Task<List<Leave>> GetAllLeavesByCreateDate(DateTime created_at)
        {
            DateTime createdDate = (DateTime)created_at;
            var response = await _dbContext.Leaves.Where(d => d.createdat != null && (DateTime)d.createdat == createdDate.Date && d.isactive == true).ToListAsync();
            return response;
        }

        public async Task<List<LeaveReport>> GetLeavesInfoByStartDateEndDateAndEmails(List<string> emp_mid, DateTime start_date, DateTime end_date)
        {
            //var emailArray = "{" + string.Join(",", employeeEmails.Select(e => $"\"{e}\"")) + "}";

            var result = await _dbContext.generate_leave_report
                .FromSqlRaw(@"SELECT * FROM generate_leave_report({0} :: DATE, {1} :: DATE, {2})", DateOnly.FromDateTime(start_date), DateOnly.FromDateTime(end_date), emp_mid)
                .ToListAsync();
            return result;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _dbContext.Locations.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<ClientLegalEntity>> GetAllClientLegalEntities()
        {
            return await _dbContext.ClientLegalEntitys.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<SectorIndustry>> GetAllSectorIndustries()
        {
            return await _dbContext.SectorIndustrys.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<Pipeline>> GetAllPipelines()
        {
            return await _dbContext.Pipelines.Include(co => co.pipeline_roles).Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<List<Job>> GetAllJobs()
        {
            return await _dbContext.Jobs.Include(co => co.job_roles).Where(a => a.isactive == true).ToListAsync();
        }
        public async Task<List<Job>> GetJobs()
        {
            return await _dbContext.Jobs.Where(a => a.isactive == true).ToListAsync();
        }

        public async Task<Job?> GetJobByJobCode(string? pipelineCode, string jobCode)
        {
            var response = await _dbContext.Jobs.Where(job => (job.is_chargeable == true && job.job_code != null && job.job_code.ToLower() == jobCode.ToLower() && job.isactive == true) ||
            (pipelineCode != null && job.is_chargeable == false && job.job_code != null && job.job_code.ToLower() == jobCode.ToLower() && job.pipeline_code != null && pipelineCode.ToLower() == job.pipeline_code.ToLower() && job.isactive == true)).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<WCGTTimesheet>> GetAllTimesheet()
        {
            return await _dbContext.Timesheet.ToListAsync();
        }

        public async Task<List<TimesheetDesignationResponse>> GetProjectDesignationTimesheet(string jobCode)
        {
            return await _dbContext.Timesheet
                .Join(_dbContext.Designations
                    , time => time.designation_id
                    , desig => desig.designation_id
                    , (time, desig) =>
                    new TimesheetDesignationResponse
                    {
                        employeename = time.employeename,
                        employeecode = time.employeecode,
                        designation_id = time.designation_id,
                        designation = desig.designation_name + string.Empty,
                        datelog = time.datelog,
                        createdby = time.createdby,
                        createdat = time.createdat,
                        chargeableflag = time.chargeableflag,
                        client = time.client,
                        gradename = time.gradename,
                        jobcode = time.jobcode,
                        id = time.id,
                        modifiedat = time.modifiedat,
                        modifiedby = time.modifiedby,
                        rate = time.rate,
                        status = time.status,
                        totaltime = time.totaltime,
                    })
                .Where(t => t.jobcode.ToLower() == jobCode.ToLower()).ToListAsync();
            //t.datelog >= DateOnly.FromDateTime(startDate) && t.datelog  <= DateOnly.FromDateTime(endDate)   && 
        }

        public List<WcgtTimesheetGroup> GetProjectGroupTimesheet(string jobCode, string timeOption, DateTime startDate, DateTime endDate)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            List<WcgtTimesheetGroup> result = new List<WcgtTimesheetGroup>();
            try
            {
                //string query = @" CALL  " + Constants.TimesheetSP + " ('" + timeOption + "','" + jobCode + "','" + startDate + "','" + endDate + "' ); FETCH ALL FROM \"rs_resultone\"";
                string query = @" CALL  " + Constants.TimesheetSP + " (@timeoptionname,@injobcode,@startdate,@enddate ); FETCH ALL FROM \"rs_resultone\"";

                DataSet ds = new DataSet();
                string? pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);
                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("timeoptionname", NpgsqlDbType.Text, timeOption);
                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, jobCode);
                cmd.Parameters.AddWithValue("startdate", NpgsqlDbType.Date, startDate);
                cmd.Parameters.AddWithValue("enddate", NpgsqlDbType.Date, endDate);

                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(ds);
                myAdapter.Dispose();

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[1].AsEnumerable().Select(wcgt => new WcgtTimesheetGroup
                    {
                        designation = wcgt.Field<string>("designation") + string.Empty,
                        monthname = wcgt.Field<DateTime>("monthname"),
                        totaltime = wcgt.Field<double>("totaltime"),
                        timesheetcost = wcgt.Field<double>("timesheetcost"),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
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

        public List<WcgtResoureTimesheetGroup> GetResourceGroupTimesheet(string jobCode, DateTime startDate, DateTime endDate)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            List<WcgtResoureTimesheetGroup> result = new List<WcgtResoureTimesheetGroup>();
            try
            {
                //string query = @" CALL  " + Constants.ResourceTimesheetSP + " ('" + startDate + "','" + endDate + "','" + jobCode + "'); FETCH ALL FROM \"rs_resultone\"";
                string query = @" CALL  " + Constants.ResourceTimesheetSP + " (@start_date,@end_date,@injobcode); FETCH ALL FROM \"rs_resultone\"";

                DataSet ds = new DataSet();
                string? pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString);
                conn = new NpgsqlConnection(pgsqlConnection);
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("start_date", NpgsqlDbType.Date, startDate);
                cmd.Parameters.AddWithValue("end_date", NpgsqlDbType.Date, endDate);
                cmd.Parameters.AddWithValue("injobcode", NpgsqlDbType.Text, jobCode);

                NpgsqlDataAdapter myAdapter = new NpgsqlDataAdapter(cmd);
                myAdapter.Fill(ds);
                myAdapter.Dispose();

                if (ds != null && ds.Tables.Count > 0)
                {
                    result = ds.Tables[1].AsEnumerable().Select(wcgt => new WcgtResoureTimesheetGroup
                    {
                        empcode = wcgt.Field<string>("empcode") + string.Empty,
                        empname = wcgt.Field<string>("empname") + string.Empty,
                        totaltime = wcgt.Field<double>("totaltime"),
                        timesheetcost = wcgt.Field<double>("timesheetcost")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
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

        public async Task<List<DesignationGradeView>> GetRateByDesignation(List<GetRateDesignationRequestDTO> designations)
        {

            if (designations.Count > 0)
            {
                var desRequest = designations
                    .Select(m => new GetRateDesignationRequestDTO()
                    {
                        Competency = String.IsNullOrEmpty(m.Competency) ? String.Empty : m.Competency.ToLower(),
                        Designation = String.IsNullOrEmpty(m.Designation) ? String.Empty : m.Designation.ToLower()
                    })
                    .ToList();

                var designationList = desRequest.ToList();

                var resultFromDb = await _dbContext.designationgraderateview
                                    .Where(t =>
                                        t.designation_name != null
                                        && t.CompetencyName != null
                                        && t.isactive == true
                                    )
                                    .ToListAsync();

                var result = resultFromDb
                    .Where(t =>
                        designationList.Any(m =>
                            m.Designation != null
                            && m.Designation == t.designation_name.ToLower()
                            && m.Competency != null
                            && m.Competency == t.CompetencyName.ToLower()
                        )
                    )
                    .ToList();

                return result;
            }
            else
            {
                return new List<DesignationGradeView>();
            }
        }

        public async Task<List<Employee>> GetResignedAndAbscondedUsers(List<string> emails)
        {
            var lowerTrimmedEmails = emails.Select(e => e.ToLower().Trim()).ToList();
            var abscondedResignedUsers = await _dbContext.Employees.AsNoTracking()
                                            .Where(m => m.email_id != null && lowerTrimmedEmails.Any(p => p == (m.employee_mid.ToLower().Trim() + "__" + m.email_id.ToLower().Trim())) &&
                                                        (m.resignation_date != null ||
                                                         m.employee_status != null && (
                                                         m.employee_status.ToUpper() == Constants.ABSCONDER.ToUpper() ||
                                                         m.employee_status.ToUpper() == Constants.VOLUNTARY.ToUpper() ||
                                                         m.employee_status.ToUpper() == Constants.INVOLUNTARY.ToUpper())))
                                            .ToListAsync();
            return abscondedResignedUsers;
        }

        public async Task<List<GetUserLeaveHolidayResponseWithUserMaster>> GetUserLeavesHolidays(string designation, DateTime start_date, DateTime end_date)
        {

            var designationId = await _dbContext.Designations
                .Where(m => m.designation_name != null && m.designation_name.ToLower().Trim() == designation.ToLower().Trim())
                .FirstOrDefaultAsync();
            if (designationId == null)
            {
                throw new Exception($"{designation} not found");
            }

            NpgsqlConnection npgsqlConnection = null;
            var response = new List<GetUserLeaveHolidayResponseWithUserMaster>();
            try
            {
                //create a constant file to store all static data like sp name with their params as well
                string pgsqlConnection = _configuration.GetConnectionString(Constants.connectionString).ToString();

                npgsqlConnection = new NpgsqlConnection(pgsqlConnection);
                using (NpgsqlCommand command = new NpgsqlCommand("sp_getUserWithLeavesHolidays", npgsqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("designationid", designationId.designation_id);

                    var pref_weightage_constraintParam = new NpgsqlParameter("start_date", NpgsqlDbType.Date)
                    {
                        Value = DateOnly.FromDateTime(start_date),
                    };
                    command.Parameters.Add(pref_weightage_constraintParam);

                    var pref_weightage_constraintParam1 = new NpgsqlParameter("end_date", NpgsqlDbType.Date)
                    {
                        Value = DateOnly.FromDateTime(end_date),
                    };
                    command.Parameters.Add(pref_weightage_constraintParam1);
                    JsonSerializerOptions options = new()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true
                    };

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
                                response.Add(JsonConvert.DeserializeObject<GetUserLeaveHolidayResponseWithUserMaster>(Convert.ToString(json)));
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

        public async Task<List<string>> GetCompetencyByBUIDs(List<string> buids)
        {
            return await _dbContext.Competencies
                .Where(a => a.isactive == true && buids.Contains(a.BuId!))
                .Select(x => x.CompetencyId)
                .Distinct()
                .ToListAsync();
        }


        public async Task<List<Employee>> GetEmployeesByBUAndCompetencies(List<string> buids, List<string> competencyIds)
        {
            if(competencyIds.Count > 0)
            {
                return await _dbContext.Employees
                    .Where(e => buids.Contains(e.business_unit_id!) && competencyIds.Contains(e.CompetencyId!))
                    .ToListAsync();
            }
            else
            {
                return await _dbContext.Employees
                    .Where(e => buids.Contains(e.business_unit_id!))
                    .ToListAsync();
            }                
        }


        #endregion
    }
}
