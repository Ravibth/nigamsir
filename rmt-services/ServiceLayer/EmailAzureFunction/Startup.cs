using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services;
using ServiceLayer.Services.AllocationService;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.EmailService;
using ServiceLayer.Services.EmployeeService;
using ServiceLayer.Services.IdentityService;
using ServiceLayer.Services.MarketPlaceService;
using ServiceLayer.Services.NotificationService;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.RolesAndPermissionHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper;
using ServiceLayer.Services.ProjectService;
using ServiceLayer.Services.PushNotificationService;
using ServiceLayer.Services.ServiceBus;
using ServiceLayer.Services.SyncEventService;
using ServiceLayer.Services.WCGT;
using ServiceLayer.Services.WorkflowService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(ServiceLayer.Startup))]

namespace ServiceLayer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddSingleton<IWorkflowService, WorkflowService>();
            //builder.Services.AddSingleton<IWorkflowService>((s) =>
            //{
            //    return new WorkflowService();
            //});
            //builder.Services.AddSingleton<IEmailService>((s) =>
            //{
            //    return new EmailService();
            //});

            builder.Services.AddScoped(typeof(IWorkflowService), typeof(WorkflowService));
            builder.Services.AddScoped(typeof(IEmailService), typeof(EmailService));
            builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            builder.Services.AddScoped(typeof(IIdentityService), typeof(IdentityService));
            builder.Services.AddScoped(typeof(INotificationService), typeof(NotificationService));
            builder.Services.AddScoped(typeof(IProjectService), typeof(ProjectService));
            builder.Services.AddScoped(typeof(IPushNotificationService), typeof(PushNotificationService));
            builder.Services.AddScoped(typeof(IConfigurationService), typeof(ConfigurationService));
            builder.Services.AddScoped(typeof(IAllocationService), typeof(AllocationService));
            builder.Services.AddScoped(typeof(IMarketPlaceService), typeof(MarketPlaceService));
            builder.Services.AddScoped(typeof(IMarketPlace), typeof(MarketPlace));
            builder.Services.AddScoped(typeof(ISyncEvent) , typeof(SyncEvent));
            builder.Services.AddScoped(typeof(IRolesAndPermission), typeof(RolesAndPermission));
            builder.Services.AddScoped(typeof(IWorkflowNotificationHelper), typeof(WorkflowNotificationHelper));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddScoped(typeof(IWCGTHttpService), typeof(WCGTHttpService));
            builder.Services.AddScoped(typeof(IAzureServiceBusService), typeof(AzureServiceBusService));
            builder.Services.AddLogging();
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
                    //.ConfigureOptions(Configuration);
                }
                else
                {
                    //ServiceBusConnection using Access Key connection string
                    var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus");
                    options.AddServiceBusClient(connectionString);
                    //.ConfigureOptions(builder.);
                }
            });

            //builder.Services.AddHttpClient<HttpClient>("ignoreSSL")
            //    .ConfigurePrimaryHttpMessageHandler(() =>
            //    {
            //        //SSL certificate Error resolution
            //        return new HttpClientHandler
            //        {
            //            ServerCertificateCustomValidationCallback = (m, crt, chn, e) => true
            //        };
            //    });

        }

    }
}