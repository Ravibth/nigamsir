using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.DTOs.Response
{
    public class ConfigurationGroupMasterResponse
    {
        public Int64 Id { get; set; }

        public string ConfigGroup { get; set; }

        public string ConfigGroupDisplay { get; set; }

        public string ConfigKey { get; set; }

        public string CongigDisplayText { get; set; }

        public string ValueType { get; set; }

        public string DefaultValue { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}
