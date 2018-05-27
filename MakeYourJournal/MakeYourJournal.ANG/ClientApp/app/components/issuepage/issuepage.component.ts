import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';

import Issue from '../../models/issue.model';
import Todo from '../../models/todo.model';
import Note from '../../models/note.model';
import Article from '../../models/article.model';

import { NavMenuComponent } from '../navmenu/navmenu.component';
import { IssueService } from '../../services/issue.service';
import { ArticleService } from '../../services/article.service';
import { TodoService } from '../../services/todo.service';
import { NoteService } from '../../services/note.service';

@Component({
    selector: 'issue-page',
    templateUrl: './issuepage.component.html'
})
export class IssuePageComponent {
    public issueId: number;
    public issue: Issue;
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
        this.issueService.getIssue(issueId)
            .subscribe(data => {
                this.issue = data;
            }, error => console.log('Could not load issue.'));
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

    todoChecked(todo: Todo) {
        this.todoService.updateTodo(todo.id, todo).subscribe(data => { }, error => console.log('Could not add article.'));
    }

    deleteTodo(todo: Todo, article: Article) {
        this.todoService.deleteTodo(todo.id);
        const index: number = article.todos.indexOf(todo);
        if (index !== -1) {
            article.todos.splice(index, 1);
        }
    }

    addNote(article: Article, note: Note) {
        note.articleId = article.id;
        this.noteService.addNote(note).subscribe(data => {
            article.notes.push(data);
        }, error => console.log('Could not add article.'));
    }

    deleteNote(note: Note, article: Article) {
        this.noteService.deleteNote(note.id);
        const index: number = article.notes.indexOf(note);
        if (index !== -1) {
            article.notes.splice(index, 1);
        }
    }

    deleteIssue(issueId: number) {
        this.issueService.deleteIssue(issueId);
        this.navMenu.ngOnInit();
        this.router.navigateByUrl('/home'); 
    }
}
