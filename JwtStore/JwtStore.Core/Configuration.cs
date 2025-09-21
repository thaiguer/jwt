namespace JwtStore.Core;

public static class Configuration
{
    public static class SecretsConfiguration
    {
        public static string ApiKey { get; set; } = string.Empty;
        public static string JwtPrivateKey { get; set; } = string.Empty;
        public static string PasswordSaltKey { get; set; } = string.Empty;
    }
}