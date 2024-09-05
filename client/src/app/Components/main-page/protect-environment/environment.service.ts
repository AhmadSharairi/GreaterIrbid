import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '../../../services/base.service';
import { EnvironmentImage } from '../../../Models/EnvironmentImage';



@Injectable({
  providedIn: 'root'
})
export class EnvironmentService extends BaseService<EnvironmentImage> {

  constructor(http: HttpClient) {
    super(http, 'http://localhost:5153/api/environment');
  }

}


