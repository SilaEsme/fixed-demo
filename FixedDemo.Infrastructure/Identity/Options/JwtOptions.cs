namespace FixedDemo.Infrastructure.Identity.Options
{
    internal class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ValidHours { get; set; } = 1;
        public string Iss { get; set; }
    }
}
