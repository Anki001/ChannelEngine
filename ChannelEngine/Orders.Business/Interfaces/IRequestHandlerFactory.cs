using System.Threading.Tasks;

namespace Orders.Business.Interfaces
{
    public interface IRequestHandlerFactory
    {
        Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class;
    }
}
