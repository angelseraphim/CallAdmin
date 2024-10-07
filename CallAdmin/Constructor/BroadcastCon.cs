using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallAdmin.Constructor
{
    public class BroadcastCon
    {
        public string Message { get; set; }
        public ushort Duration { get; set; }    
        public BroadcastCon() { }
        public BroadcastCon(string Message, ushort Duration)
        {
            this.Duration = Duration;
            this.Message = Message;
        }
    }
}
