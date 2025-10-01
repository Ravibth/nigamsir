using RMT.Projects.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class EmployeeListingProjectData
    {
        public string Name { get; set; }

        public Project Value { get; set; }
    }
}
