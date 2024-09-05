import { Component, LOCALE_ID, OnInit } from '@angular/core';
import { NewsService } from './news.service';

import { CommonModule, DatePipe, registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import localeAr from '@angular/common/locales/ar';

import { Route, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NewsArticle } from '../../../Models/NewsArticle';
import { NewsComponent } from '../../shared/news/news.component';
registerLocaleData(localeAr, 'ar');

@Component({
  selector: 'app-news-section',
  standalone: true,
  imports: [CommonModule, HttpClientModule , RouterOutlet, RouterLink, RouterLinkActive ],
  providers: [NewsService, { provide: LOCALE_ID, useValue: 'ar' } , DatePipe , NewsComponent],
  templateUrl: './news-section.component.html',
  styleUrl: './news-section.component.scss',
})
export class NewsSectionComponent implements OnInit {

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





  //for Admin
  createNews(newsArticle: NewsArticle): void {
    this.newsService.create(newsArticle).subscribe(
      (response) => {
        console.log('News created successfully:', response);
        this.loadNews(); // Refresh news list after creation
      },
      (error) => {
        console.error('Error creating news article:', error);
      }
    );
  }

  updateNews(newsArticle: NewsArticle): void {
    this.newsService.update(newsArticle).subscribe(
      (response) => {
        console.log('News updated successfully:', response);
        this.loadNews(); // Refresh news list after update
      },
      (error) => {
        console.error('Error updating news article:', error);
      }
    );
  }

  deleteNews(id: number): void {
    this.newsService.delete(id).subscribe(
      (response) => {
        console.log('News deleted successfully:', response);
        this.loadNews(); // Refresh news list after deletion
      },
      (error) => {
        console.error('Error deleting news article:', error);
      }
    );
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
