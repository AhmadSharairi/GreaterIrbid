import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { SafePipe } from '../../../safe.pipe';


declare var YT: any; // Declare YT as any to avoid TypeScript errors

@Component({
  selector: 'app-carousel-videos',
  standalone: true,
  templateUrl: './carousel-videos.component.html',
  styleUrls: ['./carousel-videos.component.scss'],
  imports: [CommonModule, MatCardModule, MatButtonModule, SafePipe],
})
export class CarouselVideosComponent implements AfterViewInit, OnDestroy {
  players: any[] = []; // To hold YouTube player instances
  private intervalId: any; // To keep track of the interval for checking video time

  videos = [
    { url: 'https://www.youtube.com/embed/BPS_RPyE958?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/rpvw03t7Vi8?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/Eduj0-1D7wk?autoplay=1&mute=1' },
    { url: 'https://www.youtube.com/embed/3pK_bxa6kok?autoplay=1&mute=1' },
    // More videos
  ];

  ngAfterViewInit() {
    this.initializePlayers();
  }

  ngOnDestroy() {
    this.players.forEach((player: any) => player.destroy());
    if (this.intervalId) {
      clearInterval(this.intervalId); // Clear the interval if it exists
    }
  }

  initializePlayers() {
    this.videos.forEach((video, index) => {
      const player = new YT.Player(`player-${index}`, {
        height: '100%',
        width: '100%',
        videoId: this.extractVideoId(video.url),
        playerVars: {
          'autoplay': 1,
          'controls': 0,
          'loop': 1, // Do not loop at the player level
          'modestbranding': 1,
          'rel': 0,
          'iv_load_policy': 3 // Disables annotations
        },
        events: {
          'onReady': (event: any) => this.onPlayerReady(event.target),
        },
      });
      this.players[index] = player;
    });
  }

  extractVideoId(url: string): string {
    const regex = /\/embed\/([a-zA-Z0-9_-]+)/;
    const match = url.match(regex);
    return match ? match[1] : '';
  }

  onPlayerReady(player: any) {
    this.intervalId = setInterval(() => {
      this.players.forEach((player: any) => {
        if (player && player.getCurrentTime) {
          const currentTime = player.getCurrentTime();
          if (currentTime >= 5) {
            player.seekTo(0); // Seek back to the start
            player.playVideo(); // Play the video again
          }
        }
      });
    }, 1000); // Check every second
  }
}
