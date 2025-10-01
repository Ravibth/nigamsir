using RMT.Employee.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Services
{
    public interface IAllocationService
    {
        Task<List<AllocationByEmailResponse>> GetAllocationsByEmail(string email);
    }
}
