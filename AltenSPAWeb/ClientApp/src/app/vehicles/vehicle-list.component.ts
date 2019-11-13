import { Component, OnInit } from '@angular/core'
import { IVehicle, ICustomerModel } from './vehicle';
import { VehicleService } from './vehicle.service';
import { interval } from "rxjs/internal/observable/interval";
import { startWith, switchMap } from "rxjs/operators";
import { Observable } from 'rxjs';
@Component({
    selector: 'pm-vehicles',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {
    pageTitle: string = 'Vehicle List';
    _listFilter: string;
    errorMessage: string;
    get listFilter(): string {
        return this._listFilter;
    }
    set listFilter(value: string) {
        this._listFilter = value;
        this.filteredVehicles = this.listFilter ? this.performFilter(this.listFilter) : this.customers;
    }

    _statusFilter: string;
    get statusFilter(): string {
        return this._statusFilter;
    }
    set statusFilter(value: string) {
        this._statusFilter = value;
        var filter: number = value.toLocaleLowerCase() == 'all' ? -1 :
            (value.toLocaleLowerCase() == 'online' ? 1 : 0);
        this.performStatusFilter(filter);
        // this.filteredVehicles = this.statusFilter ? this.performStatusFilter(this.statusFilter) : this.customers;
    }
    filteredVehicles: ICustomerModel[];
    customers: ICustomerModel[] = [];

    constructor(private vehicleService: VehicleService) {

    }

    performFilter(filterBy: string): ICustomerModel[] {
        console.log('filter list by ' + filterBy);
        filterBy = filterBy.toLocaleLowerCase();
        return this.customers.filter((cust: ICustomerModel) =>
            cust.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
    }

    performStatusFilter(filterBy: number) {
        console.log('filter list by ' + filterBy);
        // filterBy = filterBy.toLocaleLowerCase();
        if (filterBy == -1) {
            this.vehicleService.getvehicles()
                .subscribe({
                    next: vehicles => {
                        this.customers = vehicles,
                            this.filteredVehicles = this.customers;
                    },
                    error: err => this.errorMessage = err
                });
        }
        else {
            this.vehicleService.getonlinevehicles(filterBy)
                .subscribe({
                    next: vehicles => {
                        this.customers = vehicles,
                            this.filteredVehicles = this.customers;
                    },
                    error: err => this.errorMessage = err
                });
        }
    }

    ngOnInit(): void {
        console.log('In OnInit');


        interval(5000)
            .pipe(
                startWith(0),
                switchMap(() => this.getVehicles())
            )
            .subscribe(
                {
                    next: cust => {
                        this.customers = cust,
                            this.filteredVehicles = this.customers;
                    },
                    error: err => this.errorMessage = err
                }
            );

        // this.vehicleService.getvehicles()
        // .subscribe({
        //     next: vehicles => {
        //         this.customers = vehicles,
        //             this.filteredVehicles = this.customers;
        //     },
        //     error: err => this.errorMessage = err
        // });
    }

    getVehicles(): Observable<ICustomerModel[]> {
        if (!this.statusFilter || this.statusFilter == 'all') {
            return this.vehicleService.getvehicles()
        }
        else {
            var filter: number = this.statusFilter == 'all' ? -1 :
                (this.statusFilter == 'online' ? 1 : 0);
            return this.vehicleService.getonlinevehicles(filter);
        }
    }
}
