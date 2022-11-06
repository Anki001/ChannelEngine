using System.Collections.Generic;

namespace Orders.Contracts.DomainModels.Products
{
    public class ProductUpsertStatus
    {
        public int RejectedCount { get; set; }
        public int AcceptedCount { get; set; }
        public List<ProductMessage> ProductMessages { get; set; }
    }
}
