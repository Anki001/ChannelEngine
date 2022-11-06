using ChanelEngine.Service.Common.Models;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.ChanelEngine.Service.Interfaces
{
    public interface IChannelEngineService
    {
        Task<ProductUpsertResponse> UpsertProduct(IEnumerable<ProductUpsertRequest> content);
        Task<ProductLoadResponse> GetProductByName(string productName);
        Task<IEnumerable<OrdersInfo>> GetInProgressOrdersAsync();
    }
}
