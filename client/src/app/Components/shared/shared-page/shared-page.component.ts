import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SharedLayoutComponent } from '../shared-layout/shared-layout.component';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-shared-page',
  standalone: true,
  imports: [SharedLayoutComponent],
  templateUrl: './shared-page.component.html',
  styleUrl: './shared-page.component.scss',
})
export class SharedPageComponent implements OnInit {
  pageContent!: TemplateRef<any>;
  title: string = '';
  backgroundImage: string = '';
  pageContentClass: string = '';
  satisfactionForm: FormGroup;

  @ViewChild('KnowledgeStation', { static: true })
  KnowledgeStation!: TemplateRef<any>;
  pdfUrl: string | undefined;
  @ViewChild('page2Content', { static: true }) page2Content!: TemplateRef<any>;
  @ViewChild('page3Content', { static: true }) page3Content!: TemplateRef<any>;
  @ViewChild('page4Content', { static: true }) page4Content!: TemplateRef<any>;
  @ViewChild('page5Content', { static: true }) page5Content!: TemplateRef<any>;
  @ViewChild('page6Content', { static: true }) page6Content!: TemplateRef<any>;
  @ViewChild('page7Content', { static: true }) page7Content!: TemplateRef<any>;
  @ViewChild('page8Content', { static: true }) page8Content!: TemplateRef<any>;

  constructor(private route: ActivatedRoute , private fb: FormBuilder) {
    this.satisfactionForm = this.fb.group({
      fullName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: [''],
      rating: [0, Validators.required],
      comments: ['']
    });
  }



  ngOnInit() {
    this.shardedFun();

  }


  shardedFun() {
    const pageId = this.route.snapshot.paramMap.get('id');
    // const pageName = this.route.snapshot.paramMap.get('pageName');
    switch (pageId) {
      case '1':
        this.title = 'محطــات المــعرفة الأردنيـــة ( إربد)';
        this.pageContent = this.KnowledgeStation;
        this.pageContentClass = 'page1-style';
        this.backgroundImage = "url('/assets/images/elctric.jpg')";

        break;
      case '2':
        this.title = 'محطــات المــعرفة الأردنيـــة (الحصن)';
        this.pageContent = this.KnowledgeStation;
        this.pageContentClass = 'page2-style';
        this.backgroundImage = "url('/assets/images/husun.jpg')";
        break;
      case '3':
        this.title = 'نبذة عن بلدية اربد';
        this.pageContent = this.page3Content;
        break;
      case '4':
        this.title = 'Page 4 Title';
        this.pageContent = this.page4Content;
        break;
      case '5':
        this.title = 'Page 5 Title';
        this.pageContent = this.page5Content;
        break;
      case '6':
        this.title = 'Page 6 Title';
        this.pageContent = this.page6Content;
        break;
      case '7':
        this.title = 'Page 7 Title';
        this.pageContent = this.page7Content;
        break;
      case '8':
        this.title = 'Page 8 Title';
        this.pageContent = this.page8Content;
        break;
      default:
        this.title = 'Default Title';
        this.pageContent = this.KnowledgeStation;
        break;
    }

  }


}
