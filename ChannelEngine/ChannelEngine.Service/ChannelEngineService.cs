﻿using ChanelEngine.Service.Common.Models;
using ChanelEngine.Service.Interfaces;
using Newtonsoft.Json;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<OrdersInfo>> GetInProgressOrdersAsync()
        {
            var orders = await GetOrdersAsync();
            return orders.Content.Where(x=> x.Status.Equals("IN_PROGRESS"));
        }
        
        private async Task<OrdersInfoPm> GetOrdersAsync()
        {
            var endpoint = _applicationConfiguration.Url + "orders?apikey=" + _applicationConfiguration.ApiKey;
            var result = await _channelEngineWebClient.GetAsync(endpoint);
            return JsonConvert.DeserializeObject<OrdersInfoPm>(result);
        }
    }
}
