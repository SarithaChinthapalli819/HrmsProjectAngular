import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class TimesheetService {
    constructor(private http: HttpClient) {

    }
    baseUrl = "https://localhost:7037/"; 
    getTimesheetData(model) {
        return this.http.post(`${this.baseUrl}api/timesheet/GetTimesheetData`,model);
    }
    getTimesheetDetailsData(model){
        return this.http.post(`${this.baseUrl}api/timesheet/GetTimesheetDetailsData`,model);
    }
}