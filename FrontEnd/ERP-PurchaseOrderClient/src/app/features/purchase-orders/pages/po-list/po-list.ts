import {
  ChangeDetectorRef,
  Component,
  inject,
  OnInit
  
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PurchaseOrderService }
from '../../services/purchase-order-service';
import {
  statusFilter,
  searchFilter
}
from '../../signals/po-filter.signal';
@Component({
  selector: 'app-po-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './po-list.html',
   styleUrls: ['./po-list.css']

})
export class PoListComponent implements OnInit{
  loading = false;
  errorMessage = '';
  private cdr = inject(ChangeDetectorRef);
statusFilter = statusFilter;
role =
    localStorage.getItem(
      'role'
    );
searchFilter = searchFilter;
  private service =
    inject(PurchaseOrderService);

    filteredPurchaseOrders: any[] = [];
  purchaseOrders: any[] = [];
  ngOnInit() {
this.loading = true;
  this.errorMessage = '';
   this.service.getAll()
    .subscribe(data => {

      this.purchaseOrders = data;

      this.applyFilters();

      this.cdr.detectChanges();

      this.loading = false;
    });
     this.loading = false;
  }
  
  setStatus(value: string) {

   this.statusFilter.set(value);

  this.applyFilters();
  }

  setSearch(event: Event) {
   const value =
    (event.target as HTMLInputElement).value;

  this.searchFilter.set(value);

  this.applyFilters();
 }
applyFilters(): void {

  const selectedStatus =
    this.statusFilter();

  const search =
    this.searchFilter()
      .toLowerCase();

  this.filteredPurchaseOrders =
    this.purchaseOrders.filter(po => {

      const statusMatch =
        !selectedStatus ||
        po.status.toString() === selectedStatus;

      const searchMatch =
        !search ||
        po.vendorName
          .toLowerCase()
          .includes(search)||
        po.poNumber
          .toLowerCase()
          .includes(search);

      return statusMatch &&
             searchMatch;
    });
}
  getStatusText(status: number): string {
  switch (status) {
    case 1: return 'Draft';
    case 2: return 'Submitted';
    case 3: return 'Approved';
    case 4: return 'Rejected';
    default: return '';
  }
}
}