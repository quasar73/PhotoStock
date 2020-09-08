import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({providedIn: 'root'})
export class ImageService {
  constructor(private http: HttpClient) {}

  public uploadImage(image: File, category: string){
    const formData = new FormData();
    formData.append('File', image);
    formData.append('Category', category);
    return this.http.post(environment.apiUrl + 'photo/import', formData);
  }

  public getImages(category: string){
    return this.http.get(environment.apiUrl + 'photo/getimages', {params: {['category']: category}});
  }
}
