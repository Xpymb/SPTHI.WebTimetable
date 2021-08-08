using CallService.Calls;
using CallService.GoogleCalendar;
using CallService.Threads;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CallService
{
    public class Program
    {
        /// <summary>
        /// application_name=CallService SPHTI
        /// calendar_id=pit5gsmeoig0m329opvjh8f5o4@group.calendar.google.com
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                var value = arg.Split(new char[] { '=' });

                switch (value[0])
                {
                    case "application_name":
                        CalendarAPI.SetApplicationName(value[1]);
                        break;

                    case "calendar_id":
                        CalendarAPI.SetCalendarId(value[1]);
                        break;
                }
            }

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
