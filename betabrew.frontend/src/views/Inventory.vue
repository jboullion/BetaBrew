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
import { IProductInventory } from "@/types/Product";

import BetaButton from "@/components/BetaButton.vue";
import NewProductModal from "@/components/Modals/NewProductModal.vue";
import ShipmentModal from "@/components/Modals/ShipmentModal.vue";

@Component({
  name: "Inventory",
  components: { BetaButton, NewProductModal, ShipmentModal },
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

  inventory: IProductInventory[] = [
    {
      id: 1,
      product: {
        id: 1,
        name: "Product 1",
        description: "The first product",
        price: 10,
        createdOn: new Date(),
        updatedOn: new Date(),
        isTaxable: true,
        isArchived: false,
      },
      quantityOnHand: 100,
      idealQuantity: 100,
    },
    {
      id: 2,
      product: {
        id: 2,
        name: "Product 2",
        description: "The second product",
        price: 30,
        createdOn: new Date(),
        updatedOn: new Date(),
        isTaxable: true,
        isArchived: false,
      },
      quantityOnHand: 80,
      idealQuantity: 75,
    },
  ];

  showNewProductModal(): void {
    this.isNewProductVisible = true;
  }

  showShipmentModal(): void {
    this.isShipmentVisible = true;
  }

  saveNewProduct(): void {
    console.log("saveNewProduct");
  }

  saveShipment(): void {
    console.log("saveShipment");
  }

  closeModals(): void {
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
</style>
