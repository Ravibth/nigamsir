using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Budget
{
    public class ProjectConfiguration
    {
        public Int64 Id { get; set; }
        public Int64 ConfigId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; } //

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }
        public virtual ConfigurationGroups ConfigurationGroup { get; set; }
    }
    public class ConfigurationGroups
    {
        public Int64 Id { get; set; }

        public string ConfigGroup { get; set; }

        public string ConfigGroupDisplay { get; set; }

        public string ConfigKey { get; set; }

        public string CongigDisplayText { get; set; }

        public string ValueType { get; set; } // stri , bool ,

        public string ConfigType { get; set; }

        public Int32 SortOrder { get; set; }

        public Int64? ConfigurationGroupMasterId { get; set; }

        public bool IsAll { get; set; }

        public string AllValue { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
