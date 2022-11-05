using Orders.Common.Interfaces;
using Orders.Common.Types;
using System.Configuration;

namespace Orders.Common
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            LoadConfiguration();
        }

        public string Url { get; set; }
        public string ApiKey { get; set; }

        private void LoadConfiguration()
        {
            Url = ConfigurationManager.AppSettings[ConfigKeys.Url];
            ApiKey = ConfigurationManager.AppSettings[ConfigKeys.ApiKey];
        }
    }
}
