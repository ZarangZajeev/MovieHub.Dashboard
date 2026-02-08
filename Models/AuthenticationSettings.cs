namespace MovieHub.Dashboard.Models
{
    public class AuthenticationSettings
    {
        public string ActiveProvider { get; set; }
        public GoogleSettings Google { get; set; }
        public EmailPasswordSettings EmailPassword { get; set; }
        public PhoneOtpSettings PhoneOtp { get; set; }
        public ApiProperties APIRpoperties { get; set; }
    }

    public class GoogleSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class ApiProperties
    {
        public string BaseUrl { get; set; }
        public EndpointSettings Endpoints { get; set; }
    }
    public class EndpointSettings
    {
        public GoogleEndpoint Google { get; set; }
    }
    public class GoogleEndpoint
    {
        public string Login { get; set; }
        public string Register { get; set; }
    }

    public class EmailPasswordSettings
    {
        public bool Enabled { get; set; }
    }

    public class PhoneOtpSettings
    {
        public bool Enabled { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
