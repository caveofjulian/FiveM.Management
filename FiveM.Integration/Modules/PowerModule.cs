using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FiveM.Integration.Helpers;

namespace FiveM.Integration.Modules
{
   
    public class PowerModule : ModuleBase<SocketCommandContext>
    {
        public bool IsRunning;
        public bool IsChanging;
        
        public const string ARTIFACTS_FOLDER = "/srv/fivem/files/server";
        public const string DATA_FOLDER = "/srv/fivem/files/cfx-server-data";

        [Command("Restart")]
        public async Task RestartServer()
        {
            await ReplyAsync(StopServer());
            await ReplyAsync(StartServer());
        }

        [Command("Start")]
        public async Task StartServerAsync()
        {
            if (IsRunning)
            {
                await ReplyAsync("Don't try to fuck with the VM, it's already running. If you're SURE it isn't then " +
                                 "there's something wrong and you have to contact Julian, but don't do this unless u spent 10 hours checking.");
            }
            else
            {
                await ReplyAsync(StartServer());
            }
        }

        [Command("Stop")]
        public async Task StopServerAsync()
        {
            if (!IsRunning)
            {
                await ReplyAsync("Don't try to fuck with the VM, it's already offline. If you're SURE it isn't then " +
                                 "there's something wrong and you have to contact Julian, but don't do this unless u spent 10 hours checking.");
            }
            else
            {
                await ReplyAsync(StopServer());
            }
        }

        private string StartServer()
        {
            if (IsChanging)
            {
                return "Don't spam to start/stop this could have caused a lot of issues if i was retarded!";
            }

            IsChanging = true;
            var result = BashHelper.Execute($"bash {ARTIFACTS_FOLDER}/run.sh +exec {DATA_FOLDER}/server.cfg");
            IsChanging = false;
            return result;
        }

        private string StopServer()
        {
            if (IsChanging)
            {
                return "Don't spam to start/stop this could have caused a lot of issues if i was retarded!";
            }

            IsChanging = true;
            var result =  BashHelper.Execute("pkill -f /srv/fivem/");
            IsChanging = false;
            return result;
        }
    }
}
