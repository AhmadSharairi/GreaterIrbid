import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Pipe({
  name: 'safe',
  standalone: true
})
export class SafePipe implements PipeTransform {

  constructor(private sanitizer: DomSanitizer) { }

  transform(url: string): SafeResourceUrl {
    // or &rel=0 if it does.
    const modifiedUrl = url.includes('?') ? `${url}&rel=0` : `${url}?rel=0`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(modifiedUrl);
  }

}
