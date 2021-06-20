using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CallController;
using CallControllerService.CallController.Ticks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CallControllerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var instancesContainer = new InstancesContainer();
            
            InstancesContainer._TicksController.TickEvery1Sec.Invoke(instancesContainer, new TicksController.DefaultEventArgs());
            InstancesContainer._TicksController.TickEvery5Min.Invoke(instancesContainer, new TicksController.DefaultEventArgs());
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => 
                {
                    webBuilder.UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Loopback, 5002);
                    });

                    webBuilder.UseStartup<Startup>(); 
                });
    }
}