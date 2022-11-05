using System.Threading.Tasks;

namespace ChanelEngine.Service.Interfaces
{
    public interface IChannelEngineWebClient
    {
        Task<string> GetAsync(string url);
    }
}
