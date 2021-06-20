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
            using (var stream = new StreamReader("json1.json"))
            {
                var json = stream.ReadToEnd();

                var deser = JsonSerializer.Deserialize<Updates>(json);

                Console.WriteLine(deser.Object.Message.payload);

                var deserpayload = JsonSerializer.Deserialize<ButtonPayloadClass>(deser.Object.Message.payload);

                Console.WriteLine(deserpayload.Button);
            }
                

            Console.ReadKey();
        }
    }
}
