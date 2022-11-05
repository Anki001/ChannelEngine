using ChanelEngine.Service.Common.Models;
using System.Threading.Tasks;

namespace Orders.ChanelEngine.Service.Interfaces
{
    public interface IChannelEngineService
    {
        Task<OrdersInfoPm> GetOrdersAsync();
    }
}
