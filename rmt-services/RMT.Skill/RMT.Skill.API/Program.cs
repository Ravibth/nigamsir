using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using RMT.Skill.API;
using RMT.Skill.API.Controllers;
using RMT.Skill.API.Services;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Application.Handlers.QueryHandler;
using RMT.Skill.Application.HttpServices;
using RMT.Skill.Application.IHttpServices;
using RMT.Skill.Domain.Repositories;
using RMT.Skill.Infrastructure.Data;
using RMT.Skill.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
});

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
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
        Title = "Skill.API",
        Version = "v1"
    });
});
builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddDbContext<SkillDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("SKILLDB")), ServiceLifetime.Transient);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SkillSubmitCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllSkillQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserSkillsByEmailQueryHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateUserSkillStatusAfterWorkflowCommandHandler>());

builder.Services.AddScoped(typeof(ISkillDataRepository), typeof(SkillDataRepository));
builder.Services.AddScoped(typeof(IUserSkillRepository), typeof(UserSkillRepository));
//builder.Services.AddScoped(typeof(ISkillDataRepository), typeof());
//builder.Services.AddScoped(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.AddScoped(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));
builder.Services.AddHttpClient<WorkflowHttpService>();
builder.Services.AddHttpClient<IdentityUserDetailsHttpApi>();
builder.Services.AddScoped(typeof(IIdentityUserDetailsHttpApi), typeof(IdentityUserDetailsHttpApi));
builder.Services.AddScoped(typeof(IWorkflowHttpService), typeof(WorkflowHttpService));

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

startup.Configure(app, builder.Environment);

app.Run();
