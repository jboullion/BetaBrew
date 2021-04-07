import { IProduct } from '@/types/Product'

export interface IInvoice {
  customerId: number
  lineItems: ISalesOrderItemModel[]
}

export interface ISalesOrderItemModel {
  quantity: number
  product: IProduct
}
