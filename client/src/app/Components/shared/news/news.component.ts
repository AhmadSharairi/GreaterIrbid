import { CommonModule, DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, LOCALE_ID, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NewsService } from '../../main-page/news-section/news.service';
import { NewsArticle } from '../../../Models/NewsArticle';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [CommonModule, HttpClientModule , RouterOutlet, RouterLink, RouterLinkActive],
  providers: [NewsService, { provide: LOCALE_ID, useValue: 'ar' } , DatePipe],
  templateUrl: './news.component.html',
  styleUrl: './news.component.scss'
})
export class NewsComponent implements OnInit {

  apiUrl = 'http://localhost:5153/';
  newsArticles: NewsArticle[] = [];

  constructor(private newsService: NewsService , private router: Router) {}

  ngOnInit() {
    this.loadNews();
  }


  loadNews(): void {
    this.newsService.getAll().subscribe(
      (data: NewsArticle[]) => {
        console.log('Received data:', data);
        if (Array.isArray(data)) {
          // Sort news articles by date in descending order
          this.newsArticles = data.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
        } else {
          console.error('API did not return an array:', data);
        }
      },
      (error) => {
        console.error('Error fetching news articles:', error);
      }
    );
  }

  goToNewsDetail(id: number): void {
    this.router.navigate(['/news', id]);
  }



  truncateDescription(description: string, wordLimit: number = 20): string {
    if (!description) return '';

    const words = description.split(' ');
    if (words.length <= wordLimit) {
      return description;
    }

    return words.slice(0, wordLimit).join(' ') + '...';
  }


}
