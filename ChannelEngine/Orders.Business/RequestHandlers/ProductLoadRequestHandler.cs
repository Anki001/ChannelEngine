using Orders.Business.Interfaces;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System;
using System.Threading.Tasks;

namespace Orders.Business.RequestHandlers
{
    public class ProductLoadRequestHandler : IRequestHandler<ProductLoadRequest, ProductLoadResponse>
    {
        private readonly IChannelEngineService _channelEngineService;
        public ProductLoadRequestHandler(IChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }
        
        public async Task<ProductLoadResponse> ProcessRequest(ProductLoadRequest request)
        {
            var response = await _channelEngineService.GetProductByName(request.ProductName);            

            return response;
        }
    }
}
