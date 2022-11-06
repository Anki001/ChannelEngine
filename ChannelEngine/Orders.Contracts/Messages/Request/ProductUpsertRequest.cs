using Orders.Contracts.DomainModels.Products;
using System.Collections.Generic;

namespace Orders.Contracts.Messages.Request
{
    public class ProductUpsertRequest
    {
        public string ParentMerchantProductNo { get; set; }
        public string ParentMerchantProductNo2 { get; set; }
        public List<AdditionalData> ExtraData { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Ean { get; set; }
        public string ManufacturerProductNumber { get; set; }
        public string MerchantProductNo { get; set; }
        public int? Stock { get; set; }
        public double? Price { get; set; }
        public double? MSRP { get; set; }
        public double? PurchasePrice { get; set; }
        public string VatRateType { get; set; }
        public double? ShippingCost { get; set; }
        public string ShippingTime { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public object ExtraImageUrl1 { get; set; }
        public object ExtraImageUrl2 { get; set; }
        public object ExtraImageUrl3 { get; set; }
        public object ExtraImageUrl4 { get; set; }
        public object ExtraImageUrl5 { get; set; }
        public object ExtraImageUrl6 { get; set; }
        public object ExtraImageUrl7 { get; set; }
        public object ExtraImageUrl8 { get; set; }
        public object ExtraImageUrl9 { get; set; }
        public string CategoryTrail { get; set; }
    }
}
