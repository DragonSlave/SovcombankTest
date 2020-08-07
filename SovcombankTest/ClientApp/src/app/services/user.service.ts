import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "../models/user";

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  approve(user: User) {
    return this.http.post('/user', user );
  }

}
