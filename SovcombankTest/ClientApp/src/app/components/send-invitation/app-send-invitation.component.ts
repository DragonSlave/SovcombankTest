import { Component } from '@angular/core';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../../models/invitation';

@Component({
  selector: 'app-send-invitation',
  templateUrl: './app-send-invitation.component.html',
  providers: [InvitationService]
})

export class SendInvitationComponent {

  message: string;
  phone: string;
  phones :string[] = [];

  constructor(private invitationService: InvitationService) {

  }


  onClick() {    
    this.phones.push(this.phone);
    this.phone = '';
  }

  onSend() {

    let invitationDto = new Invitation();

    invitationDto.message = this.message;
    invitationDto.phoneNumbers = this.phones;

    this.invitationService.invite(invitationDto).subscribe(
      data => console.log(data)
    );

  }
}
