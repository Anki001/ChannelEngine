using System.Collections.Generic;

namespace Orders.Contracts.DomainModels.Products
{
    public class ProductMessage
    {
        public string Name { get; set; }
        public string Reference { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }
    }
}
