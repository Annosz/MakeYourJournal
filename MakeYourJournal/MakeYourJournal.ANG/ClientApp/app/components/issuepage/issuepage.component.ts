import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

import Todo from '../../models/todo.model';
import Article from '../../models/article.model';

import { ArticleService } from '../../services/article.service';

@Component({
    selector: 'issue-page',
    templateUrl: './issuepage.component.html'
})
export class IssuePageComponent {
    public articles: Article[];

    constructor(
        private http: Http,
        private articleService: ArticleService,
        private activatedroute: ActivatedRoute)
    {
    }

    ngOnInit() {
        this.activatedroute.params.subscribe(params => { this.getArticlesForIssue(params['id']); });
    }

    getArticlesForIssue(issueId: number) {
        this.articleService.getAllArticle(issueId).then(articles => this.articles = articles);
    }
}
