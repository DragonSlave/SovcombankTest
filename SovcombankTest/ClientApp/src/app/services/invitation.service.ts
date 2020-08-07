import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Invitation } from "../models/invitation";

@Injectable()
export class InvitationService {

  constructor(private http: HttpClient) { }

  invite(invitation: Invitation) {    
    return this.http.post('/invitation', invitation);
  }

}
