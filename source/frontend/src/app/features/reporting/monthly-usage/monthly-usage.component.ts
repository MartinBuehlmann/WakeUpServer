// Documentation: Chart.js
// https://www.chartjs.org/docs/latest/charts/doughnut.html

import { Component, OnInit } from '@angular/core';
import { LegendLabelsContentArgs } from '@progress/kendo-angular-charts';
import { Observable, ReplaySubject, takeUntil } from 'rxjs';
import { MonthlyReport } from '../services/models/monthly-report.model';
import { ReportingService } from '../services/reporting-service';
import { ChartConfiguration, ChartData } from 'chart.js';

@Component({
  selector: 'app-reporting-monthly-usage',
  templateUrl: './monthly-usage.component.html',
  styleUrls: ['./monthly-usage.component.scss']
})
export class MonthlyUsageComponent implements OnInit {
  public reportingData: ChartData<'doughnut'> | undefined;
  public chartType: ChartConfiguration<'doughnut'>['type'] = 'doughnut';
  public chartOptions: ChartConfiguration<'doughnut'>['options'] = {
     responsive: true,
     plugins: {
      legend: {
        display: true,
        position: "bottom",
        align: "center",
      },
     },
     cutout: 60 
   };

  public totalWakeUpCount: number = 0;
  public selectedMonth: Date;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(0);

  constructor(private reportingService : ReportingService) {
    this.labelContent = this.labelContent.bind(this);
    this.selectedMonth = new Date();
  }

  ngOnInit(): void {
    this.updateChart();
  }

  public onSelectedMonthChange(value: Date): void {
    this.updateChart();
  }

  public labelContent(args: LegendLabelsContentArgs): string {
    return `${args.dataItem.wakeUpCount}`;
  }

  private updateChart(): void {
    let monthlyReport$: Observable<MonthlyReport> = this.reportingService.getMonthlyReport(this.selectedMonth.getFullYear(), this.selectedMonth.getMonth() + 1);
    monthlyReport$
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (res) => {
          let reportingItems = res.reportItems;
          this.totalWakeUpCount = reportingItems.map(item => item.wakeUpCount).reduce((sum, current) => sum + current)
          console.log(reportingItems);

          if (reportingItems.length > 0) {
            this.reportingData = {
              labels: reportingItems.map(x => x.macAddress),
              datasets: [
                { data: reportingItems.map(x => x.wakeUpCount) },
              ]
            }
          }
        }
      });
  }
}
