// Documentation: Chart.js
// https://www.chartjs.org/docs/latest/charts/doughnut.html

import { Component, OnInit } from '@angular/core';
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

   public chartPlugins: any[] = [{
    afterDraw(chart: any) {
      const ctx = chart.ctx;

      let totalWakeUpCount = 0;
      if (chart.config.data.datasets.length > 0)
      {
        totalWakeUpCount = chart.config.data.datasets[0].data
          .reduce((sum: number, current: number) => sum + current, 0)
      }

      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      const centerX = ((chart.chartArea.left + chart.chartArea.right) / 2);
      const centerY = ((chart.chartArea.top + chart.chartArea.bottom) / 2);

      ctx.font = '24px Arial';
      ctx.fillStyle = 'white';

      // Draw text in center
      if (totalWakeUpCount > 0) {
        ctx.fillText(`${totalWakeUpCount}`, centerX, centerY);
      }
    }
  }];

  public selectedMonth: Date;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(0);

  constructor(private reportingService : ReportingService) {
    this.selectedMonth = new Date();
  }

  ngOnInit(): void {
    this.updateChart();
  }

  onSelectedMonthChange(value: Date): void {
    this.updateChart();
  }

  private updateChart(): void {
    let monthlyReport$: Observable<MonthlyReport> = this.reportingService.getMonthlyReport(this.selectedMonth.getFullYear(), this.selectedMonth.getMonth() + 1);
    monthlyReport$
      .pipe(takeUntil(this.destroyed$))
      .subscribe({
        next: (res: MonthlyReport) => {
          let reportingItems = res.reportItems;

          if (reportingItems.length > 0) {
            this.reportingData = {
              labels: reportingItems.map(x => x.macAddress),
              datasets: [
                { data: reportingItems.map(x => x.wakeUpCount) },
              ]
            }
          }
          else {
            this.reportingData = {
              labels: [],
              datasets: [],
            }
          }
        }
      });
  }
}
