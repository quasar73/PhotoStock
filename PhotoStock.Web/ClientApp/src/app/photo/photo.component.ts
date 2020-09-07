import { Component, OnInit } from '@angular/core';
import { Photo } from '../shared/models/photo.model';
import { ImageGetterService } from '../shared/image-getter/image-gettr.service'
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss'],
  providers: [ImageGetterService]
})
export class PhotoComponent implements OnInit {

  photoList: Photo[];
  serverUrl: string = environment.serverUrl;
  categories: string[] = [
    'Nature',
    'Animals',
    'Human',
    'Weapon',
    'Thing',
    'Other'
  ];

  constructor(private getterService: ImageGetterService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      let category = params['category'];
      console.log(category);
      if(!this.categories.includes(category)) {
        this.getAllImages();
      }
      else {
        this.getCategory(category);
      }
    });
  }

  getCategory(category: string): void{
    this.getterService.getImagesByCategory(category).subscribe((images: Photo[]) => {
      this.photoList = images;
    });
  }

  getAllImages(): void{
    this.getterService.getImages().subscribe((images: Photo[]) => {
      this.photoList = images;
    });
  }
}
