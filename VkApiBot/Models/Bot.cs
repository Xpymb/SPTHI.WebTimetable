using System.Collections.Generic;
using System.Threading.Tasks;
using VkApiBot.Models.Commands;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkApiBot.Models
{
    public static class Bot
    {
        private static VkApi _client;

        private static List<Command> _listCommands;

        public static IReadOnlyList<Command> Commands { get => _listCommands.AsReadOnly(); }

        public static async Task<VkApi> Get()
        {
            if(_client != null)
            {
                return _client;
            }

            _listCommands = new List<Command>
            {
                new HelloCommand()
            };

            _client = new VkApi();

            await _client.AuthorizeAsync(new ApiAuthParams()
            {
                ApplicationId = 2685278,
                Login = AppSettings.Login,
                Password = AppSettings.Password,
                Settings = Settings.All,
                AccessToken = AppSettings.Token
            });

            return _client;
        }
    }
}