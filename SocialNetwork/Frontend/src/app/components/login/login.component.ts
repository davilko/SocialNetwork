import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    
    public login: string = '';
    public password: string = '';
    
  constructor(private _httpService: HttpClient) { }

  
  ngOnInit() {
  }
  public send() {
      console.log(this.login, this.password);
      const headers = new HttpHeaders({ "Authorization": "Basic " + btoa(this.login + ":" + this.password)});
      this._httpService.get("api/auth/token", { headers: headers })
          .subscribe(
              response => console.log(response), 
              error1 => console.log(error1));
  }

}
