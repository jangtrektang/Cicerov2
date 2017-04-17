import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './../../services/general/authentication.service';
import { UserService } from './../../services/users/user.service';
import { User } from './../../DTO/users/user';

@Component({
  selector: 'app-adminlayout',
  templateUrl: './adminlayout.component.html',
  styleUrls: ['./adminlayout.component.css'],
  providers: [AuthenticationService, UserService]
})
export class AdminlayoutComponent implements OnInit {

  user: User;
  constructor(private _authenticationService: AuthenticationService, private _userService: UserService, private _router: Router) {
    let userName = this._authenticationService
      .getUserName(); 
    
    // Get user data
    this._userService
      .getByUsername(userName)
      .subscribe(user => this.user = user);
  }

  logOut() {
    this._authenticationService.logOut();
    this._router.navigate(['/login']);
  }

  ngOnInit() {
  }

}
