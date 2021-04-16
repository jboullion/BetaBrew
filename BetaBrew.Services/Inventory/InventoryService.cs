using System;
using System.Collections.Generic;
using System.Linq;
using BetaBrew.Data;
using BetaBrew.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Services.Inventory
{
    
    public class InventoryService : IInventoryService
    {
        private readonly BetaBrewDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(BetaBrewDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot();
                }
                
                catch (Exception e)
                {
                    _logger.LogError("Error creating inventory snapshot");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.ProductInventory>
                {
                    Data = inventory,
                    Time = DateTime.UtcNow,
                    Message = $"Product Inventory Updated to {inventory.QuantityOnHand}",
                    IsSuccess = true
                };
            }
            
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.ProductInventory>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }

        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(inv => inv.Product)
                .FirstOrDefault(inv => inv.Product.Id == productId);
        }

        
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(2);
            
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest
                               && !snap.Product.IsArchived)
                .ToList();
        }
        
        /// <summary>
        /// Create a snapshot of a single item
        /// </summary>
        /// <param name="inventory"></param>
        /*
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }
        */


        /// <summary>
        /// Create a snapshot of the current inventory
        /// </summary>
        private void CreateSnapshot()
        {
            var now = DateTime.UtcNow;

            var inventories = _db.ProductInventories
                .Include(inv => inv.Product)
                .ToList();

            foreach(var inventory in inventories)
            {
                var snapshot = new ProductInventorySnapshot
                {
                    SnapshotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
                };

                _db.Add(snapshot);
            }
           
        }
    }
}