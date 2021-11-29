using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace IdentityServer
{
    public class Program
    {
        private static string AppName = typeof(Program).Namespace;
        public static void Main(string[] args)
        {
            /*var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);*/
            CreateHostBuilder(args).Build().Run();
            /*try
            {
                Log.Information("Configuring web host", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Starting web host...", AppName);
                host.Run();
                return 0;

            }catch(Exception e)
            {
                Log.Fatal(e, "Program crash", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }*/
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .UseSerilog()
                .Build();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var logstahUrl = configuration["Serilog:LogstashUrl"];
            var cfg = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console();
            if (!string.IsNullOrWhiteSpace(logstahUrl))
            {
                cfg.WriteTo.Http(logstahUrl);
            }
            return cfg.CreateLogger();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
