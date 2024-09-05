import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

import { EnvironmentService } from './environment.service';
import { SafePipe } from '../../../safe.pipe';
import { EnvironmentImage } from '../../../Models/EnvironmentImage';

@Component({
  selector: 'app-protect-environment',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, SafePipe],
  templateUrl: './protect-environment.component.html',
  styleUrls: ['./protect-environment.component.scss'],
})
export class ProtectEnvironmentComponent implements OnInit {
  apiUrl = 'http://localhost:5153/';
  environmentItems: EnvironmentImage[] = [];

  constructor(private enviService: EnvironmentService) {}

  ngOnInit() {
    this.loadImageEnvironment();
  }

  loadImageEnvironment(): void {
    this.enviService.getAll().subscribe(
      (data: EnvironmentImage[]) => {
        console.log('Received data:', data);
        if (Array.isArray(data)) {
          this.environmentItems = data;
        } else {
          console.error('API did not return an array:', data);
        }
      },
      (error) => {
        console.error('Error fetching environment images:', error);
      }
    );
  }

  scrollRight(carouselId: string) {
    const carousel = document.getElementById(carouselId);
    if (carousel) {
      const firstItem = carousel.querySelector('.carousel-item');
      if (firstItem) {
        const itemWidth = firstItem.clientWidth;
        const marginRight = parseInt(window.getComputedStyle(firstItem).marginRight, 10) || 0;
        const scrollAmount = itemWidth + marginRight;

        // Scroll by the width of one item plus the margin
        carousel.scrollBy({ left: scrollAmount, behavior: 'smooth' });
      } else {
        console.error('No carousel items found');
      }
    } else {
      console.error(`Carousel with ID ${carouselId} not found`);
    }
  }

  scrollLeft(carouselId: string) {
    const carousel = document.getElementById(carouselId);
    if (carousel) {
      const firstItem = carousel.querySelector('.carousel-item');
      if (firstItem) {
        const itemWidth = firstItem.clientWidth;
        const marginRight = parseInt(window.getComputedStyle(firstItem).marginRight, 10) || 0;
        const scrollAmount = itemWidth + marginRight;

        // Scroll by the width of one item plus the margin
        carousel.scrollBy({ left: -scrollAmount, behavior: 'smooth' });
      } else {
        console.error('No carousel items found');
      }
    } else {
      console.error(`Carousel with ID ${carouselId} not found`);
    }
  }


}
