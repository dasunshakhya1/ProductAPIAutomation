namespace ProductAPIAutomation.Core.Configs
{
    public class ApplicationConfigs
    {
        public static readonly string BASEURL = Environment.GetEnvironmentVariable("BASE_URL") ?? "https://api.restful-api.dev";
       
    }
}
