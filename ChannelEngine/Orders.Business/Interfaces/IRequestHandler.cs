using System.Threading.Tasks;

namespace Orders.Business.Interfaces
{
    public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : class
    where TResponse : class
    {
        Task<TResponse> ProcessRequest(TRequest request);
    }
}
