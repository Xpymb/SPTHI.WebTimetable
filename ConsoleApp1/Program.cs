using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new VkApi();

            client.Authorize(new ApiAuthParams()
            {
                AccessToken = "dbfffc021ae9db88fdc08e36fb90e0f9b8fca5f9a65dadc50a25cf2dc11cf6e93367bc74a14aedcaeaea9",
                Settings = Settings.Messages,
            });

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(0, Int32.MaxValue) },
                { "peer_id", 658187118 },
                { "message", "Приветули" },
                //{ "v", "5.130" }
            });
            Console.ReadKey();
        }
    }
}
