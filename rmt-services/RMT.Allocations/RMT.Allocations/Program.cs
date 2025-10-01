using Azure.Identity;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.Repositories;
using RMT.Allocations.API;
using RMT.Allocations.API.Services;
using RMT.Scheduler.service;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
});

//builder.Configuration
//    .AddJsonFile($"appsettingsGlobal.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    })
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RMT.Allocations.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddDbContext<AllocationDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("AllocationDB")), ServiceLifetime.Transient);
//builder.Services.AddDbContext<AllocationRequistionViewDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("AllocationDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetResourceAllocationByEmailQueryHandler>());// (Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateResourceAllocationCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetProjectsByEmployeeEmailQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetSystemSuggestionsByRequisitionIdQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllRequisitionByProjectCodeQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUsersTimelineQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DoesUserHaveAnyFutureOrOngoingAllocationsQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RemoveUsersAllDraftAllocationsCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetRequisitionDetailsByDateQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserAvailabilitiesForSystemSuggestionHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetDraftTimesheetQueryHandler>());// (Assembly.GetExecutingAssembly()));

builder.Services.AddHttpClient<EmployeePreferencesHttpApi>();
builder.Services.AddHttpClient<EmployeeMasterHttpApi>();
builder.Services.AddHttpClient<MarketPlaceHttpApi>();
builder.Services.AddHttpClient<GetEmployeeLeavesHttpApi>();
builder.Services.AddHttpClient<ProjectServiceHttpApi>();
builder.Services.AddHttpClient<WCGTTimesheetHttpApi>();
builder.Services.AddHttpClient<WCGTResourceTimesheetHttpApi>();
builder.Services.AddHttpClient<IdentityUserDetailsHttpApi>();
builder.Services.AddHttpClient<WCGTMasterHttpApi>();
builder.Services.AddHttpClient<ConfigurationHttpService>();
builder.Services.AddHttpClient<SkillHttpServiceApi>();
builder.Services.AddHttpClient<WorkflowServiceHttpApi>();
builder.Services.AddHttpClient<HolidayHttpService>();
builder.Services.AddHttpClient<AzureHttpService>();

builder.Services.AddAzureClients(options =>
{
    string sbConnectionMethod = builder.Configuration.GetSection("AzureSBConfig").GetSection("ServiceBusConnectionMethod").Value;

    if (sbConnectionMethod == "AD")
    {
        //ServiceBusConnection using AD authentication
        var fullQualifiedName = builder.Configuration.GetSection("AzureSBConfig").GetSection("AzureServiceBusFullQualifiedName").Value;
        var clientId = builder.Configuration.GetSection("AzureSBConfig").GetSection("SBClientId").Value;
        var clientSecret = builder.Configuration.GetSection("AzureSBConfig").GetSection("SBClientSecret").Value;
        var tenatId = builder.Configuration.GetSection("AzureSBConfig").GetSection("SBTenantId").Value;
        // Create a TokenCredential using client credentials
        var credential = new ClientSecretCredential(tenatId, clientId, clientSecret);
        options.AddServiceBusClientWithNamespace(fullQualifiedName).WithCredential(credential).ConfigureOptions(builder.Configuration);
    }
    else
    {
        //ServiceBusConnection using Access Key connection string
        var connectionString = builder.Configuration.GetSection("AzureSBConfig").GetSection("AzureServiceBus").Value;
        options.AddServiceBusClient(connectionString).ConfigureOptions(builder.Configuration);
    }
});

builder.Services.AddScoped(typeof(IResourceAllocationRepository), typeof(ResourceAllocationRepository));
builder.Services.AddScoped(typeof(IRequisitionRepository), typeof(RequisitionRepository));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped(typeof(IEmployeeMasterHttpApi), typeof(EmployeeMasterHttpApi));
builder.Services.AddScoped(typeof(IEmployeePreferencesHttpApi), typeof(EmployeePreferencesHttpApi));
builder.Services.AddScoped(typeof(IMarketPlaceHttpApi), typeof(MarketPlaceHttpApi));
builder.Services.AddScoped(typeof(IGetEmployeeLeavesHttpApi), typeof(GetEmployeeLeavesHttpApi));
builder.Services.AddScoped(typeof(IProjectServiceHttpApi), typeof(ProjectServiceHttpApi));
builder.Services.AddScoped(typeof(IDatesUtils), typeof(DatesUtils));
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.AddScoped(typeof(IWCGTMasterHttpApi), typeof(WCGTMasterHttpApi));
builder.Services.AddScoped(typeof(IConfigurationHttpService), typeof(ConfigurationHttpService));
builder.Services.AddScoped(typeof(ISkillHttpServiceApi), typeof(SkillHttpServiceApi));
builder.Services.AddScoped(typeof(IIdentityUserDetailsHttpApi), typeof(IdentityUserDetailsHttpApi));
builder.Services.AddScoped(typeof(IWorkflowHttpApi), typeof(WorkflowServiceHttpApi));
builder.Services.AddScoped(typeof(IHolidayHttpService), typeof(HolidayHttpService));
builder.Services.AddScoped(typeof(IAzureHttpService), typeof(AzureHttpService));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

string[] corsPolicyConsumerHostUrl = builder.Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value.Split(";");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    //builder.WithOrigins(corsPolicyConsumerHostUrl).AllowAnyMethod().AllowAnyHeader();
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
startup.Configure(app, builder.Environment); // calling Configure method

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
