using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RMT.Configuration.API;
using RMT.Configuration.API.Controllers;
using RMT.Configuration.API.Service;
using RMT.Configuration.Application.Handlers.CommandHandlers;
using RMT.Configuration.Application.HttpServices;
using RMT.Configuration.Application.IHttpServices;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Data;
using RMT.Configuration.Infrastructure.Repositories;

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
        Title = "RMT.Configuration.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));
//builder.Services.AddMediatR(typeof(Startup));

builder.Services.AddDbContext<ConfigurationDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("ConfigurationPGDB")), ServiceLifetime.Transient);

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining);
//builder.Services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateConfigurationGroupCommand>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateMenuMasterCommand>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoggerCommand>());

builder.Services.AddHttpClient<WCGTMasterHttpApi>();

builder.Services.AddScoped(typeof(IConfigurationRepository), typeof(ConfigurationRepository));
//builder.Services.AddScoped(typeof(IMasterRepository), typeof(MasterRepository));
builder.Services.AddScoped(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.AddScoped(typeof(INavigationRepository), typeof(NavigationRepository));
builder.Services.AddScoped(typeof(ILoggerRepository), typeof(LoggerRepository));
builder.Services.AddScoped(typeof(IWCGTMasterHttpApi), typeof(WCGTMasterHttpApi));
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
