using System.Configuration;

namespace HiPo.Api.Code
{
    public class AppConfig
    {
        public static string PrimaryStorage
        {
            get { return ConfigurationManager.AppSettings["PrimaryStorage"]; }
        }

        public static string RedisConnection
        {
            get { return ConfigurationManager.AppSettings["RedisConnection"]; }
        }

        public static string EventHubConnection
        {
            get { return ConfigurationManager.AppSettings["EventHubConnection"]; }
        }
        public static string EventHubName
        {
            get { return ConfigurationManager.AppSettings["EventHubName"]; }
        }

    }
}