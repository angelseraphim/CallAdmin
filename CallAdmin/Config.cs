using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallAdmin.Constructor;
using Exiled.API.Interfaces;

namespace CallAdmin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public List<string> AdminGroups = new List<string>() { "owner", "admin", "mod" };
        public BroadcastCon broadcast { get; set; } = new BroadcastCon("%player% %id% %userid% called the administrator for a reason %reason%", 10);
        public EmbedCon EmbedCon { get; set; } = new EmbedCon("", "%player% %id% %userid% called the administrator", "Reason", "%reason%");
    }
}
