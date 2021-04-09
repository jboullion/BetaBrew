<template>
  <div id="lineItems" v-if="lineItems.length">
    <table class="table">
      <thead>
        <tr>
          <th v-if="invoiceStep < 3"></th>
          <th>Name</th>
          <th>Price</th>
          <th>Qty</th>
          <th style="text-align: center">Subtotal</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="lineItem in lineItems" :key="lineItem.product.id">
          <td
            v-if="invoiceStep < 3"
            class="remove"
            @click="deleteLineItem(lineItem.product.id)"
          >
            <span class="lni lni-cross-circle"></span>
          </td>
          <td>{{ lineItem.product.name }}</td>
          <td>{{ lineItem.product.price | price }}</td>
          <td>{{ lineItem.quantity }}</td>
          <td style="text-align: center">
            {{ (lineItem.product.price * lineItem.quantity) | price }}
          </td>
        </tr>
      </tbody>
      <tfoot>
        <tr>
          <th :colspan="invoiceStep < 3 ? 4 : 3" style="text-align: right">
            Total
          </th>
          <th style="text-align: center">
            {{ runningTotal | price }}
          </th>
        </tr>
      </tfoot>
    </table>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop } from 'vue-property-decorator'

import { ISalesOrderItemModel } from '@/types/Invoice'

@Component({
  name: 'LineItems',
  components: {},
})
export default class LineItems extends Vue {
  @Prop({ required: true, type: Array, default: false })
  lineItems?: ISalesOrderItemModel[]

  @Prop({ required: true, type: Number, default: 1 })
  invoiceStep?: number

  get runningTotal(): number {
    if (this.lineItems) {
      return this.lineItems.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      )
    }
    return 0
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import '@/scss/global.scss';

#lineItems {
  padding: 15px 0;

  .remove {
    cursor: pointer;
    font-weight: bold;
    font-size: 1.2rem;
    color: $brand-red;
    text-align: center;
  }
}
</style>
