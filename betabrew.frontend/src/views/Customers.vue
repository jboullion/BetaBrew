<template>
  <div class="customers-container">
    <h1 id="customersTitle" class="page-title">Customers</h1>

    <div class="customers-actions">
      <beta-button id="addNewBtn" @button:click="showNewCustomerModal">
        Add New Customer
      </beta-button>
    </div>

    <table id="customersTable" class="table">
      <tr>
        <th>Name</th>
        <th>Address</th>
        <th>City</th>
        <th>State</th>
        <th>Zip</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="customer in customers" :key="customer.id">
        <td>{{ customer.firstName + ' ' + customer.lastName }}</td>
        <td>
          {{ customer.primaryAddress.addressLine1 }}
          {{ customer.primaryAddress.addressLine2 }}
        </td>
        <td>{{ customer.primaryAddress.city }}</td>
        <td>{{ customer.primaryAddress.state }}</td>
        <td>{{ customer.primaryAddress.postalCode }}</td>
        <td>{{ customer.createdOn | humanizeDate }}</td>
        <td
          class="lni lni-cross-circle customer-archive"
          @click="deleteCustomer(customer.id)"
        ></td>
      </tr>
    </table>
    <new-customer-modal
      v-if="isNewCustomerVisible"
      @save:customer="createCustomer"
      @close="closeModals"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch,
import { ICustomer } from '@/types/Customer'

import BetaButton from '@/components/BetaButton.vue'
import NewCustomerModal from '@/components/Modals/NewCustomerModal.vue'
import { CustomerService } from '@/services/customer-service'

const customerService = new CustomerService()

@Component({
  name: 'Customers',
  components: { BetaButton, NewCustomerModal },
})
export default class Customers extends Vue {
  isNewCustomerVisible = false

  customers: ICustomer[] = []

  showNewCustomerModal(): void {
    this.isNewCustomerVisible = true
  }

  closeModals(): void {
    this.isNewCustomerVisible = false
  }

  async deleteCustomer(customerId: number): Promise<void> {
    //this.fade[customerId] = true
    await customerService.deleteCustomer(customerId)
    await this.updateCustomers()
  }

  async createCustomer(customer: ICustomer): Promise<void> {
    await customerService.createCustomer(customer)
    await this.updateCustomers()
  }

  async updateCustomers(): Promise<void> {
    this.customers = await customerService.getCustomers()
    this.isNewCustomerVisible = false
  }

  async created(): Promise<void> {
    await this.updateCustomers()
  }
}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';

.customers-actions {
  display: flex;
  margin-bottom: 0.8rem;
}

.customer-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $brand-red;
}
</style>
