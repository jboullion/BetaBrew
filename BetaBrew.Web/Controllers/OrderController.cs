using BetaBrew.Services.Customer;
using BetaBrew.Services.Order;
using BetaBrew.Web.Serialization;
using BetaBrew.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        
        public OrderController(
            ILogger<OrderController> logger, 
            IOrderService orderService,
            ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Generating Invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetCustomerById(invoice.CustomerId);
            _orderService.GenerateInvoiceForOrder(order);
            return Ok();
        }


        [HttpGet("/api/order")]
        public ActionResult GetOrders()
        {
            _logger.LogInformation("Getting All Orders");
            var orders = _orderService.GetOrders();
            var orderModels = OrderMapper.SerializeOrdersToViewModel(orders);
            return Ok(orderModels);
        }

        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult CompleteOrder(int id)
        {
            _logger.LogInformation("Completing Order");
            _orderService.MarkFulfilled(id);
            return Ok();
        }
        
        
    }
}