import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [UserService]
})
export class HomeComponent {

  phone: string;

  constructor(private userService: UserService) {
  }

  onApprove() {

    let userDto = new User();

    userDto.phoneNumber = this.phone;

    this.userService.approve(userDto).subscribe(
      data => console.log(data)
    );
  }
}
