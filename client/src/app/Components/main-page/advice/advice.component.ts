import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AdviceService } from './advice.service';
import { SafePipe } from '../../../safe.pipe';
import { AdviceImage } from '../../../Models/AdviceImage';

@Component({
  selector: 'app-advive',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    SafePipe
  ],
  templateUrl: './advice.component.html',
  styleUrls: ['./advice.component.scss'],
})
export class AdviceComponent implements OnInit {
  apiUrl = 'http://localhost:5153/';
  adviceItems: AdviceImage[] = [];

  constructor(private adviceService: AdviceService) {}

  ngOnInit() {
    this.loadImageAdvice();
  }

  loadImageAdvice(): void {
    this.adviceService.getAll().subscribe(
      (data: AdviceImage[]) => {
        console.log('Received data:', data);
        if (Array.isArray(data)) {
          this.adviceItems = data;
        } else {
          console.error('API did not return an array:', data);
        }
      },
      (error) => {
        console.error('Error fetching advice images:', error);
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
