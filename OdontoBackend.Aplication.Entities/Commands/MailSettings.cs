namespace OdontoBackend.Services.Api.Configurations
{
    public class MailSettings
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? SenderNameFrom { get; set; }
        public string? SenderEmailFrom { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
    }
}
