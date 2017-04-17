import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { AuthUser } from './../../DTO/users/auth-user';
import { GlobalVariables } from './../../global-variables';
import { User } from './../../DTO/users/user';


@Injectable()
export class AuthenticationService {
  user: User;
  private _apiUrl = GlobalVariables.API_URL + 'token';
  private _clientId = GlobalVariables.CLIENT_ID;

  constructor(private _http : Http) { }

  logIn(authUser: AuthUser) {
    console.log(authUser);
    let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded'});
    let data = "grant_type=password&username=" + authUser.username + "&password=" + authUser.password + "&client_id=" + this._clientId;
    
    this._http.post(this._apiUrl, data, { headers: headers})
      .map((res:any) => res.json())
      .subscribe(
        data =>
          localStorage.setItem("authUser", JSON.stringify({ token: data.access_token, username: data.userName, refreshToken: data.refresh_token })),
        err =>
          console.log(err)
      );

      if(localStorage.getItem("authUser") != null) {
      return true;
    }

    return false;
  }

  logOut() {
    localStorage.removeItem("authUser");
  }

  isLoggedIn(){
    if(localStorage.getItem("authUser") != null) {
      return true;
    }

    return false;
  }

  getUserName() {
    // Check if logged in
    if(localStorage.getItem("authUser") != null) {
      let data = JSON.parse(localStorage.getItem("authUser"));
      console.log(data);
      console.log(data.username);
      return data.username;
    }
  }
}