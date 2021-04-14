import { make } from 'vuex-pathify'

import { IInventoryTimeline } from '@/types/InventoryGraph'
import { InventoryService } from '@/services/inventory-service'

class GlobalStore {
  snapshotTimeline: IInventoryTimeline = {
    productInventorySnapshots: [],
    timeline: [],
  }

  isTimelineBuilt = false
}

const state = new GlobalStore()

const mutations = make.mutations(state)

const actions = {
  async assignSnapshots( context: { commit: (arg0: string, arg1: boolean | IInventoryTimeline) => void } ) {
    const inventoryService = new InventoryService()
    const res = await inventoryService.getSnapshotHistory()

    const timeline: IInventoryTimeline = {
      productInventorySnapshots: res.productInventorySnapshots,
      timeline: res.timeline,
    }

    context.commit('SET_SNAPSHOT_TIMELINE', timeline)
    context.commit('SET_IS_TIMELINE_BUILT', true)
  },
}

const getters = {}

export default {
  state,
  mutations,
  actions,
  getters,
}