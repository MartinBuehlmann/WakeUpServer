export class ReportItem {
    macAddress: string;
    wakeUpCount: number;
    callerIpAddresses : string[];

    constructor(json: any) {
        this.macAddress = json.macAddress ?? '';
        this.wakeUpCount = json.wakeUpCount ?? 0;
        this.callerIpAddresses = json.callerIpAddresses ?? null;
    }
}