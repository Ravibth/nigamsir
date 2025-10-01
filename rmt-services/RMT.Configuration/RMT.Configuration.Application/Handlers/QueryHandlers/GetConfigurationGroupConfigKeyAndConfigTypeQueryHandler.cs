using MediatR;
using RMT.Configuration.Application.DTOs.Response;
using RMT.Configuration.Application.IHttpServices;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetConfigurationGroupConfigKeyAndConfigTypeQuery : IRequest<List<ConfigurationGroupResponse>>
    {
        public string ConfigGroup { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigType { get; set; }
        public string AttributeName { get; set; }
    }
    public class GetConfigurationGroupConfigKeyAndConfigTypeQueryHandler : IRequestHandler<GetConfigurationGroupConfigKeyAndConfigTypeQuery, List<ConfigurationGroupResponse>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IWCGTMasterHttpApi _WCGTMasterHttpApi;
        public GetConfigurationGroupConfigKeyAndConfigTypeQueryHandler(IConfigurationRepository configurationRepository, IWCGTMasterHttpApi wCGTMasterHttpApi)
        {
            _configurationRepository = configurationRepository;
            _WCGTMasterHttpApi = wCGTMasterHttpApi;
        }
        public async Task<List<ConfigurationGroupResponse>> Handle(GetConfigurationGroupConfigKeyAndConfigTypeQuery request, CancellationToken cancellationToken)
        {
            //for debugging purpose
            //int i = 0;

            //if (i == 0)
            //{

                List<WCGTBUTreeMappingDTO> buTreeMapping = await _WCGTMasterHttpApi.GetWCGTBUTreeMappingListApiQuery();

                var configurationMaster = await _configurationRepository.GetConfigurationMasterByConfigGroupAndConfigKey(request.ConfigGroup, request.ConfigKey, request.AttributeName, request.ConfigType, buTreeMapping);
                //Transform method to be implemented.

                List<ConfigurationGroupResponse> response = Helper.HelperMethod.TransposeDataMasterWise(new List<ConfigurationMaster>() { configurationMaster }, request.ConfigKey);

                return response;

            //    }
            //    else
            //    {
            //        try
            //        {
            //            request.ConfigGroup = "Requisition_form";
            //            request.ConfigKey = "Location";
            //            request.ConfigType = "Offerings";

            //            /*
            //            List<ConfigurationMainBreakup> allDataBreakUp = GetConfigBreakupData();

            //            var filterdata = allDataBreakUp.Where(a =>
            //            a.ConfigurationMaster.ConfigGroup.ToLower().Trim() == request.ConfigGroup.ToLower().Trim()
            //            && a.ConfigurationMaster.schemaValues.Any(m => m.Key.ToLower().Trim() == request.ConfigKey.ToLower().Trim())
            //            && a.ConfigurationMaster.SelectorConfigType.ToLower().Trim() == request.ConfigType.ToLower().Trim()
            //            ).ToList();

            //            List<ConfigurationGroupResponse> response1 = TransposeData(filterdata, allDataBreakUp, buTreeMapping, request.ConfigGroup, request.ConfigKey, request.ConfigType);

            //            */

            //            //List<ConfigurationMaster> allDataMaster = GetConfigMasterData();
            //            List<ConfigurationMaster> allDataMaster = Helper.HelperMethod.GetDeepeshData();

            //            //var filterdata = allDataMaster;

            //            var filterdata = allDataMaster.Where(a =>
            //            a.ConfigGroup.ToLower().Trim() == request.ConfigGroup.ToLower().Trim()
            //            && a.schemaValues.Any(m => m.Key.ToLower().Trim() == request.ConfigKey.ToLower().Trim())
            //            && a.SelectorConfigType.ToLower().Trim() == request.ConfigType.ToLower().Trim()
            //            ).ToList();

            //            List<ConfigurationGroupResponse> response1 = Helper.HelperMethod.TransposeDataMasterWise(filterdata, request.ConfigKey);

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Catch", ex);
            //        }

            //        return null;
            //    }
        }

    }
}
