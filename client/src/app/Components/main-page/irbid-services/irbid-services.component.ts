import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-irbid-services',
  standalone: true,
  imports: [],
  templateUrl: './irbid-services.component.html',
  styleUrl: './irbid-services.component.scss'
})
export class IrbidServicesComponent {
  IrbidServicesComponent(){}
  constructor(private router: Router) {}

  navigateToElectric(): void {
    this.router.navigate(['/electric']);
  }
  navigateToLibary(): void {
    this.router.navigate(['/libary']);
  }

  navigateToComplaint(){
    this.router.navigate(['/complaintForm']);
  }
}
