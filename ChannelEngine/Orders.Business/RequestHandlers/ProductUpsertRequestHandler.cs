using Orders.Business.Interfaces;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System.Threading.Tasks;

namespace Orders.Business.RequestHandlers
{
    public class ProductUpsertRequestHandler : IRequestHandler<ProductUpsertRequest, ProductUpsertResponse>
    {
        private readonly IChannelEngineService _channelEngineService;
        public ProductUpsertRequestHandler(IChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }

        public async Task<ProductUpsertResponse> ProcessRequest(ProductUpsertRequest request)
        {
            var response = await _channelEngineService.UpsertProduct(request);

            return response;
        }
    }
}
