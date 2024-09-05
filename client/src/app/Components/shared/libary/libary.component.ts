import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-libary',
  standalone: true,
  imports: [],
  templateUrl: './libary.component.html',
  styleUrl: './libary.component.scss'
})
export class LibaryComponent {
  constructor(private router: Router) {}



  navigateToVideoGallery(){
    this.router.navigate(['/videoGallery']);
  }
  navigateToImageGallery(){
    this.router.navigate(['/imagesGallery']);
  }
}
