using Orders.Business.Interfaces;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System.Threading.Tasks;

namespace Orders.Business.RequestHandlers
{
    public class OrdersLoadRequestHandler : IRequestHandler<EmptyRequest, OrdersLoadResponse>
    {
        private readonly IChannelEngineService _channelEngineService;
        public OrdersLoadRequestHandler(IChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }
        public async Task<OrdersLoadResponse> ProcessRequest(EmptyRequest request)
        {
            var response = new OrdersLoadResponse();

            var Orders = await _channelEngineService.GetOrdersAsync();

            return response;
        }
    }
}
