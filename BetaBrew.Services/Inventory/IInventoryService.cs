using System.Collections.Generic;
using BetaBrew.Data.Models;

namespace BetaBrew.Services.Inventory
{
    public interface IInventoryService
    {
        List<Data.Models.ProductInventory> GetCurrentInventory();
        ServiceResponse<Data.Models.ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        ProductInventory GetByProductId(int productId);
        List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}