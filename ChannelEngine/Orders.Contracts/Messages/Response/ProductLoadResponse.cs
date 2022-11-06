using Orders.Contracts.DomainModels.Products;
using System.Collections.Generic;

namespace Orders.Contracts.Messages.Response
{
    public class ProductLoadResponse
    {
        public List<Product> Content { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int StatusCode { get; set; }
        public object RequestId { get; set; }
        public object LogId { get; set; }
        public bool Success { get; set; }
        public object Message { get; set; }
        public ValidationErrors ValidationErrors { get; set; }
    }
}
