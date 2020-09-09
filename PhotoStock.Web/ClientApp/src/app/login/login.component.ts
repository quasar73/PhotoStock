import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/authentication/authentication.service';
import { LoginStateService } from '../shared/authentication/login-state.service';
import { Login } from '../shared/models/login.model';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { RoleStateService } from '../shared/authentication/role-sate.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup = new FormGroup({
    "userName" : new FormControl("", [Validators.required]),
    "password": new FormControl("", [Validators.required])
  });
  message: string;
  inProgress: boolean = false;

  constructor(private router: Router,
    private authService: AuthenticationService,
    private loginStateService: LoginStateService,
    private roleStateService: RoleStateService) { }

  ngOnInit(): void {
  }

  public login() {
    this.inProgress = true;
    let loginVM: Login = {
      username: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value
    };
    this.authService
      .login(loginVM)
      .subscribe(() => {
        this.loginStateService.update(true);
        this.roleStateService.update();
        this.router.navigate(['/']);
        this.inProgress = false;
      },
      () => {
       this.message = 'Wrong login or passwrod!';
       this.inProgress = false;
      });
  }
}
