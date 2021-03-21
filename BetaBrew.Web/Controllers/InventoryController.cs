using System;
using System.Linq;
using BetaBrew.Services.Customer;
using BetaBrew.Services.Inventory;
using BetaBrew.Web.Serialization;
using BetaBrew.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Web.Controllers
{
    
    [ApiController]
    public class InventoryController: ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;
        
        public InventoryController(
            ILogger<InventoryController> logger, 
            IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }
        
        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting current Inventory");
            
            var inventory = _inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    Product = ProductMapper.SerializeProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                })
                .OrderBy(inv => inv.Product.Name)
                .ToList();
            
            return Ok(inventory);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            _logger.LogInformation($"Updating Inventory for {shipment.ProductId} : Adjustment {shipment.Adjustment}");
            
            var inventory = _inventoryService.UpdateUnitsAvailable(shipment.ProductId, shipment.Adjustment);

            return Ok(inventory);
        }
        
    }

}