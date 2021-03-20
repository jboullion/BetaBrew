using System;
using System.Collections.Generic;
using System.Linq;
using BetaBrew.Data;
using BetaBrew.Data.Models;
using BetaBrew.Services.Inventory;
using BetaBrew.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly BetaBrewDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(
            BetaBrewDbContext dbContext, 
            ILogger<OrderService> logger,
            IProductService productService,
            IInventoryService inventoryService)
        {
            _db = dbContext;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }
        
        /// <summary>
        /// Create an open sales order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;

                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Open Order Created",
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

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            try
            {
                var salesOrder = _db.SalesOrders.Find(id);
                salesOrder.UpdatedOn = DateTime.UtcNow;
                salesOrder.IsPaid = true;

                _db.SalesOrders.Update(salesOrder);
                
                _db.SaveChanges();
                
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Sales Order Fullfilled",
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