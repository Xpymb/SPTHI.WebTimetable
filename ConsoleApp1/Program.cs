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
            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new ScheduleAPI.ScheduleAPIClient(channel);

            var reply = client.GetDateScheduleByGroupName(new DateScheduleRequest
            {
                GroupName = "ТМО-21д",
            });

            await foreach(var response in reply.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"{response.GroupName} {response.Date}");
            }

            Console.ReadKey();
        }
    }
}
