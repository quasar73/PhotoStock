import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class AdminService {
  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get(environment.apiUrl + 'admin/GetUsers');
  }
}
