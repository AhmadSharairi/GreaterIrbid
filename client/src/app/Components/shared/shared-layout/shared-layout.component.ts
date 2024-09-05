import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-shared-layout',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shared-layout.component.html',
  styleUrl: './shared-layout.component.scss'
})
export class SharedLayoutComponent {
  @Input() title: string = '';
  @Input() backgroundImage: string | null = null;
  @Input() contentTemplate!: any;
  @Input() contentClass: string = '';



}
