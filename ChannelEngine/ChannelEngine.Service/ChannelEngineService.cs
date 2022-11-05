using ChanelEngine.Service.Common.Models;
using ChanelEngine.Service.Interfaces;
using Newtonsoft.Json;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Common.Interfaces;
using System.Threading.Tasks;

namespace Orders.ChanelEngine.Service
{
    public class ChannelEngineService : IChannelEngineService
    {
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IChannelEngineWebClient _channelEngineWebClient;

        public ChannelEngineService(IApplicationConfiguration applicationConfiguration,
            IChannelEngineWebClient channelEngineWebClient)
        {
            _applicationConfiguration = applicationConfiguration;
            _channelEngineWebClient = channelEngineWebClient;
        }

        public async Task<Root> GetOrdersAsync()
        {
            var endpoint = _applicationConfiguration.Url + "/orders?" + _applicationConfiguration.ApiKey;
            var result = await _channelEngineWebClient.GetAsync(endpoint);
            return JsonConvert.DeserializeObject<Root>(result);
        }
    }
}
