import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Http } from '@angular/http';

import Todo from '../models/todo.model';
import Article from '../models/article.model';

@Injectable()
export class ArticleService {
    constructor(private http: Http) { }



}
