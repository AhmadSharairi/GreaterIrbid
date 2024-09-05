import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SatisfactionService } from './satisfaction.service';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedLayoutComponent } from '../shared-layout/shared-layout.component';

@Component({
  selector: 'app-citizen-satisfaction-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    CommonModule,
    MatSnackBarModule,
    SharedLayoutComponent,
  ],
  templateUrl: './citizen-satisfaction-form.component.html',
  styleUrls: ['./citizen-satisfaction-form.component.scss'],
})
export class CitizenSatisfactionFormComponent {
  satisfactionForm: FormGroup;
  rating: number = 0;

  title = 'رضا المواطنين';
  backgroundImage = "url('/assets/images/elctric.jpg')";

  constructor(
    private fb: FormBuilder,
    private satisfactionService: SatisfactionService,
    private snackBar: MatSnackBar
  ) {
    this.satisfactionForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.maxLength(100)]],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)],
      ],
      email: ['', [Validators.email]],
      rating: [0, [Validators.required, Validators.min(0), Validators.max(5)]],
      comments: ['', [Validators.maxLength(1000)]], // Updated max length
    });
  }

  rate(value: number) {
    this.satisfactionForm.get('rating')?.setValue(value);
  }

  onSubmit() {
    if (this.satisfactionForm.valid) {
      this.satisfactionService
        .submitForm(this.satisfactionForm.value)
        .subscribe((response) => {
          console.log(response);
        });

      this.snackBar.open(' شكراً لك تم إرسال المعلومات بنجاح!', 'إغلاق', {
        duration: 3000,
        verticalPosition: 'top',
        horizontalPosition: 'center',
      });

      this.satisfactionForm.reset();
    }
  }
}
