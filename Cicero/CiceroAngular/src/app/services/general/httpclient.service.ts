import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

@Injectable()
export class HttpclientService {

  constructor(private http: Http) { }

  createAuthorizationHeader(header: Headers){
    if(localStorage.getItem("authorizationData") != null) {
      let data = JSON.parse(localStorage.getItem("authorizationData"));
      header.append('Authorization', `Bearer ${data.token}`);
    }
  }

  get(url: string) {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);

    return this.http.get(url, { headers: headers });
  }

  post(url: string, data: any) {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);

    return this.http.post(url, data, { headers: headers });
  }

  put(url: string, data: any) {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);

    return this.http.post(url, data, { headers: headers });
  }

  delete(url: string){
    let headers = new Headers();
    this.createAuthorizationHeader(headers);

    return this.http.delete(url, { headers: headers });
  }

}
