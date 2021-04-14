<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle" class="page-title">Inventory</h1>

    <inventory-chart />

    <div class="inventory-actions">
      <beta-button id="addNewBtn" @button:click="showNewProductModal">
        Add New Item
      </beta-button>
      <beta-button id="receiveShipmentBtn" @button:click="showShipmentModal">
        Receive Shipment
      </beta-button>
    </div>

    <table id="inventoryTable" class="table">
      <tr>
        <th>Item</th>
        <th>Quantity On Hand</th>
        <th>Unit Price</th>
        <th>Taxable</th>
        <th>Remove</th>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td
          v-bind:class="`${applyColor(
            item.quantityOnHand,
            item.idealQuantity
          )}`"
        >
          {{ item.quantityOnHand }}
        </td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td
          class="lni lni-cross-circle product-archive"
          @click="archiveProduct(item.product.id)"
        ></td>
      </tr>
    </table>
    <new-product-modal
      v-if="isNewProductVisible"
      :inventory="inventory"
      @save:product="saveNewProduct"
      @close="closeModals"
    />
    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @save:shipment="saveShipment"
      @close="closeModals"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,
import { IProduct, IProductInventory } from '@/types/Product'
import { IShipment } from '@/types/Shipment'

import BetaButton from '@/components/BetaButton.vue'
import NewProductModal from '@/components/Modals/NewProductModal.vue'
import ShipmentModal from '@/components/Modals/ShipmentModal.vue'
import InventoryChart from '@/components/Charts/InventoryChart.vue'
import { InventoryService } from '@/services/inventory-service'
import { ProductService } from '@/services/product-service'

const inventoryService = new InventoryService()
const productService = new ProductService()

@Component({
  name: 'Inventory',
  components: { BetaButton, NewProductModal, ShipmentModal, InventoryChart },
})
export default class Inventory extends Vue {
  isNewProductVisible = false
  isShipmentVisible = false

  inventory: IProductInventory[] = []
  //fade: boolean[] = []

  // @Watch("inventory")
  // onPropertyChanged(value: IProductInventory[], oldValue: IProductInventory[]): void {
  //   console.log("oldValue");
  //   console.log(oldValue);
  //   console.log("value");
  //   console.log(value);
  // }

  log(data: never): void {
    console.log(data)
  }

  applyColor(quantityOnHand: number, idealQuantity: number): string {
    if (quantityOnHand <= 0) {
      return 'red'
    } else if (quantityOnHand < idealQuantity) {
      return 'yellow'
    }

    return 'green'
  }

  showNewProductModal(): void {
    this.isNewProductVisible = true
  }

  showShipmentModal(): void {
    this.isShipmentVisible = true
  }

  closeModals(): void {
    this.isShipmentVisible = false
    this.isNewProductVisible = false
  }

  async archiveProduct(productId: number): Promise<void> {
    //this.fade[productId] = true
    await productService.archiveProduct(productId)
    await this.updateInventory()
  }

  async saveNewProduct(product: IProduct): Promise<void> {
    await productService.saveNewProduct(product)
    await this.updateInventory()
  }

  async saveShipment(shipment: IShipment): Promise<void> {
    await inventoryService.updateInventoryQuantity(shipment)
    this.isShipmentVisible = false
    await this.updateInventory()
  }

  async updateInventory(): Promise<void> {
    this.inventory = await inventoryService.getInventory()
    await this.$store.dispatch('assignSnapshots')
  }

  async created(): Promise<void> {
    await this.updateInventory()
  }
}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';

.inventory-actions {
  display: flex;
  margin-bottom: 0.8rem;
}

.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $brand-red;
}

.fade {
  opacity: 0.5;
}
</style>
