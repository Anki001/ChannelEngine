namespace Orders.Contracts.DomainModels.Products
{
    public class AdditionalData
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsPublic { get; set; }
    }
}
