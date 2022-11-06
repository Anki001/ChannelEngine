using Orders.Business.Interfaces;
using Orders.Contracts.DomainModels.Products;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.ConsoleApp
{
    public class OrderManagement
    {
        private readonly IRequestHandlerFactory _requestHandlerFactory;

        public OrderManagement(IRequestHandlerFactory requestHandlerFactory)
        {
            _requestHandlerFactory = requestHandlerFactory;
        }

        public async void PrintOrderInformation()
        {
            var response = await _requestHandlerFactory.ProcessRequest<EmptyRequest, OrdersLoadResponse>(EmptyRequest.Instance);

            if (response == null && !response.IsSucess)
            {
                Console.WriteLine($"Error: {response.Message}");
                return;
            }

            foreach (var prod in response.Orders)
            {
                Console.WriteLine($"{prod.SerialNumber}     {prod.ProductName}      {prod.GtIn}     {prod.Quantity}" + Environment.NewLine);
            }

            Console.ReadLine();
        }        

        public async void ModifyStock(int stockQty)
        {
            var upsertResponse = new ProductUpsertResponse();

            var loadRequest = new ProductLoadRequest
            {
                ProductName = "T-shirt met lange mouw BASIC petrol: XL"
            };

            var loadResponse = await _requestHandlerFactory.ProcessRequest<ProductLoadRequest, ProductLoadResponse>(loadRequest);

            if (loadResponse.Content.Any())
            {
                var product = loadResponse.Content.Where(x => x.Name.Equals(loadRequest.ProductName)).FirstOrDefault();

                if (product != null)
                {
                    product.Stock = stockQty;

                    var upsertRequest = GetProductUpsertRequest(product);

                    upsertResponse = await _requestHandlerFactory.ProcessRequest<IEnumerable<ProductUpsertRequest>, ProductUpsertResponse>(upsertRequest);
                }
            }

            if (!upsertResponse.Success)
            {
                Console.WriteLine("Failed to update product stock");
                return;
            }

            Console.WriteLine("Product stock updated successfully...");
        }

        private IEnumerable<ProductUpsertRequest> GetProductUpsertRequest(Product product)
        {
            return new List<ProductUpsertRequest> {
                new ProductUpsertRequest
                {
                    ExtraData = product.ExtraData,
                    Name = product.Name,
                    Description = product.Description,
                    Brand = product.Brand,
                    Size = product.Size,
                    Color = product.Color,
                    Ean = product.Ean,
                    ManufacturerProductNumber = product.ManufacturerProductNumber,
                    MerchantProductNo = product.MerchantProductNo,
                    Stock = product.Stock,
                    Price = product.Price,
                    MSRP = product.MSRP,
                    PurchasePrice = product.PurchasePrice,
                    VatRateType = product.VatRateType,
                    ShippingCost = product.ShippingCost,
                    ShippingTime = product.ShippingTime,
                    Url = product.Url,
                    ImageUrl = product.ImageUrl,
                    ExtraImageUrl1 = product.ExtraImageUrl1,
                    ExtraImageUrl2 = product.ExtraImageUrl2,
                    ExtraImageUrl3 = product.ExtraImageUrl3,
                    ExtraImageUrl4 = product.ExtraImageUrl4,
                    ExtraImageUrl5 = product.ExtraImageUrl5,
                    ExtraImageUrl6 = product.ExtraImageUrl6,
                    ExtraImageUrl7 = product.ExtraImageUrl7,
                    ExtraImageUrl8 = product.ExtraImageUrl8,
                    ExtraImageUrl9 = product.ExtraImageUrl9,
                    CategoryTrail = product.CategoryTrail
                }
            };
        }
    }
}
