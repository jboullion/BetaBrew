using System;
using System.Collections.Generic;
using BetaBrew.Data.Models;

namespace BetaBrew.Web.ViewModels
{
    /// <summary>
    /// View Model for open Sales Orders
    /// </summary>
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        public int CustomerId { get; set; }
        public List<SalesOrderItemModel> LineItems { get; set; }
    }
    
    /// <summary>
    /// View model for SalesOrderItems
    /// </summary>
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}