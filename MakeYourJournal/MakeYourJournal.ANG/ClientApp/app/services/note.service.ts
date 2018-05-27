import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Note from '../models/note.model';

@Injectable()
export class NoteService {
    private headers = new Headers();

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
        this.headers.append('Content-Type', 'application/json; charset=utf-8');
    }

    getAllNote(articleId: number): Observable<Note[]> {
        return this.http.get(this.baseUrl + 'api/Note/GetByArticle/' + articleId)
            .map(response => response.json())
            .catch(this.handleError);
    }

    addNote(newNote: Note): Observable<Note> {
        return this.http.post(this.baseUrl + 'api/Note', JSON.stringify(newNote), { headers: this.headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    deleteNote(noteId: number) {
        this.http.delete(this.baseUrl + 'api/Note/' + noteId)
            .subscribe(data => { }, error => console.error('Could not delete note.'));
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
