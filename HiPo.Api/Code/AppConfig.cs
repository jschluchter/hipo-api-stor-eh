using System.Configuration;

namespace HiPo.Api.Code
{
    public class AppConfig
    {
        public static string PrimaryStorage
        {
            get { return ConfigurationManager.AppSettings["PrimaryStorage"]; }
        }

    }
}