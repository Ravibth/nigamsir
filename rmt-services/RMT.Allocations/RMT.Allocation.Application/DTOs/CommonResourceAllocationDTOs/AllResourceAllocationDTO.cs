using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static RMT.Allocation.Domain.Constants;

namespace RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs
{
    public class UserDetailsCommonDTO
    {
        public string Email { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Competency { get; set; }
    }
    public class AllResourceAllocationDTO
    {
        public string Email { get; set; }
        public UserDetailsCommonDTO UserInfo { get; set; }
        //[EnumDataType(typeof(EAllocationType))]
        public string type { get; set; }
        public SkillsEntities[] Skills { get; set; }
        public CompetencyMasterDTO competency { get; set; }
        public bool Available { get; set; }
        //public object Meta { get; set; }
        //public bool Interested { get; set; }
        public List<FormValuesForAllocationDTO>? Allocations { get; set; }
        //public Requisition? Requisition { get; set; }
        public Guid? RequisitionId { get; set; }
        public string? Description { get; set; }
        //public bool ShowDescription { get; set; }
        public bool? IsContinuousAllocation { get; set; }

        [Range(Int64.MinValue, Int64.MaxValue)]
        public Int64 TotalEfforts { get; set; }
        public ProjectDTO? ProjectInfo { get; set; }
    }

    public class NewJobCodeMoveDTO
    {
        public List<AllResourceAllocationDTO> AllResourceAllocation { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string NewPipelineCode { get; set; }
        public string NewPipelineName { get; set; }
        public string? NewJobCode { get; set; }
        public string? NewJobName { get; set; }

    }
    public class NewJobCodeMoveResponseDTO
    {
        public List<string> employee_jobcode_change { get; set; }
        public string? projectname { get; set; }
        public string oldprojectcode { get; set; }
        public string newprojectcode { get; set; }
        public string newpipelinecode { get; set; }
        public string newjobcode { get; set; }
        public List<string>? previousrolesemails { get; set; }
    }
}
