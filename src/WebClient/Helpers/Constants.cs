namespace WebClient.Helpers
{
    public static class Constants
    {
        public const string AuthenticationScheme = "Bearer";
        public const string Authority = "https://localhost:5004";
        public const string OidcResponseType = "code id_token";
        public const string WebClientId = "WebClient";
        public const string ApiScope = "WeatherApi";
        public const string OfflineScope = "offline_access";
        public const string AccessToken = "access_token";
    }
}