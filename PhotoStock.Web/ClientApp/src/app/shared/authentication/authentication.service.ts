import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, map, switchMap, catchError } from 'rxjs/operators';
import { AuthService } from 'ngx-auth';
import { Login } from '../models/login.model';
import { Registration } from '../models/registration.model';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from "@auth0/angular-jwt";
import { TokenStorage } from './token-storage.service';

const jwtHelper = new JwtHelperService();

interface AccessData {
  token: string;
  message: string;
}

@Injectable({providedIn: 'root'})
export class AuthenticationService implements AuthService {

  constructor(
    private http: HttpClient,
    private tokenStorage: TokenStorage
  ) {}

  public isAuthorized(): Observable < boolean > {
    return this.tokenStorage
      .getAccessToken()
      .pipe(map(token => !!token));
  }


  public getAccessToken(): Observable < string > {
    return this.tokenStorage.getAccessToken();
  }

  public refreshToken(): Observable <AccessData> {
    return this.tokenStorage
      .getRefreshToken()
      .pipe(
        switchMap((refreshToken: string) =>
          this.http.post(environment.apiUrl + 'account/refresh', { refreshToken })
        ),
        tap((tokens: AccessData) => this.saveAccessData(tokens)),
        catchError((err) => {
          this.logout();

          return Observable.throw(err);
        })
      );
  }

  public refreshShouldHappen(response: HttpErrorResponse): boolean {
    return response.status === 401
  }

  public verifyTokenRequest(url: string): boolean {
    return url.endsWith('/refresh');
  }


  public login(loginVM: Login): Observable<any> {
    return this.http.post(environment.apiUrl + 'auth/login', loginVM)
    .pipe(tap((tokens: AccessData) => this.saveAccessData(tokens)));
  }

  public register(registrationVM: Registration) {
    return this.http.post(environment.apiUrl + 'auth/register', registrationVM);
  }

  public logout(): void {
    this.tokenStorage.clear();
    location.reload(true);
  }

  private saveAccessData({ token , message }: AccessData) {
    this.tokenStorage
      .setAccessToken(token);
  }


  public getRole(): Observable<string>{
    return this.tokenStorage.getAccessToken().pipe(map(token => jwtHelper.decodeToken(token).role))
  }
}
