import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Components/main-page/header/header.component';
import { MainContentComponent } from './Components/main-page/main-content/main-content.component';
import { NewsSectionComponent } from './Components/main-page/news-section/news-section.component';
import { CarouselImageComponent } from './Components/main-page/carousel-image/carousel-image.component';
import { FooterComponent } from './Components/main-page/footer/footer.component';
import { IrbidServicesComponent } from './Components/main-page/irbid-services/irbid-services.component';
import { ProtectEnvironmentComponent } from './Components/main-page/protect-environment/protect-environment.component';
import { AdviceComponent } from './Components/main-page/advice/advice.component';
import { CarouselVideosComponent } from './Components/main-page/carousel-videos/carousel-videos.component';
import { NewsComponent } from './Components/shared/news/news.component';
import { HomeComponent } from './Components/main-page/home/home.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NewsDetailsComponent } from './Components/shared/news-details/news-details.component';
import { ComplaintFormComponent } from './Components/shared/complaint-form/complaint-form.component';
import { ImageGalleryComponent } from './Components/shared/image-gallery/image-gallery.component';
import { VideoGalleryComponent } from './Components/shared/video-gallery/video-gallery.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    CommonModule,
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    NgxSpinnerModule,
  ],
})
export class AppComponent implements OnInit {
  title = 'GreaterIrbid';
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        setTimeout(() => {
          window.scrollTo(0, 0);
        }, 0); // Delay can be adjusted if needed
      }
    });
  }
}
