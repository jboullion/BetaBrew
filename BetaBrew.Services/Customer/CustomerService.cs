using System;
using System.Collections.Generic;
using System.Linq;
using BetaBrew.Data;
using BetaBrew.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BetaBrew.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly BetaBrewDbContext _db;

        public CustomerService(BetaBrewDbContext dbContext)
        {
            _db = dbContext;
        }
        
        /// <summary>
        /// Return a list of customers and their addresses
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }
        
        /// <summary>
        /// Get a customer record by Primary Key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Customer GetCustomerById(int id)
        {
            return _db.Customers.Find(id);
        }
        
        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Time = DateTime.UtcNow,
                    Message = "Created new customer",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }
        
        /// <summary>
        /// Delete a Customer Record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            try
            {
                var customerToDelete = _db.Customers.Find(id);
            
                if (customerToDelete == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
                        Time = DateTime.UtcNow,
                        Message = "Customer Not Found",
                        IsSuccess = false
                    };
                }
                
                _db.Remove(customerToDelete);
                _db.SaveChanges();
                
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Customer Deleted",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }
    }
}