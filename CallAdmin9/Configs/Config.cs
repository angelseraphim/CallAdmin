namespace CallAdmin.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using CallAdmin.Constructor;

    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Command names")]
        public List<string> CommandAliases { get; set; } = new List<string>() { "admin", "админ", "help_pls"};

        [Description("Allow calling the administrator if the reason is empty")]
        public bool AllowEmpty { get; set; } = false;

        [Description("Cooldown (In seconds)")]
        public int Cooldown { get; set; } = 60;

        [Description("Administration groups that will be able to see the call")]
        public List<string> AdminGroups { get; set; } = new List<string>() { "owner", "admin", "mod" };

        [Description("Broadcast that will appear to administrators if they are on the server")]
        public CallBroadcast Broadcast { get; set; } = new CallBroadcast("%player% %id% %userid% called the administrator for a reason %reason%", 10);

        [Description("Webhook that will be sent if there are admins on the server")]
        public CallWebhook OnlineWebhook { get; set; } = new CallWebhook("", "%player% %id% %userid% called the administrator\nAdmins who are on the server: %admin%", "Reason", "%reason%", "%nick% (%userid%) [%group%] %nextline%");

        [Description("Webhook that will be sent if there are no administrators on the server")]
        public CallWebhook OfflineWebhook { get; set; } = new CallWebhook("", "%player% %id% %userid% called the administrator", "Reason", "%reason%");
    }
}
