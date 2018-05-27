import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

import Todo from '../../models/todo.model';
import Note from '../../models/note.model';
import Article from '../../models/article.model';

import { ArticleService } from '../../services/article.service';
import { TodoService } from '../../services/todo.service';
import { NoteService } from '../../services/note.service';

@Component({
    selector: 'issue-page',
    templateUrl: './issuepage.component.html'
})
export class IssuePageComponent {
    public issueId: number;
    public articles: Article[];
    public newArticle: Article;
    public newTodo: Todo;
    public newNote: Note;

    constructor(
        private http: Http,
        private articleService: ArticleService,
        private todoService: TodoService,
        private noteService: NoteService,
        private activatedroute: ActivatedRoute)
    {
        this.newArticle = new Article();
        this.newTodo = new Todo();
        this.newNote = new Note();
    }

    ngOnInit() {
        this.activatedroute.params.subscribe(params => {
            this.issueId = params['id'];
            this.getArticlesForIssue(params['id']);
        });
    }

    getArticlesForIssue(issueId: number) {
        this.articleService.getAllArticle(issueId)
            .subscribe(data => {
                this.articles = data;
            }, error => console.log('Could not load articles.'));
    }

    addArticle(article: Article) {
        article.issueId = this.issueId;
        this.articleService.addArticle(article);
    }

    deleteArticle(articleId: number) {
        this.articleService.deleteArticle(articleId);
    }

    addTodo(articleId: number, todo: Todo) {
        todo.articleId = articleId;
        this.todoService.addTodo(todo);
    }

    deleteTodo(todoId: number) {
        this.todoService.deleteTodo(todoId);
    }

    addNote(articleId: number, note: Note) {
        note.articleId = articleId;
        this.noteService.addNote(note);
    }

    deleteNote(noteId: number) {
        this.noteService.deleteNote(noteId);
    }
}
