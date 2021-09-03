using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLogT01
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    Log.Logger = new LoggerConfiguration()
        //        .ReadFrom.Configuration(configuration)
        //        .Enrich.FromLogContext()
        //        .CreateLogger();

        //    try
        //    {
        //        Log.Information("Application Starting Up");
        //        CreateHostBuilder(args).Build().Run();
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Fatal(ex, "The Application failed to start");
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}

        public static void Main(string[] args)
        {
            const string logTemplate = @"{Timestamp:G} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.File("./App_Data/logs/OtherLog.txt", LogEventLevel.Information, logTemplate,
                        rollingInterval: RollingInterval.Day
                    );
                    l.Filter.ByExcluding(e => e.Properties.ContainsKey("secondLog"));
                    l.Filter.ByExcluding(e => e.Properties.ContainsKey("firstLog"));
                })
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.File("./App_Data/logs/firstLog.txt", LogEventLevel.Information, logTemplate,
                        rollingInterval: RollingInterval.Day
                    );
                    l.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("firstLog"));
                })
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.File("./App_Data/logs/secondLog.txt", LogEventLevel.Information, logTemplate,
                        rollingInterval: RollingInterval.Day
                    );
                    l.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("secondLog"));
                });
            Log.Logger = loggerConfig.CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {                   
                    Log.Information($"Application [{hostContext.HostingEnvironment.ApplicationName}] Starts****. ");

                    services.AddHostedService<Log01>();
                    services.AddSingleton<ILog02, Log02>();
                })
                .UseSerilog();
    }

    //public static IHostBuilder CreateHostBuilder(string[] args)
    //{
    //    return Host.CreateDefaultBuilder(args)
    //      .UseSerilog();
    //        .ConfigureWebHostDefaults((hostContext, services) =>
    //        {
    //            //webBuilder.UseStartup<Startup>();
    //        });
    //}

}
