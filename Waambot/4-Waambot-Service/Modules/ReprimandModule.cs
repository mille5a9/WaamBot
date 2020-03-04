using Discord.Commands;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace _4_Waambot_Service.Modules
{
    public class ReprimandModule : ModuleBase<SocketCommandContext>
    {
        [Command("reprimand")]
        [Summary("Tags the subject and tells them how bad they have been.")]
        public async Task ReprimandAsync([Remainder] [Summary("The user's tag to reprimand")] string usertag)
        {
            await this.Context.Channel.SendMessageAsync($"{usertag}, you're reprimanded.");
        }
    }
}
