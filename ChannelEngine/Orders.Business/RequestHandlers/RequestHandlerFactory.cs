using Orders.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Business.RequestHandlers
{
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public RequestHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> ProcessRequest<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class
        {
            var handler = (IRequestHandler<TRequest, TResponse>)_serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResponse>));

            if (handler == null)
                throw new NotImplementedException($"No handler register for type: {typeof(TRequest).FullName}");

            return await handler.ProcessRequest(request);
        }
    }
}
