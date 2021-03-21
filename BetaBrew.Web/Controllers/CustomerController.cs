using BetaBrew.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        
        public CustomerController(
            ILogger<CustomerController> logger, 
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
    }
}