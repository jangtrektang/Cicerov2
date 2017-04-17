import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { routing } from './app.routing';

import { AppComponent } from './app.component';
import { LoginComponent } from './users/login/login.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './users/signup/signup.component';
import { AdminlayoutComponent } from './layout/adminlayout/adminlayout.component';
import { AdminbreadcrumbComponent } from './layout/adminbreadcrumb/adminbreadcrumb.component';
import { DashboardComponent } from './components/admin/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoginComponent,
    HomeComponent,
    SignupComponent,
    AdminlayoutComponent,
    AdminbreadcrumbComponent,
    DashboardComponent    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    routing
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
