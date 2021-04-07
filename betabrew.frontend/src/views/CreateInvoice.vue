<template>
  <div class="create-invoice-container">
    <h1 id="createInvoiceTitle" class="page-title">Create Invoice</h1>

    <div class="invoice-step" v-if="invoiceStep === 1">
      <div class="invoice-step-detail">
        <h3>Step 1</h3>

        <label for="customer">Select Customer:</label>
        <select
          v-if="customers.length"
          v-model="selectedCustomer"
          id="customer"
        >
          <option>Select Customer</option>
          <option
            :value="customer.id"
            v-for="customer in customers"
            :key="customer.id"
          >
            {{ customer.firstName + ' ' + customer.lastName }}
          </option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 2">
      <div class="invoice-step-detail">
        <h3>Step 2</h3>

        <label for="product">Select Product:</label>
        <select v-model="newItem.Product" class="invoiceLineItem" id="product">
          <option>Select a Product</option>
          <option :value="item.id" v-for="item in inventory" :key="item.id">
            {{ item.product.name }}
          </option>
        </select>

        <label for="quantity">Quantity:</label>
        <input v-model="newItem.quantity" id="quantity" type="number" min="0" />
      </div>

      <div class="invoice-step-actions">
        <beta-button
          @button:click="addLineItem"
          :disabled="!newItem.Product || !newItem.quantity"
        >
          Add Line Item
        </beta-button>
        <beta-button
          @button:click="finalizeOrder"
          :disabled="!lineItems.length"
        >
          Finalize Order
        </beta-button>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 3">
      <div class="invoice-step-detail">
        <h3>Step 3</h3>
      </div>
    </div>

    <div class="invoice-step-actions">
      <beta-button @button:click="startOver">Start Over</beta-button>
      <beta-button @button:click="prev" :disabled="!canGoPrev">
        Previous
      </beta-button>
      <beta-button @button:click="next" :disabled="!canGoNext">
        Next
      </beta-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,

import BetaButton from '@/components/BetaButton.vue'

import { IInvoice, ISalesOrderItemModel } from '@/types/Invoice'
import { ICustomer } from '@/types/Customer'
import { IProduct, IProductInventory } from '@/types/Product'

import { InventoryService } from '@/services/inventory-service'
import { InvoiceService } from '@/services/invoice-service'
import { CustomerService } from '@/services/customer-service'

const customerService = new CustomerService()
const inventoryService = new InventoryService()
const invoiceService = new InvoiceService()

@Component({
  name: 'CreateInvoice',
  components: { BetaButton },
})
export default class CreateInvoice extends Vue {
  invoiceStep = 1
  maxInvoiceSteps = 3
  invoice: IInvoice = {
    customerId: 0,
    lineItems: [],
  }

  inventory: IProductInventory[] = []

  customers: ICustomer[] = []
  selectedCustomer = 0

  lineItems: ISalesOrderItemModel[] = []

  newProduct: IProduct = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    isTaxable: false,
    isArchived: false,
  }

  newItem: ISalesOrderItemModel = {
    product: this.newProduct,
    quantity: 0,
  }

  addLineItem(): void {
    let newItem: ISalesOrderItemModel = {
      product: this.newItem.product,
      quantity: parseInt(this.newItem.quantity.toString()),
    }

    let existingItems = this.lineItems.map((item) => item.product.id)

    if (existingItems.includes(newItem.product.id)){
      let lineItem = this.lineItems.find(
        (item) => item.product.id === newItem.product.id
      )

      if (lineItem) {
        //let currentQuantity = lineItem.quantity
        //let updatedQuantity = currentQuantity + this.newItem.quantity
        lineItem.quantity += parseInt(this.newItem.quantity.toString())
      }
    } else {
      this.lineItems.push(newItem)
    }

    this.newItem = {
      product: this.newProduct,
      quantity: 0,
    }
  }

  finalizeOrder(): void {
    this.invoiceStep = 3
  }

  prev(): void {
    if (this.invoiceStep === 1) return;
    this.invoiceStep -= 1
  }

  get canGoPrev(): boolean {
    if (this.invoiceStep > 1) return true
    return false
  }

  next(): void {
    if (this.invoiceStep == this.maxInvoiceSteps) return
    this.invoiceStep += 1
  }

  get canGoNext(): boolean {
    if (this.invoiceStep === 1 && this.selectedCustomer === 0) return false

    if (this.invoiceStep === 2) {
      return true
    }

    if (this.invoiceStep < this.maxInvoiceSteps) return true

    return false
  }

  startOver(): void {
    this.invoiceStep = 1
    this.selectedCustomer = 0

    this.lineItems = []

    this.newItem = {
      product: this.newProduct,
      quantity: 0,
    }
  }

  async updateInventory(): Promise<void> {
    this.inventory = await inventoryService.getInventory()
  }

  async updateCustomers(): Promise<void> {
    this.customers = await customerService.getCustomers()
  }

  async created(): Promise<void> {
    await this.updateInventory()
    await this.updateCustomers()
  }
}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';

.invoice-step {
  h3 {
    margin-bottom: 20px;
  }
}

.invoice-step-detail {
  padding: 15px;
  width: 100%;
  max-width: 300px;
}

.invoice-step-actions {
  padding: 15px;
  display: flex;
}
</style>
