using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Discord;

namespace _4_Waambot_Service.Modules
{
    public class ReprimandModule : ModuleBase<SocketCommandContext>
    {

        [Command("reprimand")]
        [Summary("Tags the subject and tells them how bad they have been.")]
        public async Task ReprimandAsync([Summary("The user's tag to reprimand")] string usertag, [Remainder][Summary("Optional Reason for Reprimanding")] string reasoning = "")
        {
            Console.WriteLine(usertag);

            // load reprimand log and add 1 reprimand to the reprimanded user
            List<Tuple<string, int>> replog = LoadReprimandLog();
            if (replog.Count() == 0) {
                replog.Add(new Tuple<string, int>(usertag, 1));
            }
            else {
                Tuple<string, int> item = replog.Find(x => x.Item1 == usertag);
                if (item == null) {
                    replog.Add(new Tuple<string, int>(usertag, 1));
                }
                else {
                    int index = replog.IndexOf(item);
                    replog[index] = new Tuple<string, int>(item.Item1, item.Item2 + 1);
                }
            }
            SaveReprimandLog(replog);


            if (string.IsNullOrEmpty(reasoning))
            {
                await this.Context.Channel.SendMessageAsync($"{usertag}, you're reprimanded.");
            }
            else
            {
                await this.Context.Channel.SendMessageAsync($"{usertag}, you're reprimanded because {reasoning}");
            }
        }

        [Command("reprimandlog")]
        [Summary("Lists total reprimands of all users, or searches a particular user.")]
        public async Task ReprimandLogAsync([Remainder][Summary("Tag of reprimanded user to search for")] string usertag = "") {
            if (usertag == "") {
                List<Tuple<string, int>> replog = LoadReprimandLog();
                String message = "Reprimand Log:\n";
                foreach (Tuple<string, int> x in replog) {
                    message += x.Item1;
                    message += ": ";
                    message += x.Item2;
                    message += " times.\n";
                }
                var builder = new EmbedBuilder() {
                    Description = message
                };
                await this.Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            else {
                List<Tuple<string, int>> replog = LoadReprimandLog();
                Tuple<string, int> instance = replog.Find(x => {
                    Console.WriteLine(x.Item1);
                    Console.WriteLine(usertag);
                    return x.Item1 == usertag;

                });
                await this.Context.Channel.SendMessageAsync($"{instance.Item1} has been reprimanded {instance.Item2} times.");
            }
        }


        private List<Tuple<string, int>> LoadReprimandLog() {
            List<Tuple<string, int>> replog = new List<Tuple<string, int>>();
            StreamReader file;

            try {
                file = new StreamReader(@"ReprimandLog.txt");
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine(ex.Message);
                return new List<Tuple<string, int>>();
            }

            String line = "";

            // Convert log contents to list of Tuples
            while ((line = file.ReadLine()) != null) {
                String[] segments = line.Split(' ');
                int quantity = Int32.Parse(segments[segments.Count() - 1]);
                List<String> segmentslist = segments.Where(x => x != segments.Last()).ToList();
                replog.Add(new Tuple<string, int>(String.Join(' ', segmentslist), quantity));
            }
            return replog;
        }

        private void SaveReprimandLog(List<Tuple<string, int>> replog) {
            StreamWriter file = new StreamWriter(@"ReprimandLog.txt", false);
            String line = "";

            // Add each object of replog as a new line in the log
            foreach (Tuple<string, int> x in replog) {
                line = x.Item1 + " " + x.Item2.ToString();
                file.WriteLine(line);
            }
            
            file.Flush();
        }
    }
}
