import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { IrbidServicesComponent } from '../irbid-services/irbid-services.component';
import { CarouselVideosComponent } from '../carousel-videos/carousel-videos.component';
import { CarouselImageComponent } from '../carousel-image/carousel-image.component';
import { NewsSectionComponent } from '../news-section/news-section.component';
import { ProtectEnvironmentComponent } from '../protect-environment/protect-environment.component';
import { AdviceComponent } from '../advice/advice.component';
import { HeaderComponent } from "../header/header.component";
import { MainContentComponent } from "../main-content/main-content.component";
import { CounterComponent } from "../counter/counter.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    IrbidServicesComponent,
    CarouselVideosComponent,
    CarouselImageComponent,
    NewsSectionComponent,
    AdviceComponent,
    ProtectEnvironmentComponent,
    HeaderComponent,
    MainContentComponent,
    CounterComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  ngOnInit() {
    window.scrollTo(0, 0);

  }
}
