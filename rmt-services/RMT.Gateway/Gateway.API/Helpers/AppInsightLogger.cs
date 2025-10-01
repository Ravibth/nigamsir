// <copyright file="AwsLogger.cs" company="RMT">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API.Helpers
{
    using System;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ApplicationInsight Logger.
    /// </summary>
    /// <typeparam name="TCategoryName">CategoryName.</typeparam>
    public class AppInsightLogger<TCategoryName>
    {
        //private static readonly TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        //private readonly TelemetryClient telemetryClient = new TelemetryClient(telemetryConfiguration);

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightLogger{TCategoryName}"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        public AppInsightLogger(IConfiguration configuration, ILogger _logger)
        {
            this.logger = _logger;
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Log Level.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            //telemetryClient.TrackTrace(message, SeverityLevel.Warning);
            this.logger.Log(logLevel, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Log Level.</param>
        /// <param name="ex">Exception.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public void Log(LogLevel logLevel, Exception ex, string message, params object[] args)
        {
            //telemetryClient.TrackTrace(message, SeverityLevel.Warning);
            this.logger.Log(logLevel, ex, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Log Level.</param>
        /// <param name="eventId">Event Id.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public void Log(LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            //telemetryClient.TrackTrace(message, SeverityLevel.Warning);
            this.logger.Log(logLevel, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Log Level.</param>
        /// <param name="eventId">event Id.</param>
        /// <param name="ex">Exception.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public void Log(LogLevel logLevel, EventId eventId, Exception ex, string message, params object[] args)
        {
            //telemetryClient.TrackTrace(message, SeverityLevel.Warning);
            this.logger.Log(logLevel, eventId, ex, message, args);
        }
    }
}
