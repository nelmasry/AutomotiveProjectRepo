import { Injectable } from '@angular/core';
import { IVehicle, ICustomerModel } from './vehicle';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, interval, timer } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
// import { stat } from 'fs';

@Injectable({
    providedIn: 'root'
})
export class VehicleService {

    private vehicleUrl = ''
    constructor(private http: HttpClient) {

    }

    getvehicles(): Observable<ICustomerModel[]> {
        var baseUrl: string = 'http://localhost:9090/';
        this.vehicleUrl = baseUrl + 'api/getvehicleswithdetails';
        var result = this.http.get<ICustomerModel[]>(this.vehicleUrl)
            .pipe(
                tap(data => console.log('All: ' + JSON.stringify(data))),
                catchError(this.handleError)
            );

        return result;
    }

    getonlinevehicles(status: number): Observable<ICustomerModel[]> {
        var baseUrl: string = 'http://localhost:9090/';
        this.vehicleUrl = baseUrl + 'api/getvehiclesbystatuswithdetails/' + status;

        var result = this.http.get<ICustomerModel[]>(this.vehicleUrl)
            .pipe(
                tap(data => console.log('All: ' + JSON.stringify(data))),
                catchError(this.handleError)
            );

        return result;
    }
    private handleError(err: HttpErrorResponse) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            errorMessage = `An error occurred: ${err.error.message}`;
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }
}
