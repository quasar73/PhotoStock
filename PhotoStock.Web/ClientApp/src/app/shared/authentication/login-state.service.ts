import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class LoginStateService {
  constructor() { }

  private stateUpdater = new Subject<boolean>();

  update(state: boolean){
    this.stateUpdater.next(state);
  }

  getUpdater(): Subject<boolean>{
    return this.stateUpdater;
  }
}
