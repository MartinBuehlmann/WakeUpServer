import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReportingService } from "./services/reporting-service";
import { MonthlyUsageComponent } from './monthly-usage/monthly-usage.component';
import { NgChartsModule } from 'ng2-charts';
import { CalendarModule } from 'primeng/calendar';

@NgModule({
    providers: [
        ReportingService
    ],
    declarations: [
      MonthlyUsageComponent
    ],
    imports: [
      FormsModule,
      NgChartsModule,
      CalendarModule,
    ],
      exports: [
        MonthlyUsageComponent
      ]
  })
export class ReportingModule {}