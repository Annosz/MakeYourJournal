import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Issue from '../models/issue.model';

@Injectable()
export class IssueService {

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    getIssues(): Observable<Issue[]> {
        return this.http.get(this.baseUrl + 'api/Issue')
            .map(response => response.json())
            .catch(this.handleError);
    }

    addIssue(newIssue: Issue) {
        this.http.post(this.baseUrl + 'api/Issue', JSON.stringify(newIssue));
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
