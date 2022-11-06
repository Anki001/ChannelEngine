using ChanelEngine.Service.Common.Models;
using ChanelEngine.Service.Common.Models.Products;
using Orders.Contracts.Messages.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.ChanelEngine.Service.Interfaces
{
    public interface IChannelEngineService
    {
        Task<ProductUpsertResponse> UpsertProduct(string content);
        Task<ProductLoadResponse> GetProductByName(string productName);
        Task<IEnumerable<OrdersInfo>> GetInProgressOrdersAsync();
    }
}
