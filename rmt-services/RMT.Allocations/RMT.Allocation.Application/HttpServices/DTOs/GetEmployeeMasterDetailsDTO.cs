using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetEmployeeMasterDetailsDTO
    {
        public string designation { get; set; }
        public List<string> Emails { get; set; }
    }

    public class GetEmployeeMasterDetailsResponseDTO
    {
        public int count { get; set; }
        public List<UserMasterList> rows { get; set; }
    }
}
