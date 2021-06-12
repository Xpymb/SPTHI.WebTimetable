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

        public static VkApi Get()
        {
            if(_client != null)
            {
                return _client;
            }

            _listCommands = new List<Command>
            {
                new HomeCommand(),
                new HelloCommand(),
                new StartCommand(),
                new AboutInstituteCommand(),
                new AboutBotCommand(),
            };

            _client = new VkApi();

            _client.Authorize(new ApiAuthParams()
            {
                AccessToken = AppSettings.Token,
                Settings = Settings.Messages,
            });

            return _client;
        }
    }
}