namespace MovieHub.Dashboard.Models
{
    public class AuthenticationSettings
    {
        public string ActiveProvider { get; set; }
        public GoogleSettings Google { get; set; }
        public EmailPasswordSettings EmailPassword { get; set; }
        public PhoneOtpSettings PhoneOtp { get; set; }
    }

    public class GoogleSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class EmailPasswordSettings
    {
        public bool Enabled { get; set; }
    }

    public class PhoneOtpSettings
    {
        public bool Enabled { get; set; }
    }
}
