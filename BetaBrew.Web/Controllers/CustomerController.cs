using System;
using System.Linq;
using BetaBrew.Services.Customer;
using BetaBrew.Web.Serialization;
using BetaBrew.Web.ViewModels;
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
        
        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Creating Customer");

            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            
            customer.PrimaryAddress.CreatedOn = DateTime.UtcNow;
            customer.PrimaryAddress.UpdatedOn = DateTime.UtcNow;
            
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting Customer");

            var customers = _customerService.GetAllCustomers();
            
            // NOTE: Cant we just use the CustomerMapper.SerializeCustomer(customer) to do this?
            var customerModels = customers
                .Select(customer => new CustomerModel()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
                    CreatedOn = customer.CreatedOn,
                    UpdatedOn = customer.UpdatedOn
                })
                .OrderByDescending(customer => customer.CreatedOn)
                .ToList();

            return Ok(customerModels);
        }

        [HttpDelete("/api/customer")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting Customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    }
}