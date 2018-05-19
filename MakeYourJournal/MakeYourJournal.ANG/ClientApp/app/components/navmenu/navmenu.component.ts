import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

import Issue from "../../models/issue.model"

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    public issues: Issue[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http.get(this.baseUrl + 'api/Issue').subscribe(result => {
            this.issues = result.json() as Issue[];
        }, error => console.error(error));
    }
}
