import { Component, OnInit } from '@angular/core';
import { LegendLabelsContentArgs } from '@progress/kendo-angular-charts';
import { Observable, ReplaySubject, takeUntil } from 'rxjs';
import { MonthlyReport } from '../reporting/services/models/monthly-report.model';
import { ReportItem } from '../reporting/services/models/report-item.model';
import { ReportingService } from '../reporting/services/reporting-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public reportingItems: ReportItem[] = [];
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
          this.reportingItems = res.reportItems;
          this.totalWakeUpCount = this.reportingItems.map(item => item.wakeUpCount).reduce((sum, current) => sum + current)
          console.log(this.reportingItems);
        }
      });
  }
}
