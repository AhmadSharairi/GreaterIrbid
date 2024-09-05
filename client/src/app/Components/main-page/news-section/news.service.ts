import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '../../../services/base.service';
import { NewsArticle } from '../../../Models/NewsArticle';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NewsService extends BaseService<NewsArticle> {
  apiUrl = 'http://localhost:5153/api/News';

  constructor(http: HttpClient) {
    super(http, 'http://localhost:5153/api/News');
  }

  override getById(id: number): Observable<NewsArticle> {
    return this.http.get<NewsArticle>(`${this.apiUrl}/${id}`);
  }

  // Method to get adjacent article IDs (previous and next)
  getAdjacentArticleIds(currentId: number): Observable<{ previousId?: number, nextId?: number }> {
    return this.http.get<{ previousId?: number, nextId?: number }>(`${this.apiUrl}/${currentId}/adjacent`);
  }

 
}
