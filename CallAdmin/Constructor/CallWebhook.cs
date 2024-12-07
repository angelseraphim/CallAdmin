namespace CallAdmin.Constructor
{
    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Features;

    public class CallWebhook
    {
        public string WebhookURL { get; set; }
        public string Text { get; set; }
        public string WebhookTitle { get; set; }
        public string WebhookText { get; set; }
        public string Admins { get; set; } = null;

        public CallWebhook() { }

        public CallWebhook(string WebhookURL, string Text, string WebhookTitle, string WebhookText, string Admins = null)
        {
            this.WebhookURL = WebhookURL;
            this.Text = Text;
            this.WebhookTitle = WebhookTitle;
            this.WebhookText = WebhookText;
            this.Admins = Admins;
        }

        public void Send(Player player, string reason, List<Player> admins = null)
        {
            string Admins = string.Empty;

            if (admins != null && admins.Any() && this.Admins != null)
            {
                foreach (Player admin in admins)
                {
                    Admins += this.Admins.Replace("%nick%", admin.Nickname).Replace("%userid%", admin.UserId).Replace("%group%", admin.GroupName).Replace("%nextline%", "\n");
                }
            }

            string Text = this.Text.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason).Replace("%admin%", Admins);
            string Title = this.WebhookTitle.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason).Replace("%admin%", Admins);
            string Description = this.WebhookText.Replace("%player%", player.Nickname).Replace("%id%", player.Id.ToString()).Replace("%userid%", player.UserId).Replace("%reason%", reason).Replace("%admin%", Admins);
            Plugin.embed.SendDiscordWebhook(WebhookURL, Text, Title, Description);
        }
    }
}
