using Azure.Identity;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RMT.Scheduler;
using RMT.Scheduler.Constants;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.Configurations;
using RMT.Scheduler.service.Employee;
using RMT.Scheduler.service.Oracle;
using RMT.Scheduler.service.Project;
using RMT.Scheduler.service.WCGT;
using System;

[assembly: WebJobsStartup(typeof(Startup))]
namespace RMT.Scheduler
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddScoped(typeof(IWCGTHttpService), typeof(WCGTHttpService));
            builder.Services.AddScoped(typeof(IConfigurationService), typeof(ConfigurationService));
            builder.Services.AddScoped(typeof(IAzureServiceBusService), typeof(AzureServiceBusService));
            builder.Services.AddScoped(typeof(IAzureBlobStorageService), typeof(AzureBlobStorageService));
            builder.Services.AddScoped(typeof(IOracleService), typeof(OracleService));
            builder.Services.AddScoped(typeof(IAllocationHttpService), typeof(AllocationHttpService));
            builder.Services.AddScoped(typeof(IProjectHttpService), typeof(ProjectHttpService));
            builder.Services.AddScoped(typeof(IEmployeeHttpService), typeof(EmployeeHttpService));
            builder.Services.AddAzureClients(options =>
            {
                string sbConnectionMethod = Environment.GetEnvironmentVariable("ServiceBusConnectionMethod");
                Console.WriteLine(sbConnectionMethod);
                if (sbConnectionMethod == "AD")
                {
                    //ServiceBusConnection using AD authentication
                    var fullQualifiedName = Environment.GetEnvironmentVariable("AzureServiceBus__fullyQualifiedNamespace");
                    var clientId = Environment.GetEnvironmentVariable("SBClientId");
                    var clientSecret = Environment.GetEnvironmentVariable("SBClientSecret");
                    var tenatId = Environment.GetEnvironmentVariable("SBTenantId");
                    // Create a TokenCredential using client credentials
                    var credential = new ClientSecretCredential(tenatId, clientId, clientSecret);
                    options.AddServiceBusClientWithNamespace(fullQualifiedName).WithCredential(credential);
                }
                else
                {
                    //ServiceBusConnection using Access Key connection string
                    var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus");
                    options.AddServiceBusClient(connectionString);
                }
            });

            builder.Services.AddAzureClients(options =>
            {
                string blobConnectionMethod = Environment.GetEnvironmentVariable(Constant.EnvAppSettingConstants.BLOB_Connection_Method);
                Console.WriteLine(blobConnectionMethod);
                if (blobConnectionMethod == "AD")
                {
                    //BlobServiceClientConnection using AD authentication
                    var fullQualifiedName = Environment.GetEnvironmentVariable(Constant.EnvAppSettingConstants.BLOB_Connection_String);
                    var clientId = Environment.GetEnvironmentVariable("SBClientId");
                    var clientSecret = Environment.GetEnvironmentVariable("SBClientSecret");
                    var tenatId = Environment.GetEnvironmentVariable("SBTenantId");
                    // Create a TokenCredential using client credentials
                    var credential = new ClientSecretCredential(tenatId, clientId, clientSecret);
                    options.AddBlobServiceClient(fullQualifiedName).WithCredential(credential);
                }
                else
                {
                    //BlobServiceClientConnection using Access Key connection string
                    var connectionString = Environment.GetEnvironmentVariable(Constant.EnvAppSettingConstants.BLOB_Connection_String);
                    options.AddBlobServiceClient(connectionString);
                }
            });


        }
    }
}
