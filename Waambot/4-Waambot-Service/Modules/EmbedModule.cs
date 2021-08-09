using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Discord;

namespace _4_Waambot_Service.Modules
{
    public class EmbedModule : ModuleBase<SocketCommandContext>
    {
        [Command("reddit")]
        [Summary("Analyzes a reddit link and provides a sensible embed")]
        public async Task RedditAsync([Summary("reddit hyperlink")] string link)
        {
        }
    }
}
