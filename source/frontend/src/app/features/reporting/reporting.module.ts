import { NgModule } from "@angular/core";
import { ReportingService } from "./services/reporting-service";
import { MonthlyUsageComponent } from './monthly-usage/monthly-usage.component';
import { ChartModule } from "@progress/kendo-angular-charts";
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";

@NgModule({
    providers: [
        ReportingService
    ],
    declarations: [
      MonthlyUsageComponent
    ],
    imports: [
      ChartModule,
      DateInputsModule
      ],
      exports: [
        MonthlyUsageComponent
      ]
  })
export class ReportingModule {}