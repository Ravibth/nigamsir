namespace RMT.Allocation.Domain.DTO.Request
{
    public class RequisitionRequest
    {
        public Guid? Id { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 EffortsPerDay { get; set; }
        public Int64 TotalHours { get; set; }
        public string Status { get; set; }
        //public string Expertise { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        //public string Competency { get; set; }
        //public string CompetencyId { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        // public string SMEG { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsAllResourcesSimilar { get; set; }
        public Int64 NumberOfResources { get; set; }
        public List<ResourceEntities>? resourceEntities { get; set; }
        public Int64 RequisitionTypeId { get; set; }

        public string? requisitionDescription { get; set; }
    }
    public class ResourceEntities
    {
        public string Competency { get; set; }
        public string CompetencyId { get; set; }
        public Int64 Effort_Hrs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsPerDayHourAllocation { get; set; }
        public List<ParametersEntities>? Parameters { get; set; }
        public List<SkillsEntities>? Skills { get; set; }
        public List<RequisitionParameterValueEntities>? Locations { get; set; }
        public List<RequisitionParameterValueEntities>? Industries { get; set; }
        public List<RequisitionParameterValueEntities>? SubIndustries { get; set; }
    }
    public class ParametersEntities
    {
        public string Name { get; set; }
        public Int64 Value { get; set; }
        public bool IsChecked { get; set; } = false;
    }

    public class SkillsEntities
    {
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string? Type { get; set; }
    }

    public class RequisitionParameterValueEntities
    {
        public string Label { get; set; }
    }

    public class UpdateRequisitionRequest
    {
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<UpdateResourceEntities>? resourceEntities { get; set; }
    }
    public class UpdateResourceEntities
    {
        public Guid id { get; set; }
        public Int64 Effort_Hrs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsPerDayHourAllocation { get; set; }
        public string? BusinessUnit { get; set; }
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public string? Description { get; set; }
        //public string? Industry { get; set; }
        //public string? SubIndustry { get; set; }
        public string? Status { get; set; }
        public List<ParametersEntities>? Parameters { get; set; }
        public List<SkillsEntities>? Skills { get; set; }
        public List<RequisitionParameterValueEntities>? Locations { get; set; }
        public List<RequisitionParameterValueEntities>? industries { get; set; }
        public List<RequisitionParameterValueEntities>? subIndustries { get; set; }
    }
}
