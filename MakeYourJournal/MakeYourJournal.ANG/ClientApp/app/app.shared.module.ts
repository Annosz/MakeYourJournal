import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistComponent } from './components/regist/regist.component';
import { AddIssueComponent } from './components/addissue/addissue.component';
import { IssuePageComponent } from './components/issuepage/issuepage.component';

import { AccountService } from "./services/account.service";
import { IssueService } from "./services/issue.service";
import { ArticleService } from "./services/article.service";
import { TodoService } from "./services/todo.service";
import { NoteService } from "./services/note.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LoginComponent,
        RegistComponent,
        AddIssueComponent,
        IssuePageComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginComponent },
            { path: 'regist', component: RegistComponent },
            { path: 'add-issue', component: AddIssueComponent },
            { path: 'issue-page/:id', component: IssuePageComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AccountService,
        IssueService,
        ArticleService,
        TodoService,
        NoteService
    ]
})
export class AppModuleShared {
}
