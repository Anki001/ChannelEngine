using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders.Business.Interfaces;
using Orders.Contracts.Messages.Request;
using Orders.Contracts.Messages.Response;
using Orders.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestHandlerFactory _requestHandlerFactory;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IRequestHandlerFactory requestHandlerFactory,
            ILogger<HomeController> logger)
        {
            _requestHandlerFactory = requestHandlerFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = new OrdersLoadResponse();

            response = await _requestHandlerFactory.ProcessRequest<EmptyRequest, OrdersLoadResponse>(EmptyRequest.Instance);

            if (response == null && !response.IsSucess)
            {
                _logger.LogError($"Error: {response.Message}");
                return Error();
            }

            return View(response.Orders);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
