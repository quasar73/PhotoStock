import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';

@Injectable({providedIn: 'root'})
export class RoleStateService {
  constructor(private authService: AuthenticationService) { }

  private stateUpdater = new Subject<string>();

  update(){
    this.authService.getRole().subscribe((role) => {
      this.stateUpdater.next(role);
    });
  }

  getUpdater(): Subject<string>{
    return this.stateUpdater;
  }
}
