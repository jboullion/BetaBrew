<template>
  <beta-modal>
    <template v-slot:header>Receive Shipment</template>
    <template v-slot:body>
      <label for="product">Product Received:</label>
      <select v-model="selectedProduct" class="shipmentItems" id="product">
        <option disabled value="">Select Product</option>
        <option
          v-for="item in inventory"
          :value="item.product"
          :key="item.product.id"
        >
          {{ item.product.name + ' ' + item.product.id }}
        </option>
      </select>

      <label for="qtyReceived">Quantity Received:</label>
      <input type="number" id="qtyReceived" v-model="qtyReceived" />
    </template>
    <template v-slot:footer>
      <beta-button @button:click="save" aria-label="Save new shipment"
        >Save Shipment</beta-button
      >
      <beta-button @button:click="close" aria-label="Close shipment modal"
        >Close</beta-button
      >
    </template>
  </beta-modal>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop } from 'vue-property-decorator'
import BetaButton from '@/components/BetaButton.vue'
import BetaModal from '@/components/Modals/BetaModal.vue'
import { IProduct, IProductInventory } from '@/types/Product'
import { IShipment } from '@/types/Shipment'

@Component({
  name: 'ShipmentModal',
  components: { BetaButton, BetaModal },
})
export default class ShipmentModal extends Vue {
  @Prop({ required: true, type: Array as () => IProductInventory[] })
  inventory!: IProductInventory[]

  selectedProduct: IProduct = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    isTaxable: false,
    isArchived: false,
  }

  qtyReceived = 1

  close(): void {
    this.$emit('close')
  }

  save(): void {
    let shipment: IShipment = {
      productId: this.selectedProduct.id,
      adjustment: this.qtyReceived,
    }

    this.$emit('save:shipment', shipment)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import '@/scss/global.scss';
</style>
