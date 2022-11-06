using ChanelEngine.Service.Interfaces;
using Moq;
using Orders.Business.Interfaces;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Common.Interfaces;

namespace Orders.UnitTests.Common
{
    public sealed class MockHelpers<T>
    {
        private Mock<IApplicationConfiguration> _applicationConfiguration;
        private Mock<IRequestHandlerFactory> _requestHandlerFactory;
        private Mock<IChannelEngineService> _channelEngineService;
        private Mock<IChannelEngineWebClient> _channelEngineWebClient;

        public Mock<IApplicationConfiguration> ApplicationConfiguration => _applicationConfiguration ??= new Mock<IApplicationConfiguration>();
        public Mock<IRequestHandlerFactory> RequestHandlerFactory => _requestHandlerFactory ??= new Mock<IRequestHandlerFactory>();
        public Mock<IChannelEngineService> ChannelEngineService => _channelEngineService ??= new Mock<IChannelEngineService>();
        public Mock<IChannelEngineWebClient> ChannelEngineWebClient => _channelEngineWebClient ??= new Mock<IChannelEngineWebClient>();
    }
}
