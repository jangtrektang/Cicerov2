import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminlayoutComponent } from './layout/adminlayout/adminlayout.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './users/login/login.component';
import { SignupComponent } from './users/signup/signup.component';

const appRoutes: Routes = [
  {
      path: 'admin', 
      redirectTo: 'admin/dashboard', 
      pathMatch: 'full'},
  {
    path: 'admin',
    children: [
      {
        path: '', component: AdminlayoutComponent,
        children: [
            {
              path: 'dashboard', 
              component: HomeComponent
            }
        ]
      }      
    ]
  },
  {
      path: 'login',
      component: LoginComponent
  },
  {
      path: 'signup',
      component: SignupComponent
  }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);