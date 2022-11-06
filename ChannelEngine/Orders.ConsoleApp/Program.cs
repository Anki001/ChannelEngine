using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Business.Interfaces;
using Orders.Common.Hosting.Extensions;
using System;

namespace Orders.ConsoleApp
{
    internal class Program
    {
        public static IServiceProvider _serviceProvider;
        public static IServiceScope _serviceScope;

        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ServiceConfiguration()
                .Build();

            _serviceScope = host.Services.CreateScope();
            _serviceProvider = _serviceScope.ServiceProvider;

            var requestHandlerFactory = GetService<IRequestHandlerFactory>();

            var startup = new OrderInformation(requestHandlerFactory);
            startup.PrintOrderInformation();

            host.RunAsync().Wait();
        }

        public static T GetService<T>() where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }    
}
