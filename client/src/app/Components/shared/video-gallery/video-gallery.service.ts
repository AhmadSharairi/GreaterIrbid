import { Injectable } from '@angular/core';

export interface Channel {
  name: string;
  url: string;
  imageUrl: string;
}

@Injectable({
  providedIn: 'root'
})
export class VideoGalleryService {
  private channels: Channel[] = [
    { name: 'Channel One', url: 'https://www.youtube.com/embed/BPS_RPyE958?autoplay=1&mute=1', imageUrl: 'path/to/image1.jpg' },
    { name: 'Channel Two', url: 'https://www.youtube.com/c/ChannelTwo', imageUrl: 'path/to/image2.jpg' },
    { name: 'Channel Three', url: 'https://www.youtube.com/c/ChannelThree', imageUrl: 'path/to/image3.jpg' }
  ];

  videos = [
    { url: 'https://www.youtube.com/embed/BPS_RPyE958?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/rpvw03t7Vi8?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/Eduj0-1D7wk?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/3pK_bxa6kok?autoplay=1&mute=1' },
    // More videos
  ];


  getChannels(): Channel[] {
    return this.channels;
  }
}
