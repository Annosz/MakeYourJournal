using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public class TodoService : ITodoService
    {
        public TodoService(JournalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public JournalDbContext DbContext { get; }

        public IEnumerable<Todo> GetTodos()
        {
            return DbContext.Todos.Include(t => t.Article).ToList();
        }

        public IEnumerable<Todo> GetTodosByArticleId(int articleId)
        {
            return DbContext.Todos.Where(t => t.ArticleId == articleId).Include(t => t.Article).ToList();
        }

        public Todo GetTodo(int Id)
        {
            return DbContext.Todos.Include(t => t.Article).FirstOrDefault(t => t.Id == Id)
                ?? throw new EntityNotFoundException("Todo not found");
        }

        public Todo AddTodo(Todo Todo)
        {
            DbContext.Todos.Add(Todo);

            DbContext.SaveChanges();

            return GetTodo(Todo.Id);
        }

        public Todo UpdateTodo(int TodoId, Todo Todo)
        {
            Todo.Id = TodoId;
            var entry = DbContext.Attach(Todo);
            entry.State = EntityState.Modified;

            try
            {
                DbContext.SaveChanges();
                return GetTodo(Todo.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Todo not found");
            }
        }

        public void DeleteTodo(int TodoId)
        {
            DbContext.Todos.Remove(new Todo { Id = TodoId });

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Todo not found");
            }
        }
    }
}
