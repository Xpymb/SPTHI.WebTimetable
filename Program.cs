using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VkApiBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    { 
                        options.Listen(IPAddress.Parse(args[0]), 80);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
