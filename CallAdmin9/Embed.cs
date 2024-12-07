namespace CallAdmin
{
    using System.Net.Http;
    using System.Text;

    using Exiled.API.Features;

    using Newtonsoft.Json;

    public class Embed
    {
        public void SendDiscordWebhook(string WebhookURL, string Text, string Title, string WebhookText)
        {
            if (WebhookURL.IsEmpty())
            {
                Log.Error("Webhook url is empty!");
                return;
            }
            if (Text.IsEmpty())
            {
                Log.Error("Text is empty!");
                return;
            }
            if (Title.IsEmpty())
            {
                Log.Error("Title is empty!");
                return;
            }
            if (WebhookText.IsEmpty())
            {
                Log.Error("WebhookText is empty!");
                return;
            }
            try
            {
                var message = new
                {
                    content = Text,
                    embeds = new[]
    {
                    new
                    {
                        color = 0xff0000,
                        title = Title,
                        description = WebhookText,
                    }
                }
                };

                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(message);

                client.PostAsync(WebhookURL, new StringContent(json, Encoding.UTF8, "application/json"));
            }
            catch
            {
                Log.Error("Webhook is wrong. Please check your configuration");
            }
        }
    }
}
