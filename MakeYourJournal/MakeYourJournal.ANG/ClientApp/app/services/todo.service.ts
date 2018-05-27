import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Todo from '../models/todo.model';

@Injectable()
export class TodoService {

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    getAllTodo(articleId: number): Observable<Todo[]> {
        return this.http.get(this.baseUrl + 'api/Todo/GetByArticle/' + articleId)
            .map(response => response.json())
            .catch(this.handleError);
    }

    addTodo(newTodo: Todo) {
        this.http.post(this.baseUrl + 'api/Todo', JSON.stringify(newTodo));
    }

    deleteTodo(todoId: number) {
        this.http.delete(this.baseUrl + 'api/Todo/' + todoId);
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
