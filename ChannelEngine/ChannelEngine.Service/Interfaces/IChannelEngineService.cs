using ChanelEngine.Service.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.ChanelEngine.Service.Interfaces
{
    public interface IChannelEngineService
    {
        Task<IEnumerable<OrdersInfo>> GetInProgressOrdersAsync();
    }
}
