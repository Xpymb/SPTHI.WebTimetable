using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ScheduleController.GoogleSheets;
using ScheduleController.Threads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SheetsAPI.ConnectToSheetsAPI();

            Schedule.ScheduleManager.SubscribeOnUpdates();

            ThreadsController.CreateThread();
            ThreadsController.StartThreads();
            
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}