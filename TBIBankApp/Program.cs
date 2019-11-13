using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;
using Serilog;

namespace TBIBankApp
{
    public class Program
    {
        

        public static void Main(string[] args)
        {



            CreateWebHostBuilder(args).Build().Run();
        }
       


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
