namespace CallAdmin.Constructor
{
    using System.Collections.Generic;

    using Exiled.API.Features;

    public class CallBroadcast
    {
        public string Message { get; set; }
        public ushort Duration { get; set; }    

        public CallBroadcast() { }

        public CallBroadcast(string Message, ushort Duration)
        {
            this.Duration = Duration;
            this.Message = Message;
        }

        public void Send(Player player, string reason, List<Player> admins)
        {
            string Text = Plugin.plugin.Config.Broadcast.Message.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason);

            foreach (Player admin in admins)
            {
                admin.Broadcast(this.Duration, Text);
            }
        }
    }
}
