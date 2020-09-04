import { Component, OnInit } from '@angular/core';
import { Photo } from '../shared/models/photo.model';
import { ImageGetterService } from '../shared/image-getter/image-gettr.service'
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss'],
  providers: [ImageGetterService]
})
export class PhotoComponent implements OnInit {

  photoList: Photo[];
  serverUrl: string = environment.serverUrl;

  constructor(private getterService: ImageGetterService) { }

  ngOnInit(): void {
    this.getterService.getImages().subscribe((images: Photo[]) => {
      this.photoList = images;
      console.log(this.photoList);
    })
  }

}
