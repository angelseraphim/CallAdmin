namespace CallAdmin.Constructor
{
    public class EmbedCon
    {
        public string WebhookURL { get; set; }
        public string Text { get; set; }
        public string WebhookTitle { get; set; }
        public string WebhookText { get; set; }
        public EmbedCon() { }
        public EmbedCon(string WebhookURL, string Text, string WebhookTitle, string WebhookText)
        {
            this.WebhookURL = WebhookURL;
            this.Text = Text;
            this.WebhookTitle = WebhookTitle;
            this.WebhookText = WebhookText;
        }
    }
}
