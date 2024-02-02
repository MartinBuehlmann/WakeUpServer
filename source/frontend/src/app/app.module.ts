import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './features/home/home.component';
import 'hammerjs';
import { FooterComponent } from './footer/footer.component';
import { ReportingModule } from './features/reporting/reporting.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReportingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
