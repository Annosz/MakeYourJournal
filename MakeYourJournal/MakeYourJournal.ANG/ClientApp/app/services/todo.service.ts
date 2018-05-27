import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Todo from '../models/todo.model';

@Injectable()
export class TodoService {
    private headers = new Headers();

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
        this.headers.append('Content-Type', 'application/json; charset=utf-8');
    }

    getAllTodo(articleId: number): Observable<Todo[]> {
        return this.http.get(this.baseUrl + 'api/Todo/GetByArticle/' + articleId)
            .map(response => response.json())
            .catch(this.handleError);
    }

    addTodo(newTodo: Todo): Observable<Todo> {
        return this.http.post(this.baseUrl + 'api/Todo', JSON.stringify(newTodo), { headers: this.headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    updateTodo(todoId: number, todo: Todo): Observable<Todo> {
        return this.http.put(this.baseUrl + 'api/Todo/' + todoId, JSON.stringify(todo), { headers: this.headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    deleteTodo(todoId: number) {
        this.http.delete(this.baseUrl + 'api/Todo/' + todoId)
            .subscribe(data => { }, error => console.error('Could not delete todo.'));
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
