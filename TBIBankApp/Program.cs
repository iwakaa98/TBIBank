﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TBIApp.Services.Services;

namespace TBIBankApp
{
    public class Program
    {

        public static void Main(string[] args)
        {

            GmailAPIService.GmailHope();


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
