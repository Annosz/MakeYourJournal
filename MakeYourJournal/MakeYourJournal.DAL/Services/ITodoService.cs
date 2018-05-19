using MakeYourJournal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetTodos();
        IEnumerable<Todo> GetTodosByArticleId(int articleId);
        Todo GetTodo(int Id);
        Todo AddTodo(Todo Todo);
        Todo UpdateTodo(int TodoId, Todo Todo);
        void DeleteTodo(int TodoId);
    }
}
