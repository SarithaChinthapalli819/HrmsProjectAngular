import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class AttendanceService{
    constructor(private http:HttpClient){

    }
    baseUrl = 'https://localhost:7037/';
    upsertAttendance(attendance){
       return this.http.post(`${this.baseUrl}api/attendance/UpsertAttendanceData`,attendance);
    }
    getAttendanceData(attendance){
        return this.http.post(`${this.baseUrl}api/attendance/GetAttendanceData`,attendance);
    }
}