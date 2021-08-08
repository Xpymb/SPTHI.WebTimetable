using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ScheduleController.GoogleSheets;
using ScheduleController.Threads;
using System;
using System.Collections.Generic;

namespace ScheduleController
{
    public class Program
    {
        // Arguments
        // application_name=ScheduleService API
        // collegesheetname=Колледж
        // collegesheetid=1l4y2eBdwInbG3C7hKObvwRYqduNgX9px06GL7kYK4N0
        // collegelistnames=Колледж
        // institutesheetname=Институт
        // institutesheetid=..
        // institutelistnames=Нечётная,Чётная
        // magistracysheetname=Магистратура
        // magistracysheetid=..
        // magistracylistnames=Нечётная,Чётная
        // aspiranturesheetname=Аспирантура
        // aspiranturesheetid=..
        // aspiranturelistnames=Нечётная,Чётная
        // range=A2:H
        public static void Main(string[] args)
        {
            string collegeSheetName = "";
            string instituteSheetName = "";
            string magistracySheetName = "";
            string aspirantureSheetName = "";
            
            Console.WriteLine("ScheduleController server arguments:");

            foreach (var arg in args)
            {
                var command = arg.Split(new char[] { '=' });
                
                Console.WriteLine(arg);

                switch(command[0])
                {
                    case "application_name":
                        SheetsAPI.SetApplicationName(command[1]);
                        break;

                    case "collegesheetname":
                        collegeSheetName = command[1];
                        SheetsAPI.AddNewSheet(command[1]);
                        break;

                    case "collegesheetid":
                    {
                        var sheet = SheetsAPI.GetSheetByName(collegeSheetName);

                        sheet.SetSheetId(command[1]);
                        break;
                    }

                    case "collegelistnames":
                    {
                        var sheet = SheetsAPI.GetSheetByName(collegeSheetName);

                        sheet.SetListNames(new() { command[1] });
                        break;
                    }

                    case "institutesheetname":
                        instituteSheetName = command[1];
                        SheetsAPI.AddNewSheet(command[1]);
                        break;

                    case "institutesheetid":
                    {
                        var sheet = SheetsAPI.GetSheetByName(instituteSheetName);

                        sheet.SetSheetId(command[1]);
                        break;
                    }

                    case "institutelistnames":
                    {
                        var listNames = CreateListNames(command[1]);

                        var sheet = SheetsAPI.GetSheetByName(instituteSheetName);

                        sheet.SetListNames(listNames);
                        break;
                    }

                    case "magistracysheetname":
                        magistracySheetName = command[1];
                        SheetsAPI.AddNewSheet(command[1]);
                        break;

                    case "magistracysheetid":
                    {
                        var sheet = SheetsAPI.GetSheetByName(magistracySheetName);

                        sheet.SetSheetId(command[1]);
                        break;
                    }

                    case "magistracylistnames":
                    {
                        var listNames = CreateListNames(command[1]);

                        var sheet = SheetsAPI.GetSheetByName(magistracySheetName);

                        sheet.SetListNames(listNames);
                        break;
                    }

                    case "aspiranturesheetname":
                        aspirantureSheetName = command[1];
                        SheetsAPI.AddNewSheet(command[1]);
                        break;

                    case "aspiranturesheetid":
                    {
                        var sheet = SheetsAPI.GetSheetByName(aspirantureSheetName);

                        sheet.SetSheetId(command[1]);
                        break;
                    }

                    case "aspiranturelistnames":
                    {
                        var listNames = CreateListNames(command[1]);

                        var sheet = SheetsAPI.GetSheetByName(aspirantureSheetName);

                        sheet.SetListNames(listNames);
                        break;
                    }

                    case "range":
                        SheetsAPI.SetRange(command[1]);
                        break;

                    default:
                        break;
                }
            }

            SheetsAPI.ConnectToSheetsAPI();

            Schedule.ScheduleManager.SubscribeOnUpdates();

            ThreadsController.CreateThread();
            ThreadsController.StartThreads();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static List<string> CreateListNames(string command)
        {
            var names = command.Split(new char[] { ',' });

            var listNames = new List<string>();

            foreach (var listName in names)
            {
                listNames.Add(listName);
            }

            return listNames;
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
