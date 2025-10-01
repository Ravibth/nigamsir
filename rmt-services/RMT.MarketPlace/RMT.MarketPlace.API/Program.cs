using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using RMT.MarketPlace.API.Controllers;
using RMT.MarketPlace.API.Services;
using RMT.MarketPlace.Application.Handlers.CommandHandlers;
using RMT.MarketPlace.Application.Handlers.QueryHandlers;
using RMT.MarketPlace.Application.HttpServices;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Domain.Repositories;
using RMT.MarketPlace.Infrastructure.Data;
using RMT.MarketPlace.Infrastructure.Repositories;

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
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RMT.MarketPlace.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddDbContext<MarketPlaceDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("MarketPlacePGDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EmpProjectinterestCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddProjectToMarketPlaceCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EmpProjectInterestScoreCommandHandler>());
builder.Services.AddHttpClient<AllocationServiceHttpApi>();

builder.Services.AddScoped(typeof(IMarketPlaceRepository), typeof(MarketPlaceRepository));
builder.Services.AddScoped(typeof(IAllocationServiceHttpApi), typeof(AllocationServiceHttpApi));
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));

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
