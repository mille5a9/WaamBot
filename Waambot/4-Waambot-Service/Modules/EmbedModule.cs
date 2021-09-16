using Discord.Commands;
using AngleSharp;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
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
            // 0 remove possible <> decorations suppressing embed
            link = link.Replace(">", string.Empty);
            link = link.Replace("<", string.Empty);

            // 1 visit reddit thread
            var config = AngleSharp.Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(link);
            var cellSelector = "a[href*=\"i.redd.it\"]";
            var cells = document.QuerySelectorAll(cellSelector);
            Console.WriteLine("Line 28");
            Console.WriteLine(cells.Length);

            // 2 analyze content of reddit post (.jpg, text post, etc)
            foreach (var node in cells) {
                Console.WriteLine("Line 33");
                Console.WriteLine(node.GetAttribute("href"));
                await this.Context.Channel.SendMessageAsync(node.GetAttribute("href"));
            }

            // 3 construct sensible embed based on results from step 2

        }
    }
}
