import { Component } from '@angular/core';
import { MonthlyUsageComponent } from "../monthly-usage/monthly-usage.component";

@Component({
  selector: 'app-home',
  imports: [MonthlyUsageComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
