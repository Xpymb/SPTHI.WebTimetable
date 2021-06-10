
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = JsonSerializer.Deserialize<Updates>(File.ReadAllText("json1.json"));



            Console.WriteLine(obj.Object.Message.FromId);

            Console.ReadKey();
        }
    }
}
