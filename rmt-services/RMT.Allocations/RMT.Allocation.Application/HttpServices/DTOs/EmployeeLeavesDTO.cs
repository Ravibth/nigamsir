using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetEmployeeLeaves
    {
        public List<string> emails { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public List<EmployeeMasterDTO>? userMasterData { get; set; }

    }

    public class EmployeeLeavesDTO
    {
        public string email { get; set; }
        public List<LeavesDTO> leaves { get; set; }
    }
}