import { Component, OnInit } from '@angular/core';
import { Registration } from '../shared/models/registration.model';
import { AuthenticationService } from '../shared/authentication/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registration: Registration = new Registration();
  isRegistrationComplete: boolean = false;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
  }

  register(): void{
    this.authService.register(this.registration).subscribe(() => {
      this.isRegistrationComplete = true;
      this.registration = new Registration();
    });
  }
}
