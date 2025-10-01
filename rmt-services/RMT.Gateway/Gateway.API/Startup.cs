// <copyright file="Startup.cs" company="RMT">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API
{
    using Azure.Identity;
    using Gateway.API.Dtos;
    using Gateway.API.Helpers;
    using Gateway.API.Helpers.HttpServices;
    using Gateway.API.Helpers.IHttpServices;
    using Gateway.API.Middlewares;
    using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper;
    using Gateway.API.ServiceLayerHelper.WorkflowService;
    using Gateway.API.Services;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Azure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Identity.Web;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;
    using Ocelot.Values;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Eventing.Reader;
    using System.IO;
    using System.Linq;
    using System.Reflection.PortableExecutable;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Startup class, to be used while creating web host builder.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets Configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        private List<ResourcePermissionMapping> ResourcePermissionMapping { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services Collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    //ValidateIssuerSigningKey = true,
                //    ValidateIssuer = true,
                //    ValidateAudience = false,
                //    //ValidAudiences = Configuration["AzureAd:ClientId"].Split(';'),
                //    //ValidIssuers = Configuration["AzureAd:ValidIssuers"].Split(';'),
                //    // You can add more validation parameters as needed.
                //};

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.WriteAsync(Constants.InValidTokenMessage);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        if (context.Principal.Identity is ClaimsIdentity identity)
                        {
                            var scpClaims = context.Principal.Claims.ToList();
                            foreach (var scpClaim in scpClaims)
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, scpClaim.Value));
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddOcelot();
            services.AddSignalR();

            services.AddAuthorization();
            services.AddAzureClients(options =>
            {
                string sbConnectionMethod = Configuration.GetSection("AzureSBConfig").GetSection("ServiceBusConnectionMethod").Value;

                if (sbConnectionMethod == "AD")
                {
                    //ServiceBusConnection using AD authentication
                    var fullQualifiedName = Configuration.GetSection("AzureSBConfig").GetSection("AzureServiceBusFullQualifiedName").Value;
                    var clientId = Configuration.GetSection("AzureSBConfig").GetSection("SBClientId").Value;
                    var clientSecret = Configuration.GetSection("AzureSBConfig").GetSection("SBClientSecret").Value;
                    var tenatId = Configuration.GetSection("AzureSBConfig").GetSection("SBTenantId").Value;
                    // Create a TokenCredential using client credentials
                    var credential = new ClientSecretCredential(tenatId, clientId, clientSecret);
                    options.AddServiceBusClientWithNamespace(fullQualifiedName).WithCredential(credential).ConfigureOptions(Configuration);
                }
                else
                {
                    //ServiceBusConnection using Access Key connection string
                    var connectionString = Configuration.GetSection("AzureSBConfig").GetSection("AzureServiceBus").Value;
                    options.AddServiceBusClient(connectionString).ConfigureOptions(Configuration);
                }
            });

            services.AddHttpContextAccessor();
            this.ResourcePermissionMapping = GetPermissionsFromJson();

            services.AddCors(opt =>
                {
                    string[] corsPolicyConsumerHostUrl = Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value?.Split(";", StringSplitOptions.TrimEntries);
                    bool useCorsPolicyFlag = Configuration.GetSection("MicroserviceApiSettings").GetSection("UseCorsPolicyFlag").Value == "true";

                    opt.AddPolicy(Constants.CorsPolicy, policy =>
                    {
                        if (useCorsPolicyFlag == true)
                        {
                            policy
                            .WithOrigins(corsPolicyConsumerHostUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                        else
                        {
                            policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                    });
                });
            //services.AddHttpClient(Constants.ProjectService, client =>
            //{
            //    client.BaseAddress = new Uri(this.Configuration[Constants.ProjectServiceURL]);
            //    client.DefaultRequestHeaders.Add(Constants.Accept, Constants.AppJsonContentType);
            //}).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            //services.AddHttpClient(Constants.IdentityService, client =>
            //{
            //    client.BaseAddress = new Uri(this.Configuration[Constants.IdentityServiceURL]);
            //    client.DefaultRequestHeaders.Add(Constants.Accept, Constants.AppJsonContentType);
            //}).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            services.AddScoped(typeof(IAllocationMiddlewareHelper), typeof(AllocationMiddlewareHelper));

            services.AddScoped(typeof(IUserAccessor), typeof(UserAccessor));


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IIdentityHttpServices), typeof(IdentityHttpServices));
            services.AddHttpClient<IdentityHttpServices>();
            services.AddScoped(typeof(IAllocationHttpService), typeof(AllocationHttpService));
            services.AddHttpClient<AllocationHttpService>();

            services.AddScoped(typeof(IConfigurationHttpService), typeof(ConfigurationHttpService));
            services.AddHttpClient<ConfigurationHttpService>();
            services.AddScoped(typeof(IProjectHttpService), typeof(ProjectHttpService));
            services.AddHttpClient<ProjectHttpService>();

            services.AddSingleton<IAzureServiceBusService, AzureServiceBusService>();

            services.AddScoped(typeof(IWorkflowService), typeof(WorkflowService));
            services.AddScoped(typeof(ISkillsHttpService), typeof(SkillsHttpService));
            services.AddScoped(typeof(IWcgtHttpServices), typeof(WcgtHttpServices));


            //ApplicationInsightsServiceOptions telemetryOptions = new();
            //telemetryOptions.ConnectionString = this.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
            //telemetryOptions.EnableAdaptiveSampling = false;
            //services.AddApplicationInsightsTelemetry(telemetryOptions);

            ////uncomment to add app insight logging
            services.AddLogging(builder =>
            {
                // Only Application Insights is registered as a logger provider
                builder.AddApplicationInsights(
                    configureTelemetryConfiguration: (config) => config.ConnectionString = this.Configuration["ApplicationInsights:AppInsightConnectionString"],
                    configureApplicationInsightsLoggerOptions: (options) => { }
                )
                /*.AddFilter("CategoryText", (level) => level == LogLevel.Trace)*/;
            });

            //services.AddSingleton(typeof(ILogger), typeof(ApplicationInsightsLogger));

            //services.AddSingleton<ITelemetryInitializer,HttpContextItemsTelemetryInitializer>();
            services.AddApplicationInsightsTelemetry();
            // services.AddResponseCaching();

            services.AddHsts(options =>
            {
                //options.Preload = true;
                //options.IncludeSubDomains = true;
                //options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddHttpsRedirection(options =>
            {
                //options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                //options.HttpsPort = 443;
            });

            //// Add anti-forgery token service
            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-CSRF-TOKEN";
            //});

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder.</param>
        /// <param name="env">WebHost Environment.</param>
        /// <param name="loggerFactory">loggerFactory</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseMiddleware<ValueMiddleware>();

            //app.UseMiddleware<ErrorHandling>();
            //app.UseMiddleware<ErrorHandlingMiddleware>();


            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            //app.UseMiddleware<ActionOnRequestMiddleware>();

            //app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseWebSockets();

            var logger = loggerFactory.CreateLogger<Startup>();

            //Checking if token is present
            //TODO uncomment later
            //app.UseMiddleware<TokenAuthenticationMiddleware>();

            //app.UseMiddleware<ActionOnResponseMiddleware>();
            //await app.UseOcelot();
            app.UseHsts();
            app.UseHttpsRedirection();

            string corsPolicyConsumerHostUrl1 = Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value;

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                //context.Response.Headers.Add("Cache-Control", "no-store");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Expires", "0");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
                //context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline'");
                //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
                //default-src 'self'; script-src 'self' https://cdnjs.cloudflare.com; style-src 'self' https://maxcdn.bootstrapcdn.com
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                context.Response.Headers.Add("Access-Control-Allow-Origin", corsPolicyConsumerHostUrl1);
                //context.Response.Headers.Add("X-Frame-Options", "DENY");
                //context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                //var antiforgery = context.RequestServices.GetService<IAntiforgery>();
                //var tokens = antiforgery.GetAndStoreTokens(context);
                //context.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = false });

                await next();
            });

            app.UseStaticFiles();
            app.UseRouting();

            //app.UseMiddleware<AppLogger>();

            bool useCorsPolicyFlag = Configuration.GetSection("MicroserviceApiSettings").GetSection("UseCorsPolicyFlag").Value == "true";

            if (useCorsPolicyFlag == true)
            {
                app.UseCors(Constants.CorsPolicy);
            }
            else
            {
                app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());
            }
            app.UseAuthentication();
            //app.UseAuthorization();

            // app.UseResponseCaching();

            bool useCorsPolicyMiddleWare = Configuration.GetSection("MicroserviceApiSettings").GetSection("UseCorsPolicyMiddleWare").Value == "true";

            if (useCorsPolicyMiddleWare == true)
            {
                string[] corsPolicyConsumerHostUrl = Configuration.GetSection("MicroserviceApiSettings").GetSection("CorsPolicyConsumerHostUrl").Value?.Split(";", StringSplitOptions.TrimEntries);
                var trustedOrigins = new List<string>(corsPolicyConsumerHostUrl);
                app.UseMiddleware<CustomCorsMiddleware>(trustedOrigins);
            }

            app.UseOcelot(new AuthorizationMiddleware1(Configuration, this.ResourcePermissionMapping, logger)).Wait();

            //app.UseMiddleware<RoutePermissionMiddleware>(this.ResourcePermissionMapping);

            app.UseMiddleware<NotificatiionMiddleware>();
        }

        private static List<ResourcePermissionMapping> GetPermissionsFromJson()
        {
            using (StreamReader r = new StreamReader("RoutePermissionMapping.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<ResourcePermissionMapping>>(json);
            }
        }
    }
}