import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './shared/authentication/authentication.service';
import { LoginStateService } from './shared/authentication/login-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [AuthenticationService, LoginStateService]
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  isAuth: boolean;
  isUser: boolean;

  constructor(private authService: AuthenticationService,
    private stateService: LoginStateService){}

  ngOnInit(): void {
    this.authService.isAuthorized().subscribe((result) => this.isAuth = result);
    this.isUser = this.authService.getRole() == 'user';
    this.stateService.getUpdater().subscribe((state) => this.isAuth = state);
  }

  logout(): void{
    this.authService.logout();
  }
}
