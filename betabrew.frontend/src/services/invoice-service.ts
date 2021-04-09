import { IInvoice } from '@/types/Invoice'
// import { IServiceResponse } from '@/types/ServiceResponse'
import axios from 'axios'

/**
 * Invoice Service
 * Provides UI business logic related to Invoices
 */
export class InvoiceService {
  API_URL = process.env.VUE_APP_API_URL

  public async makeNewInvoice(invoice: IInvoice): Promise<IInvoice[]> {
    const result = await axios.post(`${this.API_URL}/invoice/`, invoice)
    return result.data
  }
}
