using Orders.Contracts.DomainModels;

namespace Orders.Contracts.Messages.Response
{
    public class OrdersLoadResponse : StatusResponse
    {
        public OrdersInfo Orders { get; set; }
    }
}
