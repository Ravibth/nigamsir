using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.DTOs.Response
{
    public class ConfigurationGroupResponse
    {
        public Int64 Id { get; set; }
        public string ConfigGroup { get; set; }

        public string ConfigGroupDisplay { get; set; }

        public string ConfigKey { get; set; }

        public string CongigDisplayText { get; set; }

        public string ValueType { get; set; }

        public string ConfigType { get; set; }

        public Int32 SortOrder { get; set; }

        public Int64? ConfigurationGroupMasterId { get; set; }

        public ConfigurationGroupMasterResponse? ConfigurationGroupMaster { get; set; }

        public bool IsAll { get; set; }
        public string AllValue { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public List<ProjectConfigurationResponse>? ProjectConfigurations { get; set; }
    }
}
