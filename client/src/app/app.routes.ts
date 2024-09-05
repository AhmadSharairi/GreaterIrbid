import { Routes } from '@angular/router';
import { HomeComponent } from './Components/main-page/home/home.component';
import { NewsComponent } from './Components/shared/news/news.component';
import { ElectricServicesComponent } from './Components/shared/electric-services/electric-services.component';
import { LibaryComponent } from './Components/shared/libary/libary.component';
import { NewsDetailsComponent } from './Components/shared/news-details/news-details.component';
import { ComplaintFormComponent } from './Components/shared/complaint-form/complaint-form.component';
import { VideoGalleryComponent } from './Components/shared/video-gallery/video-gallery.component';
import { ImageGalleryComponent } from './Components/shared/image-gallery/image-gallery.component';
import { CounterComponent } from './Components/main-page/counter/counter.component';
import { SharedPageComponent } from './Components/shared/shared-page/shared-page.component';
import { CitizenSatisfactionFormComponent } from './Components/shared/citizen-satisfaction-form/citizen-satisfaction-form.component';


export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'news', component: NewsComponent },
  { path: 'electric', component: ElectricServicesComponent },
  { path: 'libary', component: LibaryComponent },
  { path: 'news/:id', component: NewsDetailsComponent } ,
  { path: 'complaintForm', component: ComplaintFormComponent } ,
  { path: 'videoGallery', component: VideoGalleryComponent } ,
  { path: 'imagesGallery', component: ImageGalleryComponent } ,
  { path: 'counter', component: CounterComponent } ,
  { path: 'form', component: CitizenSatisfactionFormComponent } ,
  { path: 'page/:id', component: SharedPageComponent },
  { path: '**', redirectTo: '' }, // Wildcard route for a 404 page
];
