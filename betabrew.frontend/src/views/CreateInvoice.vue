<template>
  <div class="create-invoice-container">
    <h1 id="createInvoiceTitle" class="page-title">Create Invoice</h1>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,
//import { IOrder } from '@/types/Order'

import BetaButton from '@/components/BetaButton.vue'
// import { OrderService } from '@/services/order-service'

//const orderService = new OrderService()

@Component({
  name: 'CreateInvoice',
  components: { BetaButton },
})
export default class CreateInvoice extends Vue {}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';
</style>
