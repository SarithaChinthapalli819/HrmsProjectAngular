import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class PayrollService{
    constructor(private http:HttpClient){
    }
    baseUrl = "https://localhost:7037/";
    upsertPayroll(model){
        return this.http.post(`${this.baseUrl}api/payroll/UpsertPayroll`,model);
    }
    getPayroll(){
        return this.http.get(`${this.baseUrl}api/payroll/GetPayroll`);
    }
}