import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/authentication/authentication.service';
import { LoginStateService } from '../shared/authentication/login-state.service';
import { Login } from '../shared/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  user: Login = new Login();
  message: string;

  constructor(private router: Router,
    private authService: AuthenticationService,
    private stateService: LoginStateService) { }

  ngOnInit(): void {
  }

  public login() {
    this.authService
      .login(this.user)
      .subscribe(() => {
        this.stateService.update(true);
        this.router.navigate(['/']);
      },
      () => {
       this.message = 'Wrong login or passwrod!';
      });
  }
}
