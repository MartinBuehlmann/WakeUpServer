import { Component, OnInit } from '@angular/core';
import { Observable, ReplaySubject, takeUntil } from 'rxjs';
import { MonthlyReport } from '../services/models/monthly-report.model';
import { ReportingService } from '../services/reporting-service';
import { ChartConfiguration, ChartData } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-monthly-usage',
  imports: [BaseChartDirective, FormsModule],
  providers: [ReportingService],
  templateUrl: './monthly-usage.component.html',
  styleUrl: './monthly-usage.component.scss'
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
        labels: {
          color: 'white',
        }
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

  // format: yyyy-MM (used with <input type="month">)
  public selectedMonth: string;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(0);

  constructor(private reportingService : ReportingService) {
    const now = new Date();
    const yyyy = now.getFullYear();
    const mm = String(now.getMonth() + 1).padStart(2, '0');
    this.selectedMonth = `${yyyy}-${mm}`;
  }

  ngOnInit(): void {
    this.updateChart();
  }

  onSelectedMonthChange(value: string): void {
    // value is yyyy-MM from the input
    this.selectedMonth = value;
    this.updateChart();
  }

  private updateChart(): void {
    // parse selectedMonth which is in the format yyyy-MM
    let year = new Date().getFullYear();
    let month = new Date().getMonth() + 1;
    if (this.selectedMonth) {
      const parts = this.selectedMonth.split('-');
      if (parts.length === 2) {
        year = Number(parts[0]);
        month = Number(parts[1]);
      }
    }

    let monthlyReport$: Observable<MonthlyReport> = this.reportingService.getMonthlyReport(year, month);
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
