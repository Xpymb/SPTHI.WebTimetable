using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using VkApiBot.Models;

namespace VkApiBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
