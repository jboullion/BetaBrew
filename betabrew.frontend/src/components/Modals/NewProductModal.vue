<template>
  <beta-modal>
    <template v-slot:header>New Product</template>
    <template v-slot:body>
      <ul class="new-product-list">
        <li>
          <label for="isTaxable">Is Taxable?:</label>
          <input
            type="checkbox"
            id="isTaxable"
            v-model="newProduct.isTaxable"
          />
        </li>
        <li>
          <label for="productName">Name:</label>
          <input type="text" id="productName" v-model="newProduct.name" />
        </li>
        <li>
          <label for="productDescription">Description:</label>
          <textarea
            id="productDescription"
            v-model="newProduct.description"
          ></textarea>
        </li>
        <li>
          <label for="productPrice">Price (USD):</label>
          <input type="number" id="productPrice" v-model="newProduct.price" />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <beta-button
        type="button"
        @click.native="save"
        aria-label="Add new product"
        >Add Product</beta-button
      >
      <beta-button
        type="button"
        @button:click="close"
        aria-label="Close product modal"
        >Close</beta-button
      >
    </template>
  </beta-modal>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
// import { Prop } from "vue-property-decorator";
import BetaButton from '@/components/BetaButton.vue'
import BetaModal from '@/components/Modals/BetaModal.vue'
import { IProduct } from '@/types/Product'

@Component({
  name: 'NewProductModal',
  components: { BetaButton, BetaModal },
})
export default class NewProductModal extends Vue {
  newProduct: IProduct = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    isTaxable: false,
    isArchived: false,
  }

  close(): void {
    this.$emit('close')
  }

  save(): void {
    this.$emit('save:product', this.newProduct)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import '@/scss/global.scss';

.new-product-list {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>
