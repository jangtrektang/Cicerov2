import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { HttpclientService } from './../general/httpclient.service';
import { User } from './../../DTO/users/user';
import { GlobalVariables } from './../../global-variables';

@Injectable()
export class UserService {

  private apiUrl = GlobalVariables.API_URL + 'users';

  constructor(private http : Http) { }

  getAll(){
    return this.http
      .get(this.apiUrl)
      .map((res: any) => res.json());
  }

  getById(id: number) {
    return this.http
      .get(this.apiUrl + '/' + id)
      .map((res: any) => res.json());
  }

  getByUsername(username: string) {
    console.log("here i am  " + username);
    return this.http
      .get(this.apiUrl + '/' + username)
      .map((res: any) => res.json());
  }

  create(user: User){
    return this.http
      .post(this.apiUrl, user)
      .map((res: any) => res.json());
  }

  update(user: User) {
    return this.http
      .put(this.apiUrl + '/' + user.id, user)
      .map((res: any) => res.json());
  }

  delete(id: number) {
    return this.http
      .delete(this.apiUrl + '/' + id)
      .map((res: any) => res.json());
  }

}
