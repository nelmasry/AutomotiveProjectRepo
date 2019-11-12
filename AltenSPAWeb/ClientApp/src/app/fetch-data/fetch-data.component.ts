import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
    public forecasts: WeatherForecast[];
    public vehicles: Vehicle[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        console.log(baseUrl);
        //http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
        
        http.get<Vehicle[]>('api/vehicle/getonlinevehicles').subscribe(result => {
            this.vehicles = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Vehicle {
    Id: number,
    CustomerId: number,
    VehicleId: string,
    RegistrationNumber: number,
    LastPingDate: string
}
