using Microsoft.Extensions.Configuration;
using Orders.Common.Interfaces;
using Orders.Common.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Orders.Common
{
    public class WebAppConfiguration: IApplicationConfiguration
    {
        private readonly IConfiguration _configuration;
        public WebAppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            LoadConfiguration();
        }

        public string Url { get; set; }
        public string ApiKey { get; set; }

        private void LoadConfiguration()
        {
            Url = _configuration?[ConfigKeys.Url];
            ApiKey = _configuration?[ConfigKeys.ApiKey];            
        }
    }
}
