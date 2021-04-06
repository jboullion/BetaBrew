<template>
  <beta-modal>
    <template v-slot:header>New Customer</template>
    <template v-slot:body>
      <ul class="new-customer-list">
        <li>
          <label for="first-name">First Name:</label>
          <input type="text" id="first-name" v-model="newCustomer.firstName" />
        </li>
        <li>
          <label for="last-name">Last Name:</label>
          <input type="text" id="last-name" v-model="newCustomer.lastName" />
        </li>

        <li>
          <label for="address">Address 1:</label>
          <input
            type="text"
            id="address"
            v-model="newCustomer.primaryAddress.addressLine1"
          />
        </li>
        <li>
          <label for="address-2">Address 2:</label>
          <input
            type="text"
            id="address-2"
            v-model="newCustomer.primaryAddress.addressLine2"
          />
        </li>
        <li>
          <label for="city">City:</label>
          <input
            type="text"
            id="city"
            v-model="newCustomer.primaryAddress.city"
          />
        </li>
        <li>
          <label for="state">State:</label>
          <select v-model="newCustomer.primaryAddress.state">
            <option value="AZ">Arizona</option>
            <option value="CA">California</option>
            <option value="MN">Minnesota</option>
            <option value="NY">New York</option>
            <option value="WI">Wisconsin</option>
          </select>
        </li>
        <li>
          <label for="zip">Zip:</label>
          <input
            type="text"
            id="zip"
            v-model="newCustomer.primaryAddress.postalCode"
          />
        </li>

      </ul>
    </template>
    <template v-slot:footer>
      <beta-button
        type="button"
        @click.native="save"
        aria-label="Add new customer"
        >Add Customer</beta-button
      >
      <beta-button
        type="button"
        @button:click="close"
        aria-label="Close customer modal"
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
import { ICustomer } from '@/types/Customer'

@Component({
  name: 'NewCustomerModal',
  components: { BetaButton, BetaModal },
})
export default class NewCustomerModal extends Vue {
  newCustomer: ICustomer = {
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    firstName: '',
    lastName: '',
    primaryAddress: {
      createdOn: new Date(),
      updatedOn: new Date(),
      addressLine1: '',
      addressLine2: '',
      city: '',
      state: '',
      postalCode: '',
      country: '',
    }
  }

  close(): void {
    this.$emit('close')
  }

  save(): void {
    this.$emit('save:customer', this.newCustomer)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import '@/scss/global.scss';

.new-customer-list {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>
