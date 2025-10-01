using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.Handlers.QueryHandlers;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.CommandHandlers
{

    public class GetAllProjectDetailsForMarketPlaceCommand : IRequest<List<MarketPlaceProjectDetailDTO>>
    {
        public int limit { get; set; }
        public int pagination { get; set; }
        public DateTime? currentDateValue { get; set; } = DateTime.UtcNow;
        public bool? showLiked { get; set; }
        public string? emailId { get; set; }
        public List<string>? buFiltervalue { get; set; }
        public List<string>? offeringsFiltervalue { get; set; }
        public List<string>? solutionsFiltervalue { get; set; }
        public List<string>? industryFiltervalue { get; set; }
        public List<string>? subIndustryFiltervalue { get; set; }
        public List<string>? locationFiltervalue { get; set; }

        public bool? isAllocatedToProject { get; set; }
        public DateTime? startDateFiltervalue { get; set; }
        public DateTime? endDateFiltervalue { get; set; }
        public string? selectedValueForSorting { get; set; }
        public string? orderBy { get; set; }

    }

    public class FieldToBeMasked
    {
        public bool? projectName { get; set; }
        public bool? clientName { get; set; }
        public bool? clientGroup { get; set; }
        public bool? projectCode { get; set; }
        public bool? pipelineCode { get; set; }
        public bool? jobCode { get; set; }
        public bool? projectID { get; set; }

    }

    public class GetAllProjectDetailsForMarketPlaceCommandHandler : IRequestHandler<GetAllProjectDetailsForMarketPlaceCommand, List<MarketPlaceProjectDetailDTO>>
    {
        private readonly IMarketPlaceRepository _MarketPlaceRepository;
        private readonly IAllocationServiceHttpApi _allocationServiceHttpApi;
        public GetAllProjectDetailsForMarketPlaceCommandHandler(IMarketPlaceRepository MarketPlaceRepository, IAllocationServiceHttpApi allocationServiceHttpApi)
        {
            _MarketPlaceRepository = MarketPlaceRepository;
            _allocationServiceHttpApi = allocationServiceHttpApi;
        }

        public async Task<List<MarketPlaceProjectDetailDTO>> Handle(GetAllProjectDetailsForMarketPlaceCommand request, CancellationToken cancellationToken)
        {
            //var  result= await _Repo.GetAllMarketPlaceProjectDetail();
            var result = await _MarketPlaceRepository.GetAllMarketPlaceProjectDetail(request.pagination, request.limit, request.emailId, request.showLiked,
                request.buFiltervalue?.Select(x => x.ToLower()).ToList(), request.offeringsFiltervalue?.Select(x => x.ToLower()).ToList(), request.solutionsFiltervalue?.Select(x => x.ToLower()).ToList(),
                //request.smegFiltervalue?.Select(x => x.ToLower()).ToList(), request.smeFiltervalue?.Select(x => x.ToLower()).ToList(), request.ruFiltervalue?.Select(x => x.ToLower()).ToList(),
                request.industryFiltervalue?.Select(x => x.ToLower()).ToList(), request.subIndustryFiltervalue?.Select(x => x.ToLower()).ToList(),
                request.locationFiltervalue?.Select(x => x.ToLower()).ToList(),
                request.isAllocatedToProject, request.startDateFiltervalue, request.endDateFiltervalue, request.selectedValueForSorting, request.orderBy,
                request.currentDateValue);

            List<MarketPlaceProjectDetailDTO> mpDetailsDto = null;

            if (result != null)
            {
                mpDetailsDto = MarketPlaceMapper.Mapper.Map<List<MarketPlaceProjectDetailDTO>>(result);
                if (mpDetailsDto is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
            }

            //List<string> distinctProjectCode = obj?.Select(x => x.ProjectCode.ToLower()).Distinct().ToList();

            if (!string.IsNullOrEmpty(request.emailId))
            {
                var projectAllocations = await _allocationServiceHttpApi.GetAllocationsByEmail(request.emailId);

                foreach (var item in mpDetailsDto)
                {
                    var allocated_flag = projectAllocations
                        //.Where(x => x.ProjectCode.ToLower() == item.ProjectCode?.ToLower()).Any())
                        .Where(m =>
                        string.Compare(m.PipelineCode, item.PipelineCode, true) == 0
                        //Convert.ToString(m.PipelineCode).Trim().ToLower() == Convert.ToString(item.PipelineCode).Trim().ToLower()
                        && ((string.IsNullOrEmpty(m.JobCode) && string.IsNullOrEmpty(item.JobCode)) ||
                        (!string.IsNullOrEmpty(item.JobCode) == true && string.Compare(m.JobCode, item.JobCode, true) == 0))).Any();

                    if (allocated_flag)
                    {
                        item.IsAllocated = true;
                    }
                    else
                        item.IsAllocated = false;
                }
            }
            if (mpDetailsDto != null && request.isAllocatedToProject == true)
            {
                mpDetailsDto = mpDetailsDto.Where(x => Convert.ToBoolean(x.IsAllocated) == true).ToList();
            }
            else if (mpDetailsDto != null && request.isAllocatedToProject == false)
            {
                mpDetailsDto = mpDetailsDto.Where(x => Convert.ToBoolean(x.IsAllocated) == false).ToList();
            }

            // code for masking 

            foreach (var i in mpDetailsDto)
            {
                var replaceMask = "xxxxxxxx";

                if (!string.IsNullOrEmpty(i.JsonData))
                {

                    var myDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(i.JsonData);

                    //dynamic jsonObj = JsonConvert.DeserializeObject(i.JsonData);

                    if (myDictionary != null && myDictionary.Count > 0)
                    {
                        foreach (var key in myDictionary.Keys)
                        {
                            Console.WriteLine(myDictionary[key]);

                            if ((myDictionary.ContainsKey("projectCode") && Convert.ToBoolean(myDictionary["projectCode"]) == true) || (myDictionary.ContainsKey("projectID") && Convert.ToBoolean(myDictionary["projectID"])) == true)
                            {
                                i.Description = MaskString(i.Description, i.PipelineCode, replaceMask);
                                i.Description = MaskString(i.Description, i.JobCode, replaceMask);
                                if (!string.IsNullOrEmpty(i.JobCode))
                                    i.JobCode = replaceMask;
                                if (i.IspipeLine == true && !string.IsNullOrEmpty(i.PipelineCode))
                                {
                                    i.PipelineCode = replaceMask;
                                }
                            }

                            if (myDictionary.ContainsKey("projectName") && Convert.ToBoolean(myDictionary["projectName"]) == true)
                            {
                                i.Description = MaskString(i.Description, i.PipelineName, replaceMask);
                                i.Description = MaskString(i.Description, i.JobName, replaceMask);
                                if (!string.IsNullOrEmpty(i.JobName))
                                    i.JobName = replaceMask;
                                if (i.IspipeLine == true && !string.IsNullOrEmpty(i.PipelineCode))
                                {
                                    i.PipelineName = replaceMask;
                                }
                            }
                            var objProperties = i.GetType().GetProperties();
                            string propertyName = key[0].ToString().ToUpper() + key.Substring(1);

                            if (objProperties != null && objProperties.Any(x => x.Name == propertyName) && (myDictionary.ContainsKey(key) && Convert.ToBoolean(myDictionary[key]) == true))
                            {
                                object mpAttributeValue = i.GetType().GetProperty(propertyName).GetValue(i);

                                i.Description = MaskString(i.Description, Convert.ToString(mpAttributeValue), replaceMask);

                                i.GetType().GetProperty(propertyName).SetValue(i, replaceMask);
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("No property to mask!");
                    }
                }

            }

            // code for pagination 
            int len = mpDetailsDto.Count();
            int startIndex, finalCountToReturn;
            startIndex = (request.pagination - 1) * request.limit;
            finalCountToReturn = Math.Min(len - startIndex, request.limit);
            if (finalCountToReturn <= 0)
            {
                return new List<MarketPlaceProjectDetailDTO> { };
            }
            return await Task.FromResult(mpDetailsDto.GetRange(startIndex, finalCountToReturn));
        }

        public string MaskString(string? input, string? target, string? replaceMask)
        {
            //do not mask if value is null or empty 
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(input))
            {
                return input;
            }
            else if (target != null && input != null)
            {
                string maskedInput = input.Replace(target, replaceMask);
                return maskedInput;
            }
            else
            {
                return replaceMask;
            }

            return input;
        }
    }
}
