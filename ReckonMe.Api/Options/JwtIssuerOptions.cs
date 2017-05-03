namespace ReckonMe.Api.Options
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Audience { get; set; }
        public int ExpirationTime { get; set; }
    }
}