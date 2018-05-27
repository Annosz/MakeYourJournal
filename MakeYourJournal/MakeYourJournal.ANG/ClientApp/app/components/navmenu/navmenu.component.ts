import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

import Issue from "../../models/issue.model"

import { IssueService } from "../../services/issue.service"

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    public issues: Issue[];

    constructor(
        private http: Http,
        private issueService: IssueService
    ) {
        this.issueService.getIssues()
            .subscribe(data => {
                this.issues = data;
            }, error => console.log('Could not load issues.'));
    }


}
