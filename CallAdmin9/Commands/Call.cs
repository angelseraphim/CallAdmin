using CommandSystem;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallAdmin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class Call : ICommand
    {
        public string Command { get; } = "call";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Call administrator";
        public bool SanitizeResponse => false;
        private readonly List<Player> CoolDown = new List<Player>();
        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (CoolDown.Contains(player))
            {
                response = "You have already called the administrator";
                return false;
            }
            string Reason = string.Empty;

            foreach(string item in args)
            {
                Reason += item + " ";
            }

            if (Reason.IsEmpty())
                Reason = "None";

            List<Player> Admins = Player.List.Where(p => player != p && Plugin.plugin.Config.AdminGroups.Contains(p.GroupName)).ToList();
            CoolDown.Add(player);
            Timing.CallDelayed(Plugin.plugin.Config.Cooldown, () => CoolDown.Remove(player));
            if (Admins.Count > 0)
            {
                string Text = Plugin.plugin.Config.broadcast.Message.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", Reason);
                foreach (Player item in Admins)
                {
                    item.Broadcast(Plugin.plugin.Config.broadcast.Duration, Text);
                }
            }
            else
            {
                string Text = Plugin.plugin.Config.EmbedCon.Text.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", Reason);
                string Title = Plugin.plugin.Config.EmbedCon.WebhookTitle.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", Reason);
                string Description = Plugin.plugin.Config.EmbedCon.WebhookText.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", Reason);
                Plugin.embed.SendDiscordWebhook(Plugin.plugin.Config.EmbedCon.WebhookURL, Text, Title, Description);
            }
            response = "You have successfully called the administrator!";
            return true;
        }
    }
}
