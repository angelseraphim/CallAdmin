namespace CallAdmin.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CommandSystem;

    using Exiled.API.Features;

    using MEC;

    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Call : ICommand
    {
        public string Command { get; } = "call";
        public string[] Aliases { get; } = Plugin.plugin.Config.CommandAliases.ToArray();
        public string Description { get; } = Plugin.plugin.Translation.CommandDescription;
        public bool SanitizeResponse { get; } = false;

        private readonly List<Player> сoolDown = new List<Player>();

        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (сoolDown.Contains(player))
            {
                response = Plugin.plugin.Translation.Cooldown;
                return false;
            }

            string reason = string.Empty;

            if (args.Any())
            {
                foreach (string item in args)
                {
                    reason += item + " ";
                }
            }
            else if (!Plugin.plugin.Config.AllowEmpty)
            {
                response = Plugin.plugin.Translation.EmptyReason;
                return false;
            }
            reason = "None";

            List<Player> Admins = Player.List.Where(p => player != p && Plugin.plugin.Config.AdminGroups.Contains(p.GroupName)).ToList();

            if (Admins.Any())
            {
                Plugin.plugin.Config.Broadcast.Send(player, reason, Admins);
                Plugin.plugin.Config.OnlineWebhook.Send(player, reason, Admins);
            }
            else
            {
                Plugin.plugin.Config.OfflineWebhook.Send(player, reason);
            }

            сoolDown.Add(player);
            Timing.CallDelayed(Plugin.plugin.Config.Cooldown, () => сoolDown.Remove(player));

            response = Plugin.plugin.Translation.Successfull;
            return true;
        }
    }
}
