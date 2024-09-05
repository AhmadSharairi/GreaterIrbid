import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../../../services/base.service';
import { Complaint } from '../../../Models/Complaint';

@Injectable({
  providedIn: 'root'
})
export class ComplaintService extends BaseService<Complaint> {
private apiUrl = "http://localhost:5153/api/Complaints"
  constructor(http: HttpClient) {
    super(http, 'http://localhost:5153/api/Complaints');
  }

  submitComplaint(formData: FormData): Observable<Complaint> {
    return this.http.post<Complaint>(this.apiUrl, formData);
  }

}
