import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminsidebarComponent } from './layout/adminsidebar/adminsidebar.component';
import { AdminheaderComponent } from './layout/adminheader/adminheader.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './users/login/login.component';
import { SignupComponent } from './users/signup/signup.component';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home', children:[
     { path: 'login', component: LoginComponent},
     { path: 'signup', component: SignupComponent},
     { path: '' , component: AdminheaderComponent, outlet: 'admin-header'},
     { path: '' , component: AdminsidebarComponent, outlet: 'admin-sidebar'}]
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