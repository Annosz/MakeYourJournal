import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

import Todo from '../../models/todo.model';
import Article from '../../models/article.model';

@Component({
    selector: 'issue-page',
    templateUrl: './issuepage.component.html'
})
export class IssuePageComponent {
    public articles: Article[];

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string,
        private activatedroute: ActivatedRoute)
    {
    }

    ngOnInit() {
        this.activatedroute.params.subscribe(params => { this.getArticlesForIssue(params['id']); });
    }

    getArticlesForIssue(issueId: number) {
        this.http.get(this.baseUrl + 'api/Article/GetByIssue/' + issueId).subscribe(result => {
            this.articles = result.json() as Article[];
        }, error => console.error(error));
    }
}
