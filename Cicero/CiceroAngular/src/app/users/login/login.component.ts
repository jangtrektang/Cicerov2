import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './../../services/general/authentication.service';
import { AuthUser } from './../../DTO/users/auth-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [AuthenticationService]
})
export class LoginComponent implements OnInit {

  authUser = new AuthUser();
  showForgotPassword: boolean;
  error: string;
  showError: boolean;

  constructor(private _authenticationService: AuthenticationService, private _router: Router) {
    // Hide forgot password
    this.showForgotPassword = false;
   }

  logIn() {
    // Login
    if(this._authenticationService
      .logIn(this.authUser)) {
        this._router.navigate(['/admin/dashboard']);
      }   
  }

  toggleForgotPassword(){
    this.showForgotPassword = !this.showForgotPassword;
  }

  forgotPassword(email: string) {

  }

  ngOnInit() {
  }

}