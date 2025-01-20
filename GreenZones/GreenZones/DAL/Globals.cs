namespace AppRateLimiter.DAL
{
    public static class Globals
    {
        public static string ReadUrl = Environment.GetEnvironmentVariable("ReadUrl") ?? string.Empty;
    }
}
