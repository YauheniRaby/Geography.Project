import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl="https://localhost:5001/api/demoSnapshot/";

  constructor(private http:HttpClient) { }

  getAllList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'getAll'); 
  }

  getSputnikList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'getSputnikArr'); 
  }

  getFilterList(val:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'getByFilter?filter='+val); 
  }

  deleteDetail(val:any){
    return this.http.delete(this.APIUrl+val) 
  }

  updateDetail(val:any){
    return this.http.put(this.APIUrl,val) 
  }
}
