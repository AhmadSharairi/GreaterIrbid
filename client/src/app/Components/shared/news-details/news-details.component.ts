import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NewsArticle } from '../../../Models/NewsArticle';
import { NewsService } from '../../main-page/news-section/news.service';
import { CommonModule, DatePipe } from '@angular/common';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'app-news-details',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.scss'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({ transform: 'translateY(-100%)' }),
        animate('300ms ease-in', style({ transform: 'translateY(0)' })),
      ]),
      transition(':leave', [
        animate('300ms ease-out', style({ transform: 'translateY(-100%)' })),
      ]),
    ]),
  ],
})
export class NewsDetailsComponent implements OnInit {
  newsArticle: NewsArticle | undefined;
  apiUrl = 'http://localhost:5153/';
  previousArticleId: number | undefined;
  nextArticleId: number | undefined;

  arabicMonths = [
    'يناير',
    'فبراير',
    'مارس',
    'أبريل',
    'مايو',
    'يونيو',
    'يوليو',
    'أغسطس',
    'سبتمبر',
    'أكتوبر',
    'نوفمبر',
    'ديسمبر',
  ];

  constructor(
    private newsService: NewsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      if (id) {
        this.loadNewsArticle(id);
      }
    });
  }

  loadNewsArticle(id: number): void {
    this.newsService.getById(id).subscribe(
      (data: NewsArticle) => {
        window.scrollTo(0, 0);
        this.newsArticle = data;
        if (!this.newsArticle.images) {
          this.newsArticle.images = [];
        }
        this.loadAdjacentArticles(id);
      },
      (error) => {
        console.error('Error fetching news article:', error);
      }
    );
  }

  loadAdjacentArticles(currentId: number): void {
    this.newsService.getAdjacentArticleIds(currentId).subscribe(
      (ids: { previousId?: number; nextId?: number }) => {
        this.previousArticleId = ids.previousId;
        this.nextArticleId = ids.nextId;
      },
      (error) => {
        console.error('Error fetching adjacent article IDs:', error);
      }
    );
  }

  goToPreviousArticle(): void {
    if (this.previousArticleId) {
      this.router.navigate(['/news', this.previousArticleId]).then(() => {});
    }
  }

  goToNextArticle(): void {
    if (this.nextArticleId) {
      this.router.navigate(['/news', this.nextArticleId]).then(() => {});
    }
  }

  getArabicMonth(date?: string | Date): string {
    if (!date) return '';
    const monthIndex = new Date(date).getMonth();
    return this.arabicMonths[monthIndex];
  }

  goToAllNews() {
    this.router.navigate(['/news']);
  }
}
