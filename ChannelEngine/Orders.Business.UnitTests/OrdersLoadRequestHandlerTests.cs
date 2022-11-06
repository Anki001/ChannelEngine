using ChanelEngine.Service.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.Business.RequestHandlers;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using Orders.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainModail = Orders.Contracts.DomainModels;
namespace Orders.Business.UnitTests
{
    [TestClass]
    public class OrdersLoadRequestHandlerTests
    {
        private MockHelpers<OrdersLoadRequestHandler> _mockHelpers;
        private IEnumerable<OrdersInfo> _ordersInfo;        

        [TestInitialize]
        public void InitializeTests()
        {
            _mockHelpers = new MockHelpers<OrdersLoadRequestHandler>();
            InitializeData();
        }

        [TestMethod]
        public void ProcessRequest_With_Empty_Request_Faild_To_Fetch_Orders_Return_Error_Message()
        {
            //Arrange
            var expectedResult = new OrdersLoadResponse
            {
                IsSucess = false,
                Message = "Failed to fetch Orders",
                Orders = null
            };

            IEnumerable<OrdersInfo> ordersInfo = null;

            _mockHelpers.ChannelEngineService
                .Setup(x => x.GetInProgressOrdersAsync())
                .ReturnsAsync(ordersInfo)
                .Verifiable();

            var handler = new OrdersLoadRequestHandler(_mockHelpers.ChannelEngineService.Object);

            //Act
            var actual = handler.ProcessRequest(EmptyRequest.Instance);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.IsNull(actual.Result.Orders);
            Assert.AreEqual(expectedResult.IsSucess, actual.Result.IsSucess);
            Assert.AreEqual(expectedResult.Message, actual.Result.Message);
        }

        [TestMethod]
        public void ProcessRequest_With_Empty_Request_No_Inprogress_Orders_Return_Information_Message()
        {
            //Arrange
            var expectedResult = new OrdersLoadResponse
            {
                IsSucess = true,
                Message = "No inprogress orders found",
                Orders = null
            };

            IEnumerable<OrdersInfo> ordersInfo = new List<OrdersInfo> { };

            _mockHelpers.ChannelEngineService
                .Setup(x => x.GetInProgressOrdersAsync())
                .ReturnsAsync(ordersInfo)
                .Verifiable();

            var handler = new OrdersLoadRequestHandler(_mockHelpers.ChannelEngineService.Object);

            //Act
            var actual = handler.ProcessRequest(EmptyRequest.Instance);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.IsNull(actual.Result.Orders);
            Assert.AreEqual(expectedResult.IsSucess, actual.Result.IsSucess);
            Assert.AreEqual(expectedResult.Message, actual.Result.Message);
        }

        [TestMethod]
        public void ProcessRequest_With_Empty_Request_Found_Inprogress_Orders_Return_Max_Five_Orders()
        {
            //Arrange
            int serialNumber = 1;
            int expectedOrderCount = 5;

            var _lines = _ordersInfo.SelectMany(s => s.Lines)
                .OrderBy(x => (x.Quantity + x.CancellationRequestedQuantity))
                .Take(5);

            var expectedResult = new OrdersLoadResponse
            {
                IsSucess = true,
                Message = "Records fetched successfully",
                Orders = _lines.Select(x => new DomainModail.OrdersInfo
                {
                    SerialNumber = serialNumber++,
                    ProductName = x.Description,
                    GtIn = x.Gtin,
                    Quantity = x.Quantity
                })
            };

            _mockHelpers.ChannelEngineService
                .Setup(x => x.GetInProgressOrdersAsync())
                .ReturnsAsync(_ordersInfo)
                .Verifiable();

            var handler = new OrdersLoadRequestHandler(_mockHelpers.ChannelEngineService.Object);

            //Act
            var actual = handler.ProcessRequest(EmptyRequest.Instance);

            //Assert            
            Assert.IsNotNull(actual.Result.Orders);
            Assert.AreEqual(expectedOrderCount, actual.Result.Orders.Count());
            Assert.AreEqual(expectedResult.IsSucess, actual.Result.IsSucess);
            Assert.AreEqual(expectedResult.Message, actual.Result.Message);
        }

