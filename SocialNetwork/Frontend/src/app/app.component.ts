import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'My Social Network';
  apiValues: string[] = [];

  constructor(private _httpService: HttpClient) {}

  ngOnInit(): void {
    this._httpService.get('/api/values')
      .subscribe(values => {
         this.apiValues = values as string[];
      });
  }
}
