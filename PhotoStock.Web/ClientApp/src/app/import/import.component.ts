import { Component, OnInit } from '@angular/core';
import { ImageService } from '../shared/image/image.service';
import { CategoriesConst } from '../shared/consts/categories.const';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.scss']
})
export class ImportComponent implements OnInit {

  message: string;
  category: string;
  categories = CategoriesConst.categories;
  isFailed: boolean = false;
  inProgress: boolean = false;

  constructor(private imageService: ImageService) { }

  ngOnInit(): void {
  }

  import(imageInput: any): void {
    this.inProgress = true;
    const file: File = imageInput.files[0];
    this.imageService.uploadImage(file, this.category).subscribe(
      () => {
        this.isFailed = false;
        this.inProgress = false;
        this.message = "Image have uploaded successfully!";
      },
      (err) => {
        this.isFailed = true;
        this.inProgress = false;
      })
  }

}
