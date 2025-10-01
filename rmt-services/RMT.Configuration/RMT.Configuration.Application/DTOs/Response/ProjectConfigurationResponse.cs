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
    public class ProjectConfigurationResponse
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

    }
}
