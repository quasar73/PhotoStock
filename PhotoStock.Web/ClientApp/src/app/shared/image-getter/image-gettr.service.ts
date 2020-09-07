import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class ImageGetterService {
  constructor(private http: HttpClient) {}

  public getImages(category: string){
    return this.http.get(environment.apiUrl + 'photo/getimages', {params: {['category']: category}});
  }
}
