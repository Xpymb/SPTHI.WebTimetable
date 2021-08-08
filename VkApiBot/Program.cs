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
        /// <summary>
        /// </summary>
        /// <param name="args">
        /// schedule_service_url=http://77.73.67.20:5000 
        /// callcontroller_service_url=http://77.73.67.20:5002
        /// vk_token=dbfffc021ae9db88fdc08e36fb90e0f9b8fca5f9a65dadc50a25cf2dc11cf6e93367bc74a14aedcaeaea9
        /// vk_group_id=205071272
        /// vk_api_version=5.131
        /// vk_admin_id=207753605
        /// callback_form=https://docs.google.com/forms/d/e/1FAIpQLSdSlKu181UUls4rd9vA5xEZrDw154NAi3zEQdlw_yVq6dF1tQ/viewform
        /// </param>
        public static void Main(string[] args)
        {
            foreach(var arg in args)
            {
                var value = arg.Split(new char[] { '=' });

                switch(value[0])
                {
                    case "schedule_service_url":
                        ScheduleServiceAPI.ConnectToService(value[1]);
                        break;

                    case "callcontroller_service_url":
                        CallControllerServiceAPI.ConnectToService(value[1]);
                        break;

                    case "vk_token":
                        AppSettings.SetToken(value[1]);
                        break;

                    case "vk_group_id":
                        AppSettings.SetGroupId(value[1]);
                        break;

                    case "vk_api_version":
                        AppSettings.SetApiVersion(value[1]);
                        break;

                    case "vk_admin_id":
                        AppSettings.SetAdminId(value[1]);
                        break;

                    case "callback_form":
                        AppSettings.SetCallbackForm(value[1]);
                        break;
                }
            }

            var client = Bot.Get();

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(0, Int32.MaxValue) },
                { "peer_id", AppSettings.AdminId },
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
