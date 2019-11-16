using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace TBIBankApp
{
    public class Program
    {


        public static void Main(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            
            //    Log.Information("Application starting Up");
                CreateWebHostBuilder(args).Build().Run();

            
        }



        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
