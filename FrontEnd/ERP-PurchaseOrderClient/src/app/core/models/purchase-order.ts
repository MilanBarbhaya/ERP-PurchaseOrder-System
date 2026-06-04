export interface PurchaseOrder {

  id:string;

  poNumber:string;

  vendorName:string;

  department:string;

  totalAmount:number;

  status:number;
  
  lineItems: PurchaseOrderItem[]; 
}
export interface PurchaseOrderItem {

  productName: string;

  quantity: number;

  unitPrice: number;

  amount?: number; // optional if backend calculates it
}