import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IUserCodeAward } from "./winners-list.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class WinnersListService {
    winnersUrl: string = "http://localhost:53327/api/lottery/"
    constructor(private http: HttpClient){

    }
    getAllwinners(): Observable<Array<IUserCodeAward>> {
        return this.http.get<Array<IUserCodeAward>>(this.winnersUrl + "GetAllWinners");
    }
}