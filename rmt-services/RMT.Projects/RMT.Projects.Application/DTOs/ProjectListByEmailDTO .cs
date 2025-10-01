using RMT.Projects.Application.QueryParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectListByEmailDTO
    {
        public string userEmail { get; set; }
        public int limit { get; set; }
        public int pagination { get; set; }       
    }
}
