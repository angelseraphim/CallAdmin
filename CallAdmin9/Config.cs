﻿using System.Collections.Generic;
using CallAdmin.Constructor;
using Exiled.API.Interfaces;

namespace CallAdmin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int Cooldown { get; set; } = 60;
        public List<string> AdminGroups { get; set; } = new List<string>() { "owner", "admin", "mod" };
        public BroadcastCon broadcast { get; set; } = new BroadcastCon("%player% %id% %userid% called the administrator for a reason %reason%", 10);
        public EmbedCon EmbedCon { get; set; } = new EmbedCon("", "%player% %id% %userid% called the administrator", "Reason", "%reason%");
    }
}
