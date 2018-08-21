using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace anonbot
{
    class Program
    {
        public static void Main(string[] args)
             => new Program().MainAsync().GetAwaiter().GetResult();

        DiscordSocketClient client;
        string token = "";
        ulong guildId = 0;
        ulong textChannel = 0;

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, token);
            client.MessageReceived += MessageReceived;
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Channel is IDMChannel)
            {
                await client.GetGuild(guildId).GetTextChannel(textChannel).SendMessageAsync(message.Content);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
