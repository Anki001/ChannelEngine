using Orders.Contracts.DomainModels.Products;

namespace ChanelEngine.Service.Common.Models.Products
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
