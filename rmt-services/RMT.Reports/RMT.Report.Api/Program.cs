using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RMT.Report.Api;
using RMT.Report.Application.Handlers.QueryHandlers;
using RMT.Report.Infrastructure.Repositories;
using RMT.Reports.Infrastructure.Data;
using RMT.Reports.Infrastructure.Repositories;
using RMT.Report.Api.Controllers;
using RMT.Report.Api.Services;
using RMT.Reports.Application.IHttpServices;
using RMT.Reports.Application.HttpServices;
using RMT.Reports.Application.Handlers.QueryHandlers;

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
    })
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RMT.Reports.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));
builder.Services.AddHttpClient<WcgtHttpService>();

builder.Services.AddDbContext<ReportsDBContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("ReportsDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeeAllocationTimeSheetQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetCapacityUtilizationOverviewQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ScheduledVsActualVarianceChartQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SummaryStatisticsChartQueryHandler>());
builder.Services.AddScoped(typeof(IReportRepository), typeof(ReportRepository));
builder.Services.AddScoped(typeof(IWcgtHttpService), typeof(WcgtHttpService));
builder.Services.AddScoped(typeof(IEmployeeHttpService), typeof(EmployeeHttpService));
builder.Services.AddScoped(typeof(IIdentityHttpService), typeof(IdentityHttpService));

builder.Services.TryAddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));

string[] corsPolicyConsumerHostUrl = builder.Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value.Split(";");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    //builder.WithOrigins(corsPolicyConsumerHostUrl).AllowAnyMethod().AllowAnyHeader();
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
startup.Configure(app, builder.Environment);

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
