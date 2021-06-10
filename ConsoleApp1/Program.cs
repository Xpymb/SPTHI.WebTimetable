using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VkNet;
using VkNet.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        { 
            var obj = JsonConvert.DeserializeObject<Updates>(File.ReadAllText("json1.json"));

            var text = obj.Object.Message.Text.ToString();

            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
