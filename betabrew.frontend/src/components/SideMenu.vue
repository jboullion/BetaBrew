<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img id="img-logo" alt="Beta Brew" src="@/assets/images/logo.png" />
    </router-link>
    <h1>Management Portal</h1>

    <beta-button
      id="menuInventory"
      is-full-width
      @button:click="goToRoute('/inventory')"
    >
      Inventory
    </beta-button>
    <beta-button
      id="menuCustomers"
      is-full-width
      @button:click="goToRoute('/customers')"
    >
      Customers
    </beta-button>
    <beta-button
      id="menuInvoice"
      is-full-width
      @button:click="goToRoute('/invoice/new')"
    >
      New Invoice
    </beta-button>
    <beta-button
      id="menuOrders"
      is-full-width
      @button:click="goToRoute('/orders')"
    >
      Orders
    </beta-button>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BetaButton from "@/components/BetaButton.vue";

@Component({
  name: "SideMenu",
  components: { BetaButton },
})
export default class SideMenu extends Vue {
  goToRoute(route: string): void {
    this.$router.push({ path: route });
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import "@/scss/global.scss";

.side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 10px;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}

#img-logo {
}

h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>
