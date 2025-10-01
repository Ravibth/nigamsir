using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RMT.Notification.API.Controllers;
using RMT.Notification.API.Hubs;
using RMT.Notification.API.Services;
using RMT.Notification.Application.Handlers.CommandHandlers;
using RMT.Notification.Application.Handlers.QueryHandlers;
using RMT.Notification.Application.HttpServices;
using RMT.Notification.Application.HttpServices.AllocationService;
using RMT.Notification.Application.HttpServices.MarketPlaceService;
using RMT.Notification.Application.HttpServices.ProjectConfigurationService;
using RMT.Notification.Application.HttpServices.SkillHtppService;
using RMT.Notification.Domain.Repositories;
using RMT.Notification.Infrastructure.Data;
using RMT.Notification.Infrastructure.Migrations;
using RMT.Notification.Infrastructure.Repositories;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.EmailService;
using ServiceLayer.Services.PushNotificationService;

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
        Title = "RMT.Notification.API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddDbContext<NotificationDbContext>(m => m.UseNpgsql(builder.Configuration.GetConnectionString("RMTNotificationPGDB")), ServiceLifetime.Transient);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MarkNotificationsAsReadCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PostNewNotificationCommandHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetLoggedInUserAllNotificationsCountQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetLoggedInUserNotificationsQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetNotificationTemplateQueryHandler>());// (Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MarkAllNotificationsAsReadCommandHandler>());// (Assembly.GetExecutingAssembly()));


builder.Services.AddScoped(typeof(IPushNotificationService), typeof(PushNotificationService));
builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
//builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddSingleton(typeof(IUserAccessor), typeof(UserAccessor));
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton(typeof(ISkillHttpService), typeof(SkillHttpService));
builder.Services.AddSingleton(typeof(IAllocationHttpService), typeof(AllocationHttpService));
builder.Services.AddSingleton(typeof(IMarketPlaceHttpService), typeof(MarketPlaceHttpService));


builder.Services.AddSingleton(typeof(BaseController), typeof(BaseController));
builder.Services.AddHttpClient<ProjectRoleHttpService>();
builder.Services.AddHttpClient<SkillHttpService>();
builder.Services.AddHttpClient<AllocationHttpService>();

builder.Services.AddScoped(typeof(IProjectRoleHttpService), typeof(ProjectRoleHttpService));
builder.Services.AddScoped(typeof(IConfigurationService), typeof(ConfigurationService));
builder.Services.AddScoped(typeof(IEmailService), typeof(EmailService));
builder.Services.AddScoped(typeof(IProjectConfigurationService), typeof(ProjectConfigurationService));
builder.Services.AddLogging();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

string[] corsPolicyConsumerHostUrl = builder.Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value.Split(";");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    //builder.WithOrigins(corsPolicyConsumerHostUrl).AllowAnyMethod().AllowAnyHeader();
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddSignalR();
// builder.Services.AddSignalR(hubOptions =>
// {
//     hubOptions.EnableDetailedErrors = true;
//     //hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
// });

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
//app.UseCors("corspolicy");
app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("hub");
    // endpoints.MapHub<NotificationHub>("/hub", options =>
    // {
    //     options.Transports =
    //         HttpTransportType.WebSockets |
    //         HttpTransportType.LongPolling;
    // });
});
app.Run();
