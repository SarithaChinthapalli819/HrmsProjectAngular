import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core"; 

@Injectable({
    providedIn:'root'
})

export class AuthService{
    basePathUrl = "https://localhost:7037";
    constructor(private http :HttpClient){

    }
    register(model){
       return this.http.post(`${this.basePathUrl}/api/user/register`,model);
    }
    login(model){
       return this.http.post(`${this.basePathUrl}/api/user/login`,model);
    }
    getDepartments(){
        return this.http.get(`${this.basePathUrl}/api/user/departments`);
    }
    getDesignations(){
        return this.http.get(`${this.basePathUrl}/api/user/designations`);
    }
    getRoles(){
        return this.http.get(`${this.basePathUrl}/api/user/roles`)
    }
    getUserDetails(){
        return this.http.get(`${this.basePathUrl}/api/users/userdetails`);
    }
}