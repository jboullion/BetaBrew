using System;
using System.Collections.Generic;

namespace BetaBrew.Web.ViewModels
{
    public class ProductInventorySnapshotModel
    {
        public int ProductId { get; set; }
        public List<int> QuantityOnHand { get; set; }
    }

    public class SnapshotResponse
    {
        public List<ProductInventorySnapshotModel> ProductInventorySnapshots { get; set; }
        public List<DateTime> Timeline { get; set; }
    }
}