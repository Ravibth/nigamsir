// <copyright file="Program.cs" company="RMT">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;

    /// <summary>
    /// Program class file is a place where we create a host for the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  It is the entry point of our app.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates Host Builder.
        /// </summary>
        /// <param name="args">arguments.</param>
        /// <returns>Host Builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 100;
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60);
                });
            })
            .ConfigureAppConfiguration((host, config) =>
            {
                config.AddJsonFile($"configuration.{host.HostingEnvironment.EnvironmentName}.json", true);
            });

    }
}