import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SatisfactionService {
  private apiUrl = 'http://localhost:5153/api/CitizenSatisfaction';

  constructor(private http: HttpClient) { }

  submitForm(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
