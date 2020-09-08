import { Component, OnInit } from '@angular/core';
import { Photo } from '../shared/models/photo.model';
import { ImageService } from '../shared/image/image.service'
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';
import { CategoriesConst } from '../shared/consts/categories.const'

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss']
})
export class PhotoComponent implements OnInit {

  categories = CategoriesConst.categories;
  photoList: Photo[];
  serverUrl: string = environment.serverUrl;

  constructor(private getterService: ImageService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      const category = params['category'];
      if(!this.categories.includes(category)) {
        this.getImages('Any');
      }
      else {
        this.getImages(category);
      }
    });
  }

  getImages(category: string): void {
    this.getterService.getImages(category).subscribe((images: Photo[]) => {
      this.photoList = images;
    });
  }
}
