using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using RMT.Employee.API;
using RMT.Employee.API.Controllers;
using RMT.Employee.API.Services;
using RMT.Employee.Application.Handlers.CommandHandlers;
using RMT.Employee.Application.Services;
using RMT.Employee.Domain.Repositories;
using RMT.Employee.Infrastructure.Data;
using RMT.Employee.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
});

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
        Title = "RMT.Employee.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));


builder.Services.AddDbContext<EmployeeDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("EmployeePGDB")), ServiceLifetime.Transient);

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining);
//builder.Services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EmployeePreferenceCommandHandlers>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PreferenceMasterCommandHandler>());
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IAllocationService, AllocationService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));
builder.Services.AddHttpClient<SkillService>();

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
