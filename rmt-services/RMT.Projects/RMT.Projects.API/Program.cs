using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RMT.Projects.Application.Handlers.CommandHandlers;
using RMT.Projects.Application.Handlers.QueryHandlers;
using RMT.Projects.Application.HttpServices;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Data;
using RMT.Projects.Infrastructure.Repositories;
using RMT.Projects.API.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RMT.Projects.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
});

//builder.Configuration.AddEnvironmentVariables();

//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//    .AddEnvironmentVariables();


//var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();


//builder.Configuration.AddJsonFile("appsettings."{env.EnvironmentName}.json").AddEnvironmentVariables();

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
        Title = "RMT.Projects.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));
builder.Services.AddScoped(typeof(IResourceAllocationHttpApi), typeof(ResourceAllocationHttpApi));
builder.Services.AddHttpClient<ResourceAllocationHttpApi>();

builder.Services.AddDbContext<ProjectDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("ProjectPGDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllProjectsQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProjectCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetMultipleProjectsByCodesQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetProjectListDataByUserQuery>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddUpdateProjectRequisitionAllocationCommand>());

builder.Services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped(typeof(IIdentityHttpService), typeof(IdentityHttpService));
builder.Services.AddScoped(typeof(IWorkflowHttpService), typeof(WorkflowHttpService));

builder.Services.AddHttpClient<IdentityHttpService>();
builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));
builder.Services.AddScoped(typeof(IWcgtHttpService), typeof(WcgtHttpService));
builder.Services.AddScoped(typeof(IConfigurationHttpApi), typeof(ConfigurationHttpApi));

builder.Services.AddHttpClient<WcgtHttpService>();

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
