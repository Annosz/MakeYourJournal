import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ToDoComponent } from './components/todo/to-do.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component'

import { TodoService } from './services/to-do.service';
import { FontsService } from '../fonts/fonts.service';

@NgModule({
  declarations: [
    AppComponent,
    ToDoComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [TodoService, FontsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
