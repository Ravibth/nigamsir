using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.API
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
           // services.AddApplicationInsightsTelemetry();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddMicrosoftIdentityWebApi(options =>
            //       {
            //           Configuration.Bind("AzureAd", options);
            //           options.TokenValidationParameters.NameClaimType = "sub";
            //           options.TokenValidationParameters.RoleClaimType = "scp";
            //           options.Events = new JwtBearerEvents
            //           {
            //               OnAuthenticationFailed = context =>
            //               {
            //                   Console.WriteLine(context);
            //                   throw new SecurityTokenValidationException("Not Authenticated");
            //               },
            //               OnTokenValidated = context =>
            //               {
            //                   if (context.Principal.Identity is ClaimsIdentity identity)
            //                   {
            //                       var scpClaims = context.Principal.Claims.ToList();
            //                       foreach (var scpClaim in scpClaims)
            //                       {
            //                           identity.AddClaim(new Claim(ClaimTypes.Role, scpClaim.Value));
            //                       }
            //                   }
            //                   return Task.CompletedTask;
            //               }
            //           };
            //       }, options => { Configuration.Bind("AzureAd", options); });

            services.AddControllersWithViews()
             .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Migrate latest database changes during startup
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<SkillDbContext>();

       
                dbContext.Database.Migrate();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
