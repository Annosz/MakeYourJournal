import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';

import Todo from '../../models/todo.model';
import Note from '../../models/note.model';
import Article from '../../models/article.model';

import { NavMenuComponent } from '../navmenu/navmenu.component';
import { ArticleService } from '../../services/article.service';
import { TodoService } from '../../services/todo.service';
import { NoteService } from '../../services/note.service';
import { IssueService } from '../../services/issue.service';

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
        private navMenu: NavMenuComponent,
        private articleService: ArticleService,
        private todoService: TodoService,
        private noteService: NoteService,
        private issueService: IssueService,
        private activatedroute: ActivatedRoute,
        private router: Router)
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
        this.articleService.addArticle(article)
            .subscribe(data => {
                this.articles.push(data);
            }, error => console.log('Could not add article.'));
    }

    deleteArticle(article: Article) {
        this.articleService.deleteArticle(article.id);
        const index: number = this.articles.indexOf(article);
        if (index !== -1) {
            this.articles.splice(index, 1);
        }
    }

    addTodo(article: Article, todo: Todo) {
        todo.articleId = article.id;
        this.todoService.addTodo(todo).subscribe(data => {
            article.todos.push(data);
        }, error => console.log('Could not add article.'));
    }

    deleteTodo(todo: Todo, article: Article) {
        this.todoService.deleteTodo(todo.id);
        const index: number = article.todos.indexOf(todo);
        if (index !== -1) {
            article.todos.splice(index, 1);
        }
    }

    addNote(articleId: number, note: Note) {
        note.articleId = articleId;
        this.noteService.addNote(note);
    }

    deleteNote(noteId: number) {
        this.noteService.deleteNote(noteId);
    }

    deleteIssue(issueId: number) {
        this.issueService.deleteIssue(issueId);
        this.navMenu.ngOnInit();
        this.router.navigateByUrl('/home'); 
    }
}
