import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IUserCode, IAward } from "../winners-list/winners-list.model"; 

@Injectable({
    providedIn: 'root'
})

export class SubmitCodeService {
    lotteryUrl: string = "http://localhost:53327/api/lottery/"
    constructor(private http: HttpClient){

    }

    submitCode(userCode: IUserCode) : Observable<IAward> {
        return this.http.post<IAward>(this.lotteryUrl + 'submitCode', userCode);
    }
    
}