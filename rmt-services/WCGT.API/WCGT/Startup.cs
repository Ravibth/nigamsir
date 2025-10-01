using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WCGT.Infrastructure.Data;

namespace WCGT.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddMicrosoftIdentityWebApi(options =>
                   {
                       Configuration.Bind("AzureAd", options);
                       options.TokenValidationParameters.NameClaimType = "sub";
                       options.TokenValidationParameters.RoleClaimType = "scp";
                       options.Events = new JwtBearerEvents
                       {
                           OnAuthenticationFailed = context =>
                           {
                               Console.WriteLine(context);
                               throw new SecurityTokenValidationException("Not Authenticated");
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
                   }, options => { Configuration.Bind("AzureAd", options); });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            // services
            //.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.Authority = $"";
            //     options.Audience = "";
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,

            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //     };
            // });
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(options =>
            //    {
            //        Configuration.Bind("AzureAd", options);
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuerSigningKey = true,
            //            //IssuerSigningKey = true,
            //            ValidateAudience = true,
            //            ValidateIssuer = true,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.Zero,
            //        };
            //    }, options => { Configuration.Bind("AzureAd", options); });
            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration);
            //services.AddAuthentication();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddMicrosoftIdentityWebApi(options =>
            //{
            //    Configuration.Bind("AzureAd", options);
            //    options.Events = new JwtBearerEvents();

            //}, options => { Configuration.Bind("AzureAd", options); });
            //    services
            //.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = $"";
            //    options.Audience = "";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //    };
            //});
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration.GetValue<string>(Constants.TokenKey)));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(opt =>
            //{
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        //IssuerSigningKey = true,
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ValidateLifetime = true,
            //        //ClockSkew = TimeSpan.Zero,
            //    };
            //});
            //services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd");
            //services.AddAuthentication();

            //services.AddSession();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddMicrosoftIdentityWebApi(options =>
            //{
            //    Configuration.Bind("AzureAd", options);
            //    options.TokenValidationParameters.NameClaimType = "name";
            //}, options => { Configuration.Bind("AzureAd", options); });


            //    services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
            //.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(opt =>
            //    {
            //        opt.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            //IssuerSigningKey = true,
            //            ValidateAudience = false,
            //            ValidateIssuer = false,
            //            ValidateLifetime = true,
            //            //ClockSkew = TimeSpan.Zero,
            //        };
            //    })
            //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Migrate latest database changes during startup
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<WcgtDbContext>();

                // Here is the migration executed
                dbContext.Database.Migrate();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            //app.Use(async (context, next) =>
            //{
            //    var claims = context.User.Claims;
            //    foreach (var claim in claims)
            //    {
            //        var ab = claim;
            //    }
            //    if (!context.User.Identity?.IsAuthenticated ?? false)
            //    {
            //        context.Response.StatusCode = 401;
            //        await context.Response.WriteAsync("Not Authenticated");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
