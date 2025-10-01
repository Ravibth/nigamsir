//using Microsoft.OpenApi.Models;
using WCGT.API;
//using WCGT.Application.Handlers.QueryHandlers;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WCGT.Infrastructure.Data;
using WCGT.API.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
//using WCGT.Application.Handlers.CommandHandlers;
using WCGT.Application.Handlers.CMDHandler;
using WCGT.Application.Handlers.QueryHandlers;
using WCGT.API.Controllers;
using WCGT.Application.Services.IHttpServices;
using WCGT.Application.Services.HttpServices;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
});

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WCGT.API",
        Version = "v1"
    });
});
builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddDbContext<WcgtDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("WCGTDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ClientCommandHandler>());
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProjectCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BUTreeMappingCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ClientLegalEntityCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DesignationCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EmployeeCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<HolidayCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<JobCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LeaveCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LocationCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PipelineCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SectorIndustryCommandHandler>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BUEfficiencyLeaderCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetBUTreeMappingListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetClientLegalEntityLegalEntityQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetClientListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetDesignationListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeeByParamListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeeListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetHolidayListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetJobListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetLeaveListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetLeavesByParamListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetLocationListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPipelineListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetSectorIndustryListQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetResignedAndAbscondedUsersWithLastAvailableDayQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserLeaveHolidayResponseQueryHandler>());

builder.Services.AddScoped(typeof(IWcgtDataRepository), typeof(WcgtDataRepository));
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IConfigurationHttpService), typeof(ConfigurationHttpService));
builder.Services.AddScoped(typeof(IIdentityHttpService), typeof(IdentityHttpService));
builder.Services.AddScoped(typeof(IAllocationHttpService), typeof(AllocationHttpService));


builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

string[] corsPolicyConsumerHostUrl = builder.Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value.Split(";");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    //builder.WithOrigins(corsPolicyConsumerHostUrl).AllowAnyMethod().AllowAnyHeader();
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// builder.Services.AddCors(o => o.AddPolicy("default", builder =>
// {
//     builder.AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();
// }));

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSession();
app.UseCors("corspolicy");
//app.UseCors();
//app.UseHttpsRedirection();
//app.UseRouting();
////app.UseAuthentication();
////app.Use(async (context, next) =>
////{
////    if (!context.User.Identity?.IsAuthenticated ?? false)
////    {
////        context.Response.StatusCode = 401;
////        await context.Response.WriteAsync("Not Authenticated");
////    }
////    else
////    {
////        await next();
////    }
////});
////app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
startup.Configure(app, builder.Environment);

app.Run();
