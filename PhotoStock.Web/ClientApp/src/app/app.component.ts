import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './shared/authentication/authentication.service';
import { LoginStateService } from './shared/authentication/login-state.service';
import { RoleStateService } from './shared/authentication/role-sate.service';

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
    private loginStateService: LoginStateService,
    private roleStateService: RoleStateService){}

  ngOnInit(): void {
    this.authService.isAuthorized().subscribe((result) => this.isAuth = result);
    this.authService.getRole().subscribe((role) => this.isUser = role == 'user');
    this.roleStateService.getUpdater().subscribe((role) => {
      this.isUser = role == 'user';
    })
    this.loginStateService.getUpdater().subscribe((state) => this.isAuth = state);
  }

  logout(): void{
    this.authService.logout();
  }
}
