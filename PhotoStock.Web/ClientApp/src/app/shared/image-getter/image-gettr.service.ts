import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class ImageGetterService {
  constructor(private http: HttpClient) {}

  public getImages(){
    return this.http.get(environment.apiUrl + 'photo/getimages');
  }
}
