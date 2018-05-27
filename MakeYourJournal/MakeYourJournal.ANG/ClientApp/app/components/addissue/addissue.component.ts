import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

import Issue from "../../models/issue.model"

import { NavMenuComponent } from '../navmenu/navmenu.component';
import { IssueService } from "../../services/issue.service"



@Component({
    selector: 'add-issue',
    templateUrl: './addissue.component.html'
})
export class AddIssueComponent {
    public newIssue: Issue;

    constructor(
        private http: Http,
        private issueService: IssueService,
        private navMenu: NavMenuComponent
    ) { }

    ngOnInit() {
        this.newIssue = new Issue();
    }

    addIssue(issue: Issue) {
        this.issueService.addIssue(issue)
            .subscribe(data => {
                this.navMenu.ngOnInit();
            }, error => console.log('Could not add issue.'));
    }
}
