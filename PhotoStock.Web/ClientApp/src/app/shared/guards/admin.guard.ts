import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import { AuthenticationService } from '../authentication/authentication.service';
import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class AdminGuard implements CanActivate {

  constructor(private authService: AuthenticationService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean> | boolean{
    return this.authService.isAuthorized() && this.authService.getRole() == 'admin';
  }
}
