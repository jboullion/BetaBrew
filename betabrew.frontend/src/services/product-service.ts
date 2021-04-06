import { IProduct } from '@/types/Product'
import axios from 'axios'

/**
 * Product Service
 * Provides UI business logic related to product Product
 */
export class ProductService {
  API_URL = process.env.VUE_APP_API_URL

  public async archiveProduct(productId: number) {
    const result = await axios.patch(`${this.API_URL}/product/` + productId)

    return result.data
  }

  public async saveNewProduct(product: IProduct) {
    const result = await axios.post(`${this.API_URL}/product/`, product)

    return result.data
  }
}
