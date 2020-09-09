import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {map, withLatestFrom} from "rxjs/operators";
import { AuthenticationService } from '../authentication/authentication.service';
import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthenticationService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean> | boolean{
    return this.authService.isAuthorized().pipe(withLatestFrom(this.authService.getRole()), map(([isAuthorized, role]) => isAuthorized && role == route.data.role));
  }
}
