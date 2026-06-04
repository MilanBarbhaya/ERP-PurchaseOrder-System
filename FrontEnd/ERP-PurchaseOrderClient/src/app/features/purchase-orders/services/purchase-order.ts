import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PurchaseOrder }
from '../../../core/models/purchase-order';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService {

  private http = inject(HttpClient);

  private apiUrl =
    'http://localhost:5237/api/v1/purchaseorders';

  getAll() {
    return this.http.get<PurchaseOrder[]>(
      this.apiUrl
    );
  }

  getById(id: string) {
    return this.http.get<PurchaseOrder>(
      `${this.apiUrl}/${id}`
    );
  }

  create(data: any) {
    return this.http.post(
      this.apiUrl,
      data
    );
  }

  submit(id: string) {
    return this.http.put(
      `${this.apiUrl}/${id}/submit`,
      {}
    );
  }

  approve(id: string) {
    return this.http.put(
      `${this.apiUrl}/${id}/approve`,
      {}
    );
  }

  reject(id: string) {
    return this.http.put(
      `${this.apiUrl}/${id}/reject`,
      {}
    );
  }
   update(id: string, data: any) {
  return this.http.put(
    `${this.apiUrl}/${id}`,
    data
  );
}
}