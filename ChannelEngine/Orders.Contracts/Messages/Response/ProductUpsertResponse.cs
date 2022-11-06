using Orders.Contracts.DomainModels.Products;

namespace Orders.Contracts.Messages.Response
{
    public class ProductUpsertResponse
    {
        public ProductUpsertStatus Content { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
        public string LogId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ValidationErrors ValidationErrors { get; set; }
    }
}
