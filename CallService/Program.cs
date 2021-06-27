using CallService.Calls;
using CallService.GoogleCalendar;
using CallService.Threads;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CallService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CalendarAPI.ConnectToApi();
            CallsManager.SubscribeToEvents();
            ThreadsController.StartThreads();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
