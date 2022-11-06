using Orders.Business.Interfaces;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System.Linq;
using System.Threading.Tasks;
using DomainModels = Orders.Contracts.DomainModels;

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
            int serialNumber = 1;

            var inProgressOrders = await _channelEngineService.GetInProgressOrdersAsync();

            if (inProgressOrders == null)
            {
                return new OrdersLoadResponse
                {
                    IsSucess = false,
                    Message = "Failed to fetch Orders"                    
                };
            }

            if (inProgressOrders != null && !inProgressOrders.Any())
            {
                return new OrdersLoadResponse
                {
                    IsSucess = true,
                    Message = "No inprogress orders found"
                };
            }

            var lines = inProgressOrders
                .SelectMany(s => s.Lines)
                .OrderBy(x => (x.Quantity + x.CancellationRequestedQuantity))
                .Take(5);
            
            return new OrdersLoadResponse
            {
                Orders = lines.Select(x => new DomainModels.OrdersInfo
                {
                    SerialNumber = serialNumber++,
                    ProductName = x.Description,
                    GtIn = x.Gtin,
                    Quantity = x.Quantity
                }),
                IsSucess = true,
                Message = "Records fetched successfully"
            };
        }
    }
}
