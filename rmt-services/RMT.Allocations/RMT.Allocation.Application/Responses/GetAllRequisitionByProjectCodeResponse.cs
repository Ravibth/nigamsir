using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMT.Allocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Allocation.Domain.DTO;

namespace RMT.Allocation.Application.Responses
{
    public class DemandDTO
    {
        public Int64? TotalDemands { get; set; }
        public Int64? PendingDemands { get; set; }
        public Guid[] Requisitions { get; set; }
        public Boolean? AllResourcesHaveSameDetails { get; set; }
    }
    public class GetAllRequisitionByProjectCodeResponse
    {

        public Guid Id { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 EffortsPerDay { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string OfferingsId { get; set; }
        public string SolutionsId { get; set; }
        public string BUId { get; set; }
        public string Competency { get; set; }
        public string CompetencyId { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64 RequisitionTypeId { get; set; }
        public RequisitionType RequisitionType { get; set; }
        public RequisitionDemand demands { get; set; }
        public List<RequisitionParameters> RequisitionParameters { get; set; }
        public List<RequisitionSkill> RequisitionSkill { get; set; }
        public List<RequisitionParameterValues> RequisitionParameterValues { get; set; }
        public string? Score { get; set; }
        public bool? hasPermissionToEdit { get; set; }
        public bool? hasPermissionToDelete { get; set; }
        public string? Suggestion { get; set; }
    }
}