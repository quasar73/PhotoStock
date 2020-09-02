import { Component, OnInit } from '@angular/core';
import { Registration } from '../shared/models/registration.model';
import { AuthenticationService } from '../shared/authentication/authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup = new FormGroup({
    "userName": new FormControl("", [Validators.required]),
    "email": new FormControl("", [Validators.required, Validators.email]) ,
    "password": new FormControl("", [Validators.required]),
    "confirmPassword": new FormControl("", [Validators.required])
  });
  isRegistrationComplete: boolean = false;
  inProgress: boolean = false;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
  }

  register(): void{
    let registrationVM: Registration = {
      username: this.registrationForm.controls['userName'].value,
      email: this.registrationForm.controls['email'].value,
      password: this.registrationForm.controls['password'].value,
      confirmPassword: this.registrationForm.controls['confirmPassword'].value
    }
    this.inProgress = true;
    this.authService.register(registrationVM).subscribe(() => {
      this.isRegistrationComplete = true;
      this.inProgress = false;
    },
    () => {
      this.inProgress = false;
    });
  }
}
