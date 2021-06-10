using System;
using VkNet;
using VkNet.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new VkApi();

            client.Authorize(new ApiAuthParams
            {
                AccessToken = "dbfffc021ae9db88fdc08e36fb90e0f9b8fca5f9a65dadc50a25cf2dc11cf6e93367bc74a14aedcaeaea9",
            });

            client.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                PeerId = 658187118,
                RandomId = new Random().Next(Int32.MinValue, Int32.MaxValue),
                Message = "Приветули"
            });

            Console.ReadKey();
        }
    }
}
