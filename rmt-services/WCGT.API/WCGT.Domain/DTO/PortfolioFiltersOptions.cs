using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;

namespace WCGT.Domain.DTO
{
    public class PortfolioFiltersOptions
    {
        public List<SuperCoach> supercoaches { get; set; }
        public List<SuperCoach> cosupercoaches { get; set; }
        public List<SuperCoach> employees { get; set; }
        public List<Designation> designations { get; set; }
        public List<Client> clients { get; set; }
        public List<Location> locations { get; set; }

    }
}
