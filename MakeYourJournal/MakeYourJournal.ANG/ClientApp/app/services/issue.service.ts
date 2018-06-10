import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Issue from '../models/issue.model';

@Injectable()
export class IssueService {
    private headers = new Headers();

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
        this.headers.append('Content-Type', 'application/json; charset=utf-8');
    }

    getIssues(): Observable<Issue[]> {
        return this.http.get(this.baseUrl + 'api/Issue')
            .map(response => response.json())
            .catch(this.handleError);
    }

    getIssue(issueId: number): Observable<Issue> {
        return this.http.get(this.baseUrl + 'api/Issue/' + issueId)
            .map(response => response.json())
            .catch(this.handleError);
    }

    addIssue(newIssue: Issue): Observable<Issue> {
        return this.http.post(this.baseUrl + 'api/Issue', JSON.stringify(newIssue), { headers: this.headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    deleteIssue(issueId: number) {
        this.http.delete(this.baseUrl + 'api/Issue/' + issueId)
            .subscribe(data => { }, error => console.error('Could not delete issue.'));
    }

    sendReminder(issueId: number) {
        this.http.get(this.baseUrl + 'api/Email/SendDeadlineReminder/' + issueId)
            .subscribe(data => { }, error => console.error('Could not send reminder.'));
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
