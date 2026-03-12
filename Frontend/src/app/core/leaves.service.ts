import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class LeaveService{
    constructor(private http:HttpClient){

    }
    baseUrl = 'https://localhost:7037/';
    getLeaveTypes(){
        return this.http.get(`${this.baseUrl}api/leaves/GetLeaveTypes`);
    }
    upsertLeaves(leaves){
        return this.http.post(`${this.baseUrl}api/leaves/UpsertLeave`,leaves);
    }
    getLeaves(){
        return this.http.get(`${this.baseUrl}api/leaves/GetLeave`);
    }
}