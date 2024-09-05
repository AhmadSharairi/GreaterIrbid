import { Component, OnInit } from '@angular/core';
import { VideoGalleryService, Channel } from './video-gallery.service';

@Component({
  selector: 'app-image-gallery',
  standalone: true,
  imports: [],
  providers:[VideoGalleryService],
  templateUrl: './video-gallery.component.html',
  styleUrl: './video-gallery.component.scss'
})
export class VideoGalleryComponent implements OnInit {
  channels: Channel[] = [];

  constructor(private videoGalleryService: VideoGalleryService) {}

  ngOnInit(): void {
    this.channels = this.videoGalleryService.getChannels();
  }
}
