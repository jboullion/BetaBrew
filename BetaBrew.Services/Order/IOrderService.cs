using System.Collections.Generic;
using BetaBrew.Data.Models;

namespace BetaBrew.Services.Order
{
    public interface IOrderService
    {
        List<Data.Models.SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}