import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './core/guards/auth/auth.guard';
import { loginGuard } from './core/guards/login.guard';

export const routes: Routes = [

  {
    path: 'login',
    loadComponent: () =>
      import(
        './features/auth/login/login.component'
      ).then(
        c => c.LoginComponent
      ),
      canActivate: [loginGuard]
  },
  {
    path:'purchase-orders',

    loadComponent:() =>
      import(
        './features/purchase-orders/pages/po-list/po-list'
      ).then(
        c => c.PoListComponent
      ),

    canActivate:[authGuard]
  },

  {
    path:'purchase-orders/create',

    loadComponent:() =>
      import(
        './features/purchase-orders/pages/po-form/po-form'
      ).then(
        c => c.PoForm
      ),

    canActivate:[authGuard]
  },
{
  path: 'purchase-orders/:id',
  loadComponent: () =>
    import('./features/purchase-orders/pages/po-detail/po-detail')
      .then(c => c.PoDetailComponent),
 canActivate:[authGuard]

},
  // {
  //   path:'purchase-orders/:id',

  //   loadComponent:() =>
  //     import(
  //       './features/purchase-orders/pages/po-detail/po-detail'
  //     ).then(
  //       c => c.PoDetailComponent
  //     ),

  //   canActivate:[authGuard]
  // },

  {
    path:'',

    redirectTo:'login',

    pathMatch:'full'
  }
];
