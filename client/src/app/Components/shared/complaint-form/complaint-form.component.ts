import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule, DatePipe } from '@angular/common';
import { Loader } from '@googlemaps/js-api-loader';  // Import Loader from @googlemaps/js-api-loader
import { HttpClient } from '@angular/common/http';
import { ComplaintService } from './complaint.service';
import { Category } from '../../../Models/Category';

@Component({
  selector: 'app-complaint-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    CommonModule
  ],
  providers: [DatePipe, ComplaintService],
  templateUrl: './complaint-form.component.html',
  styleUrls: ['./complaint-form.component.scss'],
})
export class ComplaintFormComponent implements OnInit {
  apiUrl = 'http://localhost:5153/';
  complaintForm: FormGroup;
  formattedDate: string | null = null;
  categories: Category[] = [];
  regions = [];
  cities = [
    { name: 'مركز إربد', value: 'Irbid_Center' },
    { name: 'بني عبيد', value: 'Al-Ramtha' },
    { name: 'الكورة', value: 'Koura' },
    { name: 'بني كنانة', value: 'Bani_Kinanah' },
    { name: 'بني إبراهيم', value: 'Bani_Ibrahim' },
    { name: 'المغير', value: 'Al-Mughayer' },
    { name: 'جامعة اليرموك', value: 'Al-Yarmouk_University' }
  ];

  @ViewChild('shareLocationInput') shareLocationInput!: ElementRef; // Add ViewChild

  constructor(
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private complaintService: ComplaintService
  ) {
    this.complaintForm = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.email]],
      nationality: ['', Validators.required],
      againstName: [''],
      idNumber: ['', Validators.required],
      region: ['', Validators.required],
      nearestComplaintLocation: ['', Validators.required],
      street: ['', Validators.required],
      buildingNumber: ['', Validators.required],
      date: ['', Validators.required],
      typePrivicy: ['', Validators.required],
      details: ['', Validators.required],
      attachment: [null],
      shareLocation: ['', Validators.required]
    });
  }

  ngOnInit() {
    const currentDate = new Date();
    this.formattedDate = this.datePipe.transform(currentDate, 'dd/MM/yyyy');
    this.loadGoogleMapsAutocomplete(); // Load Google Maps Autocomplete
  }

  loadGoogleMapsAutocomplete() {
    const loader = new Loader({
      apiKey: 'AIzaSyAx9_cPpOPDJQjUoXye5GIk_0B6-X9SDKk',
      libraries: ['places']
    });

    loader.load().then(() => {
      const autocomplete = new google.maps.places.Autocomplete(this.shareLocationInput.nativeElement, {
        types: ['address']
      });
      autocomplete.addListener('place_changed', () => {
        const place: google.maps.places.PlaceResult = autocomplete.getPlace();
        if (place.geometry === undefined || place.geometry === null) {
          return;
        }
        this.complaintForm.patchValue({ shareLocation: place.formatted_address });
      });
    });
  }

  onSubmit() {
    if (this.complaintForm.valid) {
      const formData = new FormData();
      Object.keys(this.complaintForm.controls).forEach(key => {
        const value = this.complaintForm.get(key)?.value;
        if (value !== null && value !== undefined) {
          formData.append(key, value);
        }
      });

      this.complaintService.submitComplaint(formData).subscribe(
        (response) => {
          console.log('Complaint submitted successfully', response);
          // Handle successful response
        },
        (error) => {
          console.error('Error submitting complaint', error);
          // Handle error response
        }
      );
    } else {
      console.warn('Form is not valid');
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.complaintForm.patchValue({ attachment: file });
    } else {
      console.error('No file selected');
    }
  }
}
