using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Employee.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Services
{
    public interface IProjectService
    {
        Task<ProjectDetailsEmployee> GetProjectDetailsEmployee(string pipelineCode, string? jobCode);
    }

    public class ProjectService : IProjectService
    {        
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ProjectService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<ProjectDetailsEmployee> GetProjectDetailsEmployee(string pipelineCode, string? jobCode)
        {
            try
            {
                var baseUrl = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("projectapiUrl").Value);
                var path = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("GetProjectDetailsForEmployee").Value);
                path = path + "?pipelineCode=" + pipelineCode + "&jobCode=" + jobCode;
                var response = await _httpClient.GetAsync(baseUrl + path);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProjectDetailsEmployee>(resp);
                }
                else
                {
                    throw new Exception("Error fetching GetWorkflowDetailsByItemId" + response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class Project
    {
        public Project()
        {
            //ProjectDemands = new List<ProjectDemand>();
            //ProjectJobCodes = new List<ProjectJobCodes>();
            ProjectRoles = new List<ProjectRoles>();
            //ProjectSkills = new List<ProjectSkills>();
            //ProjectRolesView = new List<ProjectRolesView>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? Id { get; set; }
        //public string? ProjectCode { get; set; }//feb
        //public string? ProjectName { get; set; }//feb
        public string? bu { get; set; }
        //public string? sector { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Description { get; set; }
        public string? ProjectAllocationStatus { get; set; }
        public string? Location { get; set; }
        public string? PipelineStage { get; set; }
        public string? ProjectType { get; set; }
        public string? PipelineStatus { get; set; }
        public string? ChargableType { get; set; }
        public string? LegalEntity { get; set; }
        public string? JobLocation { get; set; }
        public string? DeliveryLocation { get; set; }
        public string? GtRefferenceCountry { get; set; }
        public string? RevenueUnit { get; set; }//Recheck
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }
        public string? JustificationToAllocate { get; set; }
        public DateTime? PublishedToMarketPlaceDate { get; set; }
        public DateTime? MarketPlaceExpirationDate { get; set; }

        // ToDo : use this flag to disable the requistion /allocation button in projectlisting-page 
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsRollover { get; set; }
        public int RolloverDays { get; set; }
        [DefaultValue(true)]
        public bool? IsRequisitionCreationallowed { get; set; }
        //[Timestamp]
        public DateTime? CreatedAt { get; set; }
        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }

        public DateTime? SuspendedModifyAt { get; set; }

        public bool? IsSuspended { get; set; } = false;
        public bool? ProjectClosureState { get; set; }//Open:-true , Closed:-false
        public bool? ProjectActivationStatus { get; set; } //Active , In - Active

        //public virtual ICollection<ProjectDemand> ProjectDemands { get; set; }
        //public virtual ICollection<ProjectCompetency> ProjectCompetencies { get; set; }

        //public virtual ICollection<ProjectBudget> ProjectBudget { get; set; }

        //public virtual ICollection<ProjectJobCodes> ProjectJobCodes { get; set; }

        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
        //public virtual ICollection<ProjectRolesView> ProjectRolesView { get; set; }

        ////public virtual ICollection<ProjectSkills> ProjectSkills { get; set; }

        //public virtual ProjectRequisitionAllocation? ProjectRequisitionAllocations { get; set; }

        public string? ClientId { get; set; }

        [NotMapped]
        public int? Total { get; set; }
    }

    public class ProjectDetailsEmployee
    {
        public Int64 Id { get; set; }
        //public string? ProjectCode { get; set; }//feb
        //public string? ProjectName { get; set; }//feb
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public string? JobId { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck

        public string? bu { get; set; }
        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }



        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? ProjectAllocationStatus { get; set; }
        public string? Location { get; set; }
        public string? PipelineStage { get; set; }
        public string? ProjectType { get; set; }
        public string? PipelineStatus { get; set; }
        public string? ChargableType { get; set; }
        public string? RevenueUnit { get; set; }
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
        //public virtual ICollection<ProjectJobCodes> ProjectJobCodes { get; set; }

        //public virtual ICollection<ProjectRolesView> ProjectRolesView { get; set; }

        //public virtual ICollection<ProjectDemand> ProjectDemand { get; set; }
        public string? ClientId { get; set; }
    }

    public class ProjectRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        //[ForeignKey("ProjectId")]
        //public virtual Project? Project { get; set; }

        [EmailAddress]
        public string User { get; set; }
        public string UserName { get; set; }

        [Required]
        [EnumDataType(typeof(RoleType))]
        public string Role { get; set; }

        //public string? DelegateRole { get; set; }
        public string? DelegateUserName { get; set; }
        public string? DelegateEmail { get; set; }

        public string? Description { get; set; }
        public string? ApplicationRole { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        //[Timestamp]
        public DateTime? CreatedAt { get; set; }

        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }

        [NotMapped]
        public string? ParentEmail { get; set; }
        [NotMapped]
        public string? ParentName { get; set; }

    }

    public enum RoleType
    {
        Requestor,
        EngagementLeader,
        EO,
        Delegate,
        AdditionalEl
    }    
}
