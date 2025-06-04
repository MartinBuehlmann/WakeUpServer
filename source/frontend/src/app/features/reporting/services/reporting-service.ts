import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable, of } from "rxjs";
import { MonthlyReport } from "./models/monthly-report.model";

@Injectable()
export class ReportingService {
    constructor(private http: HttpClient) {
    }

    getMonthlyReport(year: number, month: number) : Observable<MonthlyReport> {
        return this.http
            .get<MonthlyReport>(`/web/Reporting/${year}/${month}`)
            .pipe(map((content) => new MonthlyReport(content)));
    }
}