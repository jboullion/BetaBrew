<template>
  <div v-if="timelineBuilt">
    <apexchart
      width="100%"
      height="300"
      type="area"
      :options="options"
      :series="series"
    ></apexchart>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator' //  Watch
import { Sync, Get } from 'vuex-pathify'

import VueApexCharts from 'vue-apexcharts'
//import BetaButton from '@/components/BetaButton.vue'

// import { InventoryService } from '@/services/inventory-service'
// import { ProductService } from '@/services/product-service'

// import { IProduct, IProductInventory } from '@/types/Product'
import { IInventoryTimeline } from '@/types/InventoryGraph'

// const inventoryService = new InventoryService()
// const productService = new ProductService()

Vue.component('apexchart', VueApexCharts)

@Component({
  name: 'InventoryChart',
  components: {  },
})
export default class InventoryChart extends Vue {
  // options = {
  //   chart: {
  //     id: 'vuechart-example',
  //   },
  //   xaxis: {
  //     categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998],
  //   },
  // }

  // series = [
  //   {
  //     name: 'series-1',
  //     data: [30, 40, 45, 50, 49, 60, 70, 91],
  //   },
  // ]

  @Sync('snapshotTimeline')
  snapshotTimeline?: IInventoryTimeline

  @Get('isTimelineBuilt')
  timelineBuilt?: boolean

  async created(): Promise<void> {
    await this.$store.dispatch('assignSnapshots')
  }

  // eslint-disable-next-line @typescript-eslint/ban-types
  get options(): object {
    return {
      dataLabels: { enabled: false },
      fill: {
        type: 'gradient',
      },
      stroke: {
        curve: 'smooth',
      },
      xaxis: {
        type: 'datetime',
        categories: this.snapshotTimeline
          ? this.snapshotTimeline.timeline
          : null,
      },
    }
  }

  // eslint-disable-next-line @typescript-eslint/ban-types
  // eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
  get series() {
    return this.snapshotTimeline?.productInventorySnapshots.map((snapshot) => ({
      name: snapshot.productId,
      data: snapshot.quantityOnHand,
    }))
  }
}
</script>

<style scoped lang="scss">
@import '@/scss/global.scss';
</style>
