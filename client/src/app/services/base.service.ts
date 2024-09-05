import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export class BaseService<T> {
  constructor(
    protected http: HttpClient,
    private baseApiUrl: string
  ) {}

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseApiUrl}`);
  }

  getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.baseApiUrl}/${id}`);
  }

  create(item: T): Observable<any> {
    return this.http.post<any>(`${this.baseApiUrl}`, item);
  }

  update(item: T): Observable<any> {
    return this.http.put<any>(`${this.baseApiUrl}/${(item as any).id}`, item);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseApiUrl}/${id}`);
  }
}
