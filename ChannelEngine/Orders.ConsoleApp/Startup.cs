using Orders.Business.Interfaces;
using Orders.Business.RequestHandlers;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.ConsoleApp
{
    public class OrderInformation
    {
        private readonly IRequestHandlerFactory _requestHandlerFactory;

        public OrderInformation(IRequestHandlerFactory requestHandlerFactory)
        {
            _requestHandlerFactory = requestHandlerFactory;
        }

        public async void PrintOrderInformation()
        {
            var request = new ProductLoadRequest { ProductName = "T-shirt met lange mouw BASIC petrol: XL" };

            var response = await _requestHandlerFactory.ProcessRequest<ProductLoadRequest, ProductLoadResponse>(request);

            //var response = await _requestHandlerFactory.ProcessRequest<EmptyRequest, OrdersLoadResponse>(EmptyRequest.Instance);

            //if (response == null && !response.IsSucess)
            //{
            //    Console.WriteLine($"Error: {response.Message}");
            //    return;
            //}

            //foreach (var prod in response.Orders)
            //{
            //    Console.WriteLine($"{prod.SerialNumber}     {prod.ProductName}      {prod.GtIn}     {prod.Quantity}" + Environment.NewLine);
            //}

            //Console.ReadLine();
        }
    }
}
