namespace CallAdmin
{
    using CallAdmin.Configs;

    using Exiled.API.Features;

    public class Plugin : Plugin<Config, Translation>
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
