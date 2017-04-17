import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './../../../services/users/user.service';
import { AuthenticationService } from'./../../../services/general/authentication.service';
import { User } from './../../../DTO/users/user';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [UserService, AuthenticationService]
})
export class DashboardComponent implements OnInit {

  users: User[] = [];

  constructor(private _userService: UserService, private _authenticationService: AuthenticationService, private _router: Router) {
    if(!_authenticationService.isLoggedIn()) {
      this._router.navigate(['/login']);
    }

    _userService.getAll()
      .subscribe(users => this.users = users);
   }

  ngOnInit() {
  }

}
