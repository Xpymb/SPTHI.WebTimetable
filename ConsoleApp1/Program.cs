
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeOpenXml;
using ConsoleApp1.VkKeyboard;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Parse(DateTime.Now.Date.ToShortDateString()));
        }
    }
}
