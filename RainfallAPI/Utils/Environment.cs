namespace RainfallAPI.Utils
{
    public static class Environment
    {
        public static string GetFirstUrlFromLaunchSettings() 
        {
            return System.Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First();
        }
    }
}
