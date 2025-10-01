using RMT.Projects.Application.QueryParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectListRequestDTO
    {
        public string userEmail { get; set; }
        public int limit { get; set; }
        public int pagination { get; set; }
        public string? searchQuery { get; set; }
        public List<string>? searchRoles { get; set; }
        public string? orderBy { get; set; }
        public FilterQueryParameters filterQueryParameters { get; set; }
    }
}
