using Discord;
using Discord.WebSocket;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace _4_Waambot_Service
{
    class Program
    {
        // adapted from https://docs.stillu.cc/guides/getting_started/first-bot.html
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            client.Log += Log;
            var token = ConfigurationManager.AppSettings["MyToken"];

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // wait forever so bot doesn't close itself
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
