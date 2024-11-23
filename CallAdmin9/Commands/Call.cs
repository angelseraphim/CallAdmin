using CentralAuth;
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
            string reason = string.Empty;

            foreach (string item in args)
            {
                reason += item + " ";
            }

            if (reason.IsEmpty())
                reason = "None";

            Player[] Admins = Player.List.Where(p => player != p && Plugin.plugin.Config.AdminGroups.Contains(p.GroupName)).ToArray();
            CoolDown.Add(player);
            Timing.CallDelayed(Plugin.plugin.Config.Cooldown, () => CoolDown.Remove(player));
            if (Admins.Count() > 0)
            {
                string Text = Plugin.plugin.Config.broadcast.Message.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason);
                string content = "0!<align=center><color=red><u>Admin call</u></color></align>\n<color=yellow>Reporter:</color>\n" + player.DisplayNickname + " (" + player.UserId + ")\n<color=yellow>Reason:</color>\n" + reason;

                foreach (Player admin in Admins)
                {
                    admin.Broadcast(Plugin.plugin.Config.broadcast.Duration, Text);

                    ReferenceHub hub = admin.ReferenceHub;
                    ClientInstanceMode mode = hub.Mode;
                    if (mode != 0 && mode != ClientInstanceMode.DedicatedServer && hub.serverRoles.AdminChatPerms)
                    {
                        hub.encryptedChannelManager.TrySendMessageToClient(content, EncryptedChannelManager.EncryptedChannel.AdminChat);
                    }
                }
            }
            else
            {
                string Text = Plugin.plugin.Config.EmbedCon.Text.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason);
                string Title = Plugin.plugin.Config.EmbedCon.WebhookTitle.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason);
                string Description = Plugin.plugin.Config.EmbedCon.WebhookText.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason);
                Plugin.embed.SendDiscordWebhook(Plugin.plugin.Config.EmbedCon.WebhookURL, Text, Title, Description);
            }
            response = "You have successfully called the administrator!";
            return true;
        }
    }
}
