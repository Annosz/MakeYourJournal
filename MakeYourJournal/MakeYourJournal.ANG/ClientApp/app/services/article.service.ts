import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import Article from '../models/article.model';

@Injectable()
export class ArticleService {

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    getAllArticle(issueId: number): Promise<Article[]> {
        return this.http.get(this.baseUrl + 'api/Article/GetByIssue/' + issueId)
            .toPromise()
            .then(response => response.json().data as Article[])
            .catch(this.handleError);
    }


    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}
