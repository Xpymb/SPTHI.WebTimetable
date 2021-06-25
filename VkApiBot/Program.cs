using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using VkApiBot.gRPC.Services;
using VkApiBot.Models;

namespace VkApiBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ScheduleServiceAPI.ConnectToService("http://77.73.67.20:5000");
            //CallControllerServiceAPI.ConnectToService("http://localhost:5002");

            var client = Bot.Get();

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(0, Int32.MaxValue) },
                { "peer_id", 207753605 },
                { "message", "Бот запущен" },
            });

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
