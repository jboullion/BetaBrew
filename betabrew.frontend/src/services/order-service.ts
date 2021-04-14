import { ISalesOrder } from '@/types/SalesOrder'

import axios from 'axios'

/**
 * Order Service
 * Provides UI business logic related to Sales Orders
 */
export class OrderService {
  API_URL = process.env.VUE_APP_API_URL

  public async getOrders(): Promise<ISalesOrder[]> {
    const result = await axios.get(`${this.API_URL}/order/`)
    return result.data
  }

  public async completeOrder(orderId: number): Promise<boolean> {
    const result = await axios.patch(
      `${this.API_URL}/order/complete/${orderId}`
    )

    return result.data
  }
}
