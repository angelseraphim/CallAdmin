using Exiled.API.Features;

namespace CallAdmin
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "CallAdmin";
        public override string Name => "CallAdmin";
        public override string Author => "angelseraphim.";
        public static Plugin plugin;
        public static Embed embed;
        public override void OnEnabled()
        {
            plugin = this;
            embed = new Embed();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            embed = null;
            base.OnDisabled();
        }
    }
}
