import { ReportItem } from "./report-item.model";

export class MonthlyReport {
    year: number;
    month: number;
    reportItems : ReportItem[];

    constructor(json: any) {
        this.year = json.year ?? 0;
        this.month = json.month ?? 0;
        this.reportItems = json.reportItems ?? null;
    }
}