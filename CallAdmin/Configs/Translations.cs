namespace CallAdmin.Configs
{
    using Exiled.API.Interfaces;

    public class Translation : ITranslation
    {
        public string CommandDescription { get; set; } = "Call administrator";
        public string Cooldown { get; set; } = "You have already called the administrator";
        public string EmptyReason { get; set; } = "Please indicate the reason";
        public string Successfull { get; set; } = "You have successfully called the administrator!";
    }
}
