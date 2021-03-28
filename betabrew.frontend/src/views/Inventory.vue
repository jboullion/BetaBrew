<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle" class="page-title">Inventory</h1>

    <div class="inventory-actions">
      <beta-button id="addNewBtn" @click.native="showNewProductModal">
        Add New Item
      </beta-button>
      <beta-button id="receiveShipmentBtn" @click.native="showShipmentModal">
        Receive Shipment
      </beta-button>
    </div>

    <table id="inventoryTable" class="table">
      <tr>
        <th>Item</th>
        <th>Quantity On Hand</th>
        <th>Unit Price</th>
        <th>Taxable</th>
        <th>Delete</th>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td>{{ item.quantityOnHand }}</td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td>X</td>
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
import { Component, Vue } from "vue-property-decorator";
import { IProduct, IProductInventory } from "@/types/Product";
import { IShipment } from "@/types/Shipment";

import BetaButton from "@/components/BetaButton.vue";
import NewProductModal from "@/components/Modals/NewProductModal.vue";
import ShipmentModal from "@/components/Modals/ShipmentModal.vue";
import { InventoryService } from "@/services/inventory-service";

const inventoryService = new InventoryService();

@Component({
  name: "Inventory",
  components: { BetaButton, NewProductModal, ShipmentModal },
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

  inventory: IProductInventory[] = [];

  showNewProductModal(): void {
    this.isNewProductVisible = true;
  }

  showShipmentModal(): void {
    this.isShipmentVisible = true;
  }

  saveNewProduct(product: IProduct): void {
    console.log(product);
  }

  saveShipment(shipment: IShipment): void {
    console.log(shipment);
  }

  closeModals(): void {
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  async initialize(): Promise<void> {
    this.inventory = await inventoryService.getInventory();

  }

  async created(): Promise<void> {
    await this.initialize();
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
</style>
