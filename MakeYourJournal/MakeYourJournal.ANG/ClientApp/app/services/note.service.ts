import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";

import 'rxjs/add/operator/map'

import Note from '../models/note.model';

@Injectable()
export class NoteService {

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    getAllNote(articleId: number): Observable<Note[]> {
        return this.http.get(this.baseUrl + 'api/Note/GetByArticle/' + articleId)
            .map(response => response.json())
            .catch(this.handleError);
    }

    addNote(newNote: Note) {
        this.http.post(this.baseUrl + 'api/Note', JSON.stringify(newNote));
    }

    deleteNote(noteId: number) {
        this.http.delete(this.baseUrl + 'api/Note/' + noteId);
    }

    private handleError(error: any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error);
    }
}
