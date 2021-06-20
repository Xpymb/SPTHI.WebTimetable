using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeOpenXml;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using Grpc.Net.Client;
using ScheduleController;
using Grpc.Core;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using ConsoleApp1.Payload;
using VkApiBot.Models.VK.Keyboard;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var str = "schedule_choosedate МП-21д";

            Console.WriteLine(str.Contains("schedule_choosedate"));

            Console.ReadKey();
        }
    }
}
