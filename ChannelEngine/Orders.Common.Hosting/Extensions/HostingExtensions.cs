using ChanelEngine.Service;
using ChanelEngine.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Business.Interfaces;
using Orders.Business.RequestHandlers;
using Orders.ChanelEngine.Service;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Common.Interfaces;
using System.Linq;
using System.Reflection;

namespace Orders.Common.Hosting.Extensions
{
    public static class HostingExtensions
    {
        public static IHostBuilder ServiceConfiguration(this IHostBuilder builder)
        {
            return builder.ConfigureServices((_, services) =>
                        services.AddScoped<IApplicationConfiguration, ConsoleAppConfiguration>()
                                .AddScoped<IRequestHandlerFactory, RequestHandlerFactory>()
                                .AddScoped<IChannelEngineService, ChannelEngineService>()
                                .AddScoped<IChannelEngineWebClient, ChannelEngineWebClient>()
                                .RegisterRequestHandler());
        }
        public static IServiceCollection RegisterRequestHandler(this IServiceCollection services)
        {
            Assembly.GetAssembly(typeof(RequestHandlerFactory))
                        .GetTypes()
                        .Where(a => a.Name.EndsWith("RequestHandler"))
                        .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
                        .ToList()
                        .ForEach(typesToRegister =>
                        {
                            typesToRegister.serviceTypes.ForEach(
                                typeToRegister => services
                                                    .AddScoped(typeToRegister, typesToRegister.assignedType));
                        });

            return services;
        }
    }
}
