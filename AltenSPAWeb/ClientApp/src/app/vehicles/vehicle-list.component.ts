import { Component, OnInit } from '@angular/core'
import { IVehicle, ICustomerModel } from './vehicle';
import { VehicleService } from './vehicle.service';
import {interval} from "rxjs/internal/observable/interval";
import {startWith, switchMap} from "rxjs/operators";
@Component({
    selector: 'pm-vehicles',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit{
    pageTitle: string = 'Vehicle List';
    _listFilter: string;
    errorMessage: string;
    get listFilter(): string{
        return this._listFilter;
    }
    set listFilter(value:string){
        this._listFilter = value;
        this.filteredVehicles = this.listFilter ? this.performFilter(this.listFilter) : this.vehicles;
    }
    filteredVehicles: ICustomerModel[];
    vehicles: ICustomerModel[] = [];

    constructor(private vehicleService: VehicleService) {
        
    }

    performFilter(filterBy: string): ICustomerModel[] {
        console.log('filter list by '+filterBy);
        filterBy = filterBy.toLocaleLowerCase();
        return this.vehicles.filter((vehicle: ICustomerModel) =>
            vehicle.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
    }

    ngOnInit(): void{
        console.log('In OnInit');

        interval(5000)
      .pipe(
        startWith(0),
        switchMap(() => this.vehicleService.getvehicles())
      )
      .subscribe(
        {
            next: vehicles => {
                this.vehicles = vehicles,
                    this.filteredVehicles = this.vehicles;
            },
            error: err => this.errorMessage = err
        }
      );

        // this.vehicleService.getvehicles()
        // .subscribe({
        //     next: vehicles => {
        //         this.vehicles = vehicles,
        //             this.filteredVehicles = this.vehicles;
        //     },
        //     error: err => this.errorMessage = err
        // });
    }
}
