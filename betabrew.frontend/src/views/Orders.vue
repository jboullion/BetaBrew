<template>
  <div class="orders-container">
    <h1 id="ordersTitle" class="page-title">Orders</h1>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,
//import { IOrder } from '@/types/Order'

import BetaButton from '@/components/BetaButton.vue'
// import { OrderService } from '@/services/order-service'

//const orderService = new OrderService()

@Component({
  name: 'Orders',
  components: { BetaButton },
})
export default class Orders extends Vue {}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';
</style>
