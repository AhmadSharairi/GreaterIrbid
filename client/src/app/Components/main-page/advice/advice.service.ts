import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdviceImage } from '../../../Models/AdviceImage';
import { BaseService } from '../../../services/base.service';


@Injectable({
  providedIn: 'root'
})
export class AdviceService extends BaseService<AdviceImage> {

  constructor(http: HttpClient)
   {
    super(http, 'http://localhost:5153/api/Advice');
   }

}
