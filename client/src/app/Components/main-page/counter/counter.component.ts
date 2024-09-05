import { AfterViewInit, Component } from '@angular/core';
import $ from 'jquery';

@Component({
  selector: 'app-counter',
  standalone: true,
  imports: [],
  templateUrl: './counter.component.html',
  styleUrl: './counter.component.scss'
})
export class CounterComponent  implements AfterViewInit {

  private observer: IntersectionObserver | undefined;

  ngAfterViewInit() {
    // Set up Intersection Observer
    this.observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          this.startCounterAnimation();
          this.observer?.unobserve(entry.target); // Stop observing after animation starts
        }
      });
    }, { threshold: 0.5 });

    // Observe the counter component
    const counterElement = document.querySelector('#counter');
    if (counterElement) {
      this.observer?.observe(counterElement); // Use optional chaining to safely call observe
    }
  }

  startCounterAnimation() {
    $('.count .number').each(function (this: HTMLElement) {
      const $this = $(this);
      $this.prop('Counter', 0).animate({
        Counter: parseInt($this.text(), 10)
      }, {
        duration: 1500, // Adjust duration as needed
        easing: 'swing',
        step: function (now: number) {
          $this.text(Math.ceil(now).toString());
        }
      });
    });
  }
}
