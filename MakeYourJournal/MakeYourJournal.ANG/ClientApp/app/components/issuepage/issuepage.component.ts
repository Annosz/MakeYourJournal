import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

import Todo from '../../models/todo.model';
import Article from '../../models/article.model';

@Component({
    selector: 'issuepage',
    templateUrl: './issuepage.component.html'
})
export class IssuePageComponent {
    public issueId: number;
    public todos: Todo[];
    public articles: Article[];

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string,
        private activatedroute: ActivatedRoute)
    {
        activatedroute.params.subscribe(params => { this.issueId = params['id']; });

        this.http.get(this.baseUrl + 'api/Article/GetByIssue/' + this.issueId).subscribe(result => {
            this.articles = result.json() as Article[];
        }, error => console.error(error));
    }

    getArticlesForIssue() {
    }
}
