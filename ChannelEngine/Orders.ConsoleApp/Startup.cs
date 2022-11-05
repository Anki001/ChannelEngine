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

        public void PrintOrderInformation()
        {
            var response = _requestHandlerFactory.ProcessRequest<EmptyRequest, OrdersLoadResponse>(EmptyRequest.Instance);

            Console.WriteLine("Hello World");
            Console.ReadLine();
        }        
    }
}
