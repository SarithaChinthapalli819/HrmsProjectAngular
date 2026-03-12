import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class TeamsService{
    constructor(private http:HttpClient){

    }
    baseUrl = 'https://localhost:7037/'
    upsertTeams(teams){
        return this.http.post(`${this.baseUrl}api/teams/UpsertTeams`,teams);
    }
    getTeams(){
        return this.http.get(`${this.baseUrl}api/teams/GetTeams`);
    }
    upsertTeammebers(team){
        return this.http.post(`${this.baseUrl}api/teams/UpsertTeamMembers`,team);
    }
    getTeammebers(team){
        return this.http.post(`${this.baseUrl}api/teams/GetTeamMembers`,team);
    }
}