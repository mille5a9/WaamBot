using Discord.Commands;
using System.Threading.Tasks;

namespace _4_Waambot_Service.Modules
{
    public class ReprimandModule : ModuleBase<SocketCommandContext>
    {
        [Command("reprimand")]
        [Summary("Tags the subject and tells them how bad they have been.")]
        public async Task ReprimandAsync([Summary("The user's tag to reprimand")] string usertag, [Remainder][Summary("Optional Reason for Reprimanding")] string reasoning = "")
        {
            if (string.IsNullOrEmpty(reasoning))
            {
                await this.Context.Channel.SendMessageAsync($"{usertag}, you're reprimanded.");
            }
            else
            {
                await this.Context.Channel.SendMessageAsync($"{usertag}, you're reprimanded because {reasoning}");
            }
        }
    }
}
