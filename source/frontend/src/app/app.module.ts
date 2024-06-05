import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './features/home/home.component';
import { FooterComponent } from './footer/footer.component';
import { ReportingModule } from './features/reporting/reporting.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        HomeComponent,
        FooterComponent,
    ],
    bootstrap: [AppComponent],
    imports: [
        AppRoutingModule,
        BrowserModule,
        BrowserAnimationsModule,
        ReportingModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