        private void InitializeData()
        {
            _ordersInfo = new List<OrdersInfo> {
                new OrdersInfo {
                    Id = 11,
                    ChannelName = "Test channel",
                    ChannelId = 1,
                    GlobalChannelName = "Custom Channel",
                    GlobalChannelId = 55,
                    ChannelOrderSupport = "SPLIT_ORDER_LINES",
                    ChannelOrderNo = "CE-TEST-39700",
                    MerchantOrderNo = null,
                    Status = "IN_PROGRESS",
                    IsBusinessOrder = false,
                    AcknowledgedDate = null,
                    CreatedAt = Convert.ToDateTime("2022-08-26T10:00:15.6578316+02:00"),
                    UpdatedAt = Convert.ToDateTime("2022-08-26T10:00:15.6578316+02:00"),
                    MerchantComment = null,
                    BillingAddress = GetDummyBillingAddress(),
                    ShippingAddress = GetDummyShippingAddress(),
                    SubTotalInclVat = 7.50,
                    SubTotalVat = 1.30,
                    ShippingCostsVat = 0.00,
                    TotalInclVat = 7.50,
                    TotalVat = 1.30,
                    OriginalSubTotalInclVat = 7.50,
                    OriginalSubTotalVat = 1.30,
                    OriginalShippingCostsInclVat = 0.00,
                    OriginalShippingCostsVat = 0.00,
                    OriginalTotalInclVat = 7.50,
                    OriginalTotalVat = 1.30,
                    Lines = new List<Line> {
                        GetDummyLine(),
                        GetDummyLine(),
                        GetDummyLine(),
                        GetDummyLine(),
                        GetDummyLine(),
                        GetDummyLine(),
                    },
                    ShippingCostsInclVat = 0.00,
                    Phone = "+31711000000",
                    Email = "test@channelengine.net",
                    CompanyRegistrationNo = null,
                    VatNo = null,
                    PaymentMethod = "iDEAL",
                    PaymentReferenceNo = null,
                    CurrencyCode = "EUR",
                    OrderDate = Convert.ToDateTime("2022-08-26T10:00:15.6551347+02:00"),
                    ChannelCustomerNo = null,
                    ExtraData = GetDummyExtraData()
                }
            };
        }

        private Line GetDummyLine()
        {
            return new Line
            {
                Status = "IN_PROGRESS",
                IsFulfillmentByMarketplace = false,
                Gtin = "8719351029609",
                Description = "T-shirt met lange mouw BASIC petrol: M",
                StockLocation = GetDummyStockLocation(),
                UnitVat = 1.30,
                LineTotalInclVat = 7.50,
                LineVat = 1.30,
                OriginalUnitPriceInclVat = 7.50,
                OriginalUnitVat = 1.30,
                OriginalLineTotalInclVat = 7.50,
                OriginalLineVat = 1.30,
                OriginalFeeFixed = 0.00,
                BundleProductMerchantProductNo = null,
                JurisCode = null,
                JurisName = null,
                VatRate = 21.00,
                ExtraData = null,
                ChannelProductNo = "10179",
                MerchantProductNo = "001201-M",
                Quantity = 1,
                CancellationRequestedQuantity = 0,
                UnitPriceInclVat = 7.50,
                FeeFixed = 0.00,
                FeeRate = 0.00,
                Condition = "UNKNOWN",
                ExpectedDeliveryDate = Convert.ToDateTime("2022-08-28T10:00:15.5200441+02:00")
            };
        }

        private StockLocation GetDummyStockLocation()
        {
            return new StockLocation
            {
                Id = 2,
                Name = "api-dev"
            };
        }

        private BillingAddress GetDummyBillingAddress()
        {
            return new BillingAddress
            {
                Line1 = "Teststreet 22",
                Line2 = null,
                Line3 = null,
                Gender = "NOT_APPLICABLE",
                CompanyName = null,
                FirstName = "T.",
                LastName = "Tester",
                StreetName = "Teststreet",
                HouseNr = "22",
                HouseNrAddition = null,
                ZipCode = "1111 TT",
                City = "Testtown",
                Region = null,
                CountryIso = "NL",
                Original = null
            };
        }

        private ShippingAddress GetDummyShippingAddress()
        {
            return new ShippingAddress
            {
                Line1 = "Teststreet 22",
                Line2 = null,
                Line3 = null,
                Gender = "NOT_APPLICABLE",
                CompanyName = null,
                FirstName = "T.",
                LastName = "Tester",
                StreetName = "Teststreet",
                HouseNr = "22",
                HouseNrAddition = null,
                ZipCode = "1111 TT",
                City = "Testtown",
                Region = null,
                CountryIso = "NL",
                Original = null
            };
        }

        private ExtraData GetDummyExtraData()
        {
            return new ExtraData
            {
                VAT_CALCULATION_METHOD_KEY = "FROM_PRICE_INCL"
            };
        }
    }
}
