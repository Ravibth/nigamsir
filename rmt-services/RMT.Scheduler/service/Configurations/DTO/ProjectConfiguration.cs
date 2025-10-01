using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.Configurations.DTO
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
}
