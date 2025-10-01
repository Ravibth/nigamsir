using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.DTOs
{
    public class BusinessUnit
    {
        public string BuId { get; set; }
        public string BuName { get; set; }
        public string? BuMid { get; set; }
    }
    public class Offering
    {
        public string OfferingId { get; set; }
        public string OfferingName { get; set; }
        public string? OfferingMid { get; set; }
    }
    public class Solution
    {
        public string SolutionId { get; set; }
        public string SolutionName { get; set; }
        public string? SolutionMid { get; set; }
    }
    public class Location
    {
        public string location_id { get; set; }
        public string? location_mid { get; set; }
        public string location_name { get; set; }
        public string? region_name { get; set; }
    }
    public class Industry
    {
        public string industry_id { get; set; }
        public string industry_name { get; set; }
        public double year_of_experience { get; set; }
        public string description { get; set; }
    }
    public class SubIndustry
    {
        public string sub_industry_id { get; set; }
        public string sub_industry_name { get; set; }
    }
    public class PreferenceDetails
    {
        public BusinessUnit? businessUnit { get; set; }
        public Offering? offering { get; set; }
        public Solution? solution { get; set; }
        public Location? location { get; set; }
        public Industry? industry { get; set; }
        public SubIndustry? subIndustry { get; set; }
    }
}
