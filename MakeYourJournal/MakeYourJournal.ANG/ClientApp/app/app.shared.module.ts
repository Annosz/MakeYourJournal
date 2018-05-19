import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { IssuePageComponent } from './components/issuepage/issuepage.component';
import { CounterComponent } from './components/counter/counter.component';
import { LoginComponent } from './components/login/login.component';
import { RegistComponent } from './components/regist/regist.component';

import { AccountService } from "./services/account.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        IssuePageComponent,
        HomeComponent,
        LoginComponent,
        RegistComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'issue-page/:id', component: IssuePageComponent },
            { path: 'login', component: LoginComponent },
            { path: 'regist', component: RegistComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AccountService
    ]
})
export class AppModuleShared {
}
