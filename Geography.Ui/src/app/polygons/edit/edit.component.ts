import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() demoSnapshot:any;
  sputnikList:any;
  dateToday:Date = new Date();

  ngOnInit(): void {
    this.service.getSputnikList().subscribe(data=>{
      this.sputnikList=data;
    })    
  }
  
  updateClick(){
    if(this.demoSnapshot.cloudiness=='') this.demoSnapshot.cloudiness=null;
    this.service.updateDetail(this.demoSnapshot).subscribe(res=>{
    alert('Updated'); 
    });
  }

  addClick(){
    let coordinate: any[] = [,];
    this.demoSnapshot.geography.points.push(coordinate);
  }

  deleteClick(i:number){
    if(this.demoSnapshot.geography.points.length>5){
      if(confirm('Are you sure??')){
        this.demoSnapshot.geography.points.splice(i, 1);   
      }
    }
    else {
      alert("Count of points cannot be less than 5!")
    }    
  }

  parseDate(dateString: any): any {
    if (dateString.value) {
        return new Date(dateString.value);
    }
    alert('Incorrect date value');
  }
}
