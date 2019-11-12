import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'
import { RouterModule } from '@angular/router'

import { AppComponent } from './app.component';
import { ProductListComponent } from './products/product-list.component';
import { convertToSpaces } from './shared/convert-to-spaces.pipe';
import { StarComponent } from './shared/star.component';
import { ProductDetailComponent } from './products/product-detail.component';
import { WelcomeComponent } from './home/welcome.component';
import { VehicleListComponent } from './vehicles/vehicle-list.component';

@NgModule({
    declarations: [
        AppComponent,
        ProductListComponent,
        convertToSpaces,
        StarComponent,
        ProductDetailComponent,
        WelcomeComponent,
        VehicleListComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: 'products', component: ProductListComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'products/:id', component: ProductDetailComponent },
            { path: 'welcome', component: WelcomeComponent },
            { path: '', redirectTo: 'welcome', pathMatch: 'full' },
            { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
        ])
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
