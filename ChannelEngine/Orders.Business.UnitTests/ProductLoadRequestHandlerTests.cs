using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.Business.RequestHandlers;
using Orders.ChanelEngine.Service;
using Orders.Common;
using Orders.Contracts.DomainModels.Products;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using Orders.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Orders.Business.UnitTests
{
    [TestClass]
    public class ProductLoadRequestHandlerTests
    {
        private MockHelpers<ProductLoadRequestHandler> _mockHelpers;
        private ProductLoadResponse _productLoadResponse;

        [TestInitialize]
        public void InitializeTests()
        {
            _mockHelpers = new MockHelpers<ProductLoadRequestHandler>();
            InitializeData();
        }

        [TestMethod]
        public void ProcessRequest_With_Product_Name_As_Parameter_Return_Product()
        {
            //Arrange
            var request = new ProductLoadRequest
            {
                ProductName = "T-shirt met lange mouw BASIC petrol: XL"
            };

            var expectedResponse = _productLoadResponse;

            _mockHelpers.ChannelEngineService
                .Setup(x => x.GetProductByName(request.ProductName))
                .ReturnsAsync(expectedResponse)
                .Verifiable();

            var handler = new ProductLoadRequestHandler(_mockHelpers.ChannelEngineService.Object);

            //Act
            var actual = handler.ProcessRequest(request);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.AreEqual(expectedResponse.Success, actual.Result.Success);            
        }
        
        private void InitializeData()
        {
            _productLoadResponse = new ProductLoadResponse
            {
                Content = new List<Product> {
                    new Product {
                        IsActive = true,
                        ExtraData = new List<AdditionalData> {
                            new AdditionalData{
                            Key = "PED",
                            Value = String.Empty,
                            Type = "TEXT",
                            IsPublic = false
                            }
                        },
                        Name = "T-shirt met lange mouw BASIC petrol: XL",
                        Description= "\n\t\t\t<br></br>\n\t\t\tBasic t-shirt met lange mouwen. Het model valt aansluitend en heeft een petrol kleur.\n\n\t\t\t<br><br>Materiaal:</br>\n\t\t\t92% katoen 8% elastaan.\n\t\t",
                        Brand= "La Ligna",
                        Size= "XL",
                        Color= "petrol",
                        Ean= "8719351029609",
                        ManufacturerProductNumber= "120675",
                        MerchantProductNo= "001201-XL",
                        Stock= 4,
                        Price= 9.5,
                        MSRP= 27.99,
                        PurchasePrice= 10,
                        VatRateType= "STANDARD",
                        ShippingCost= 3.95,
                        ShippingTime= "Dit product is helaas uitverkocht",
                        Url= "http://www.mijnshop.nl/shirt-petrol-xl",
                        ImageUrl= "http://feeds.channelengine.net/shirt.jpg",
                        ExtraImageUrl1= null,
                        ExtraImageUrl2= null,
                        ExtraImageUrl3= null,
                        ExtraImageUrl4= null,
                        ExtraImageUrl5= null,
                        ExtraImageUrl6= null,
                        ExtraImageUrl7= null,
                        ExtraImageUrl8= null,
                        ExtraImageUrl9= null,
                        CategoryTrail= "Vrouwen > Tops > Basics"
                    }
                },
                Count = 1,
                TotalCount = 1,
                ItemsPerPage = 100,
                StatusCode = 200,
                RequestId = null,
                LogId = null,
                Success = true,
                Message = null,
                ValidationErrors = new ValidationErrors()
            };
        }
    }
}
