using System;
using System.Diagnostics;
using System.IO;

using Microsoft.Extensions.Caching.Memory;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Json;
using Nancy.TinyIoc;

using Serilog;

namespace C.Teleport.AirportDistanceCalculator
{
    public class CustomBootStrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Json(serializeEnumToString: true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<ILogger>(
                new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Seq("http://localhost:5341")
                    .WriteTo.File(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"calculator-{Date}.log"), outputTemplate: "{Timestamp} [{Level}] ({Name}){NewLine} {Message}{NewLine}{Exception}", fileSizeLimitBytes: 20971520, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, rollingInterval: RollingInterval.Day
                    )
                    .CreateLogger()
            );

            container.Register<IMemoryCache>(
                new MemoryCache(new MemoryCacheOptions())
            );

            base.ConfigureApplicationContainer(container);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            ILogger logger = container.Resolve<ILogger>();

            Stopwatch stopwatch = new Stopwatch();

            pipelines.BeforeRequest += c =>
            {
                stopwatch.Start();

                logger.Debug("Starting request for Url:{@Url}.", c.Request.Url.ToString());

                return null;
            };

            pipelines.AfterRequest += c =>
            {
                stopwatch.Stop();

                logger.Debug("Completed request for Url:{@Url}.  Elapsed: {Elapsed}.", c.Request.Url.ToString(), stopwatch.Elapsed);
            };

            pipelines.OnError += (c, e) =>
            {
                stopwatch.Stop();

                logger.Error("Error on request {@Url}: {Message}. Elapsed: {Elapsed}.", c.Request.Url.ToString(), e.Message, stopwatch.Elapsed);

                return c.Response;
            };
        }
    }
}
