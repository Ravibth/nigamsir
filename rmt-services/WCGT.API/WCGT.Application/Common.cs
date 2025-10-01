using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WCGT.Domain.Entities;

namespace WCGT.Application
{
    public static class Common
    {
        public enum WCGTDataLogStatus
        {
            Success,
            Failed
        }

        public enum ReportPeriodType { Weekly, Monthly }

        public static class ApplicationLevelSettingsKeys
        {
            public const string Weekends = "Weekends";
            public const string ResignedUserAvailabilityThreshold = "ResignedUserAvailabilityThreshold";
            public const int  Weekly_Total_Working_Hours = 40;
            public static readonly List<string> AvailablesType = new List<string>
                {
                    "Leave",
                    "Holiday",
                    "Allocated",
                    "Available_Time"
                };
        }

        public static WCGTDataLog CreateWCGTDataLogObject<T>(T input_object_json, Type entity_type, Exception ex)
        {
            WCGTDataLog dataLogEntity = new WCGTDataLog()
            {
                input_json = JsonSerializer.Serialize(input_object_json),
                status = WCGTDataLogStatus.Failed.ToString(),
                entity_type = entity_type.FullName,
                error_message = $"Exception:-{ex.Message}; InnerException:-{ex.InnerException?.Message}",
                error_stacktrace = ex.StackTrace,
                createdat = DateTime.UtcNow.ToUniversalTime(),
                modifiedat = DateTime.UtcNow.ToUniversalTime(),
                createdby = "System",
                modifiedby = "System"
            };
            return dataLogEntity;
        }


    }
}
