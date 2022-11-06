using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.Business.RequestHandlers;
using Orders.Contracts.DomainModels.Products;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using Orders.UnitTests.Common;
using System.Collections.Generic;

namespace Orders.Business.UnitTests
{
    [TestClass]
    public class ProductUpsertRequestHandlerTests
    {
        private MockHelpers<ProductUpsertRequestHandler> _mockHelpers;
        private ProductUpsertRequest _productUpsertRequest;
        private ProductUpsertResponse _productUpsertResponse;

        [TestInitialize]
        public void InitializeTests()
        {
            _mockHelpers = new MockHelpers<ProductUpsertRequestHandler>();
            InitializeData();
        }

        [TestMethod]
        public void ProcessRequest_With_Product_Name_As_Parameter_Return_Product()
        {
            //Arrange                        
            _mockHelpers.ChannelEngineService
                .Setup(x => x.UpsertProduct(_productUpsertRequest))
                .ReturnsAsync(_productUpsertResponse)
                .Verifiable();

            var handler = new ProductUpsertRequestHandler(_mockHelpers.ChannelEngineService.Object);

            //Act
            var actual = handler.ProcessRequest(_productUpsertRequest);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.AreEqual(_productUpsertResponse.Success, actual.Result.Success);
            Assert.AreEqual(_productUpsertResponse.Content.AcceptedCount, actual.Result.Content.AcceptedCount);
        }

        private void InitializeData()
        {
            _productUpsertRequest = new ProductUpsertRequest
            {
                ParentMerchantProductNo = null,
                ParentMerchantProductNo2 = null,
                ExtraData = new List<AdditionalData>
                {
                    new AdditionalData {
                        Key = "PED",
                        Value = string.Empty,
                        Type = "TEXT",
                        IsPublic = false
                    }
                },
                Name = "T-shirt met lange mouw BASIC petrol: XL",
                Description = "\n\t\t\t<br></br>\n\t\t\tBasic t-shirt met lange mouwen. Het model valt aansluitend en heeft een petrol kleur.\n\n\t\t\t<br><br>Materiaal:</br>\n\t\t\t92% katoen 8% elastaan.\n\t\t",
                Brand = "La Ligna",
                Size = "XL",
                Color = "petrol",
                Ean = "8719351029609",
                ManufacturerProductNumber = "120675",
                MerchantProductNo = "001201-XL",
                Stock = 4,
                Price = 9.5,
                MSRP = 27.99,
                PurchasePrice = 10,
                VatRateType = "STANDARD",
                ShippingCost = 3.95,
                ShippingTime = "Dit product is helaas uitverkocht",
                Url = "http://www.mijnshop.nl/shirt-petrol-xl",
                ImageUrl = "http://feeds.channelengine.net/shirt.jpg",
                ExtraImageUrl1 = null,
                ExtraImageUrl2 = null,
                ExtraImageUrl3 = null,
                ExtraImageUrl4 = null,
                ExtraImageUrl5 = null,
                ExtraImageUrl6 = null,
                ExtraImageUrl7 = null,
                ExtraImageUrl8 = null,
                ExtraImageUrl9 = null,
                CategoryTrail = "Vrouwen > Tops > Basics"
            };

            _productUpsertResponse = new ProductUpsertResponse
            {
                Content = new ProductUpsertStatus
                {
                    RejectedCount = 0,
                    AcceptedCount = 1,
                    ProductMessages = null,
                },
                StatusCode = 200,
                RequestId = null,
                LogId = null,
                Success = true,
                Message = null,
                ValidationErrors = null
            };
        }
    }
}
