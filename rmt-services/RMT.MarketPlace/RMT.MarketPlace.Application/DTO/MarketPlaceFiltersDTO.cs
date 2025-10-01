using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.MarketPlace.Domain.DTOs.Response;

namespace RMT.MarketPlace.Application.DTO
{
    public class MarketPlaceFiltersDTO
    {
        public List<string>? buFiltervalue { get; set; }

        public List<Offerings>? offeringsFiltervalue { get; set; }
        public List<Solutions>? solutionsFiltervalue { get; set; }

        public List<string>? industryFiltervalue { get; set; }
        public List<SubIndustry>? subIndustryFiltervalue { get; set; }
        public List<string>? locationFiltervalue { get; set; }

    }
}
