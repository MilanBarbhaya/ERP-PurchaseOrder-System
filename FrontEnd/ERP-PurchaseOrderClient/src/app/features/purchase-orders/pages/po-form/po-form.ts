import { Component, inject } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  FormArray,
  ReactiveFormsModule,Validators
}
from '@angular/forms';

import { CommonModule }
from '@angular/common';

import { Router }
from '@angular/router';

import { PurchaseOrderService }
from '../../services/purchase-order-service';

@Component({
  selector:'app-po-form',

  standalone:true,

  imports:[
    CommonModule,
    ReactiveFormsModule
  ],

  templateUrl:'./po-form.html',
   styleUrls: ['./po-form.css']

})
export class PoForm {
constructor() {

  this.addItem();
}
  private fb = inject(FormBuilder);

private service =
  inject(PurchaseOrderService);

private router =
  inject(Router);
  
  role =
    localStorage.getItem(
      'role'
    );
  form = this.fb.group({

  vendorName: [
    '',
    Validators.required
  ],
   department: [
    '',
    Validators.required
  ],

  lineItems:
    this.fb.array([])
});

get lineItems(): FormArray {

  return this.form.get(
    'lineItems'
  ) as FormArray;
}
addItem(): void {

  this.lineItems.push(

    this.fb.group({

      productName:[''],

      quantity:[1],

      unitPrice:[0]
    })
  );
}
removeItem(index:number): void {

  this.lineItems.removeAt(index);
}
save(): void {
  console.log('Save Clicked');
  console.log(this.form.value);
if (this.form.invalid) {

    this.form.markAllAsTouched();

    return;
  }
  const payload = {
     vendorName: this.form.value.vendorName,
  department: this.form.value.department,
  Items: (this.form.value.lineItems ?? []).map((x: any) => ({
    productName: x.productName,
    quantity: Number(x.quantity),
    unitPrice: Number(x.unitPrice)
  }))
  };

  this.service.create(payload).subscribe({
    next: () => {
      alert('Purchase Order Created');

      this.router.navigate(['/purchase-orders']);
    },
    error: (error) => {
      console.error(error);
    }
  });
}
}
// save(): void {
//  console.log('Save Clicked');
//   console.log(this.form.value);

//   this.service
//       .create(this.form.value)
//       .subscribe({

//         next:() => {

//           alert(
//             'Purchase Order Created'
//           );

//           this.router.navigate([
//             '/Create'
//           ]);
//         },

//         error:(error)=>{

//           console.log(error);
//         }
//       });
// }
// }
// @Component({
//   selector: 'app-po-form',
//   imports: [],
//   templateUrl: './po-form.html',
//   styleUrl: './po-form.css',
// })
// export class PoForm {}
