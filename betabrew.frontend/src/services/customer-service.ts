import { ICustomer } from '@/types/Customer'
import axios from 'axios'

/**
 * Customer Service
 * Provides UI business logic related to Customer
 */
export class CustomerService {
  API_URL = process.env.VUE_APP_API_URL

  public async getCustomers(): Promise<ICustomer[]> {
    const result = await axios.get(`${this.API_URL}/customer/`)
    return result.data
  }

  public async deleteCustomer(customerId: number) {
    const result = await axios.delete(`${this.API_URL}/customer/` + customerId)

    return result.data
  }

  public async createCustomer(customer: ICustomer) {
    const result = await axios.post(`${this.API_URL}/customer/`, customer)

    return result.data
  }
}
