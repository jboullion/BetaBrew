<template>
  <div class="orders-container">
    <h1 id="ordersTitle" class="page-title">Sales Orders</h1>

    <table id="sales-orders" class="table" v-if="orders.length">
      <thead>
        <tr>
          <th>Name</th>
          <th>Total</th>
          <th>Paid</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="order in orders" :key="order.id">
          <td>
            {{ order.customer.firstName + ' ' + order.customer.lastName }}
          </td>
          <td>{{ orderTotal(order) | price }}</td>
          <td class="pay" @click="completeOrder(order.id)">
            <span class="lni lni-dollar green" v-if="order.isPaid"></span>
            <span class="lni lni-checkmark-circle blue" v-else></span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,
import { ISalesOrder } from '@/types/SalesOrder'

import BetaButton from '@/components/BetaButton.vue'
import { OrderService } from '@/services/order-service'

const orderService = new OrderService()

@Component({
  name: 'Orders',
  components: { BetaButton },
})
export default class Orders extends Vue {
  orders: ISalesOrder[] = []

  orderTotal(order: ISalesOrder): number {
    if (order.salesOrderItems) {
      return order.salesOrderItems.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      )
    }
    return 0
  }

  async completeOrder(orderId: number): Promise<void> {
    orderService.completeOrder(orderId).then(() => {
      let completedOrder = this.orders.find((order) => order.id == orderId)

      // Set our order to "paid"
      if (completedOrder) {
        completedOrder.isPaid = true
      }
    })
  }

  async initialize(): Promise<void> {
    this.orders = await orderService.getOrders()
  }

  async created(): Promise<void> {
    this.initialize()
  }
}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';

.pay {
  cursor: pointer;
  font-size: 1.2rem;
  text-align: center;
  width: 20px;
}
</style>
