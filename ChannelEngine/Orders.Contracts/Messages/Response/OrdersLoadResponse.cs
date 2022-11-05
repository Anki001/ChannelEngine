using Orders.Contracts.DomainModels;
using System.Collections.Generic;

namespace Orders.Contracts.Messages.Response
{
    public class OrdersLoadResponse : StatusResponse
    {
        public IEnumerable<OrdersInfo> Orders { get; set; }
    }
}
