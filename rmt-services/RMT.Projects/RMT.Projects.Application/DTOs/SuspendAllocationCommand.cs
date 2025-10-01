using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class SuspendAllocationCommand : IRequest<List<SuspendAllocationResponse>>
    {
        public List<KeyValuePair<string, string>> ProjectCode { get; set; }
    }
}
