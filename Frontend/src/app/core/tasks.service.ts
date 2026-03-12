import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class TasksService{
    constructor(private http:HttpClient){

    }
    baseUrl = "https://localhost:7037/";

    upsertTask(project){
       return this.http.post(`${this.baseUrl}api/project/UpsertTask`,project);
    }

    getTask(model){
       return this.http.post(`${this.baseUrl}api/project/GetTask`,model);
    }

    upsertSpenTime(model){
        return this.http.post(`${this.baseUrl}api/project/UpsertSpentTime`,model);
    }

    getSpenTime(model){
        return this.http.post(`${this.baseUrl}api/project/GetSpentTime`,model);
    }
}