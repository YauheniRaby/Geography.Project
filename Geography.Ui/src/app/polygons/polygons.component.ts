import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-polygons',
  templateUrl: './polygons.component.html',
  styleUrls: ['./polygons.component.css']
})
export class PolygonsComponent implements OnInit {

  constructor(private service:SharedService) { }

  demoSnapshotList:any=[];
  demoSnapshot:any;
  filter:string='';
  activateAddComp:boolean=false;

  ngOnInit(): void {
    this.filter='';
    this.refreshDemoSnapshotList();
  }

  refreshDemoSnapshotList(){
    if(Boolean(this.filter)){
      this.service.getFilterList(this.filter).subscribe(data=>{
        this.demoSnapshotList=data;
      })
    }
    else{
      this.service.getAllList().subscribe(data=>{
        this.demoSnapshotList=data;
      })
    }
  }

  removeFilter(): void{
    this.filter='';
    this.refreshDemoSnapshotList();
  }

  deleteClick(id:any){
    if(confirm('Are you sure??')){
      this.service.deleteDetail(id);
      this.filter='';
      this.refreshDemoSnapshotList();  
    }
  }

  editClick(dataItem:any){
    this.demoSnapshot=dataItem;
    this.activateAddComp = true;
  }

  closeClick(){
    this.activateAddComp=false;
    this.refreshDemoSnapshotList();
  }
}
