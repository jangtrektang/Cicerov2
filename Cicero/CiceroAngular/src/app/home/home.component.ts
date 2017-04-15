import { Component, OnInit } from '@angular/core';
import { UserService } from './../services/users/user.service';
import { User } from './../DTO/users/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [UserService]
})
export class HomeComponent implements OnInit {

  users: User[] = [];
  constructor(private _userService: UserService) {
    _userService.getAll()
      .subscribe(users => this.users = users);
   }

  ngOnInit() {
  }

}
