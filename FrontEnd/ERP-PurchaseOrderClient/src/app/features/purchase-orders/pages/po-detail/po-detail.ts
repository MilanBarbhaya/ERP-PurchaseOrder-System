import {
  ChangeDetectorRef,
 Component,
 inject,
 OnInit
}
from '@angular/core';

import {
 CommonModule
}
from '@angular/common';

import {
 ActivatedRoute,RouterModule
}
from '@angular/router';
import {  FormBuilder,
  ReactiveFormsModule,
  Validators,FormsModule } from '@angular/forms';
import {
 PurchaseOrderService
}
from '../../services/purchase-order-service';

@Component({
  selector:'app-po-detail',

  standalone:true,

  imports:[CommonModule, RouterModule,ReactiveFormsModule],

  templateUrl:'./po-detail.html',
   styleUrls: ['./po-detail.css']
})
export class PoDetailComponent
implements OnInit {

  private fb = inject(FormBuilder);
private cdr = inject(ChangeDetectorRef);

editForm = this.fb.group({

  vendorName: [
    '',
    Validators.required
  ],

  department: [
    '',
    Validators.required
  ]
});
  private route =
    inject(ActivatedRoute);

  private service =
    inject(PurchaseOrderService);
loading = true;
 
  purchaseOrder: any = null;
  isEditMode = false;
  role =
    localStorage.getItem(
      'role'
    );

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) return;

    this.service.getById(id).subscribe({
      
      next: (res) => {
         console.log('API:', res);
        this.purchaseOrder = {...res};  
        this.loading = false;
        this.cdr.detectChanges();
        console.log('purchaseOrder value:', this.purchaseOrder);
      },
      error: () => {
        this.loading = false;
      }
    });
}
   enableEdit() {
    if (this.purchaseOrder.status !== 1) return;
    this.isEditMode = true;
  }

  cancelEdit() {
    this.isEditMode = false;
  }
 saveEdit() {

  if (this.editForm.invalid) {

    this.editForm.markAllAsTouched();

    return;
  }

  const payload = {

    ...this.purchaseOrder,

    vendorName:
      this.editForm.value.vendorName,

    department:
      this.editForm.value.department
  };

  this.service
      .update(
        this.purchaseOrder.id,
        payload
      )
      .subscribe({

        next: () => {

          this.isEditMode = false;
  this.cdr.detectChanges();
          this.loadPurchaseOrder(
            this.purchaseOrder.id
          );

          alert(
            'Updated Successfully'
          );
        }
      });
}
  loadPurchaseOrder(
    id:string
  )
  {
    this.service
        .getById(id)
        .subscribe({

          next:(data)=>{

            this.purchaseOrder =
              data;
          }
        });
  }

  submit()
  {
    this.service
        .submit(
          this.purchaseOrder.id
        )
        .subscribe({

          next:()=>{

            alert(
              'PO Submitted'
            );
        this.cdr.detectChanges();

            this.loadPurchaseOrder(
              this.purchaseOrder.id
            );
          }
        });
  }

  approve()
  {
    this.service
        .approve(
          this.purchaseOrder.id
        )
        .subscribe({

          next:()=>{

            alert(
              'PO Approved'
            );
        this.cdr.detectChanges();

            this.loadPurchaseOrder(
              this.purchaseOrder.id
            );
          }
        });
  }

  reject()
  {
    this.service
        .reject(
          this.purchaseOrder.id
        )
        .subscribe({

          next:()=>{

            alert(
              'PO Rejected'
            );
        this.cdr.detectChanges();

            this.loadPurchaseOrder(
              this.purchaseOrder.id
            );
          }
        });
  }
  reload() {
    this.service.getById(this.purchaseOrder.id)
      .subscribe(res => this.purchaseOrder = res);
        this.cdr.detectChanges();

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