import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class ProjectService{
    constructor(private http:HttpClient){

    }
    baseUrl = "https://localhost:7037/";

    upsertProject(project){
       return this.http.post(`${this.baseUrl}api/project/UpsertProject`,project);
    }
    getProject(){
       return this.http.get(`${this.baseUrl}api/project/GetProject`);
    }
}