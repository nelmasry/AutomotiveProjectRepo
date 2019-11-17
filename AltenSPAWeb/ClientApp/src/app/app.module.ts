import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'
import { RouterModule } from '@angular/router'

import { AppComponent } from './app.component';
import { convertToSpaces } from './shared/convert-to-spaces.pipe';
import { StarComponent } from './shared/star.component';
import { WelcomeComponent } from './home/welcome.component';
import { VehicleListComponent } from './vehicles/vehicle-list.component';

@NgModule({
    declarations: [
        AppComponent,
        convertToSpaces,
        StarComponent,
        WelcomeComponent,
        VehicleListComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'welcome', component: WelcomeComponent },
            { path: '', redirectTo: 'welcome', pathMatch: 'full' },
            { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
        ])
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
