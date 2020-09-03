import { Component, OnInit } from '@angular/core';
import { ImageImportService } from '../shared/image-import/image-import.service';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.scss'],
  providers: [ImageImportService]
})
export class ImportComponent implements OnInit {

  message: string;
  category: string;
  categories: string[] = [
    'Human',
    'Nature',
    'Anumals',
    'Weapon',
    'Things',
    'Other'
  ];
  isFailed: boolean = false;
  inProgress: boolean = false;

  constructor(private imageService: ImageImportService) { }

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
