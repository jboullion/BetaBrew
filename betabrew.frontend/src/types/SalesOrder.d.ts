import { ICustomer } from "./Customer";
import { ISalesOrderItemModel } from "./Invoice";

export interface ISalesOrder {
  id: number
  createdOn: Date
  updatedOn: Date
  customer: ICustomer
  salesOrderItems: ISalesOrderItemModel[]
  isPaid: boolean
}