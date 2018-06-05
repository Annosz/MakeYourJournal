using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeYourJournal.DAL.Seed
{
    public class JournalDataSeeder : IDataSeeder<JournalDbContext, JournalSeedData>
    {
        public int SeedData(JournalDbContext context, Func<JournalSeedData> dataAccessor)
        {
            if (!context.Issues.Any())
            {
                var data = dataAccessor();


                var Issues = data.Issues.ToDictionary(i => i.AllTime, i => new Issue
                {
                    AllTime = i.AllTime,
                    IssueDetails = new IssueDetails
                    {
                        Volume = i.Volume,
                        Number = i.Number,
                        Deadline = i.Deadline,
                        Name = i.Name,
                        Description = i.Description,
                        ExpectedPageCount = i.ExpectedPageCount,
                        CopyNumber = i.CopyNumber
                    }
                });
                var Articles = data.Articles.ToDictionary(a => a.Title, a => new Article
                {
                    Title = a.Title,
                    Topic = a.Topic,
                    Issue = Issues[a.AllTime]
                });

                context.Issues.AddRange(Issues.Values);
                context.Articles.AddRange(Articles.Values);

                //Mappint needs to be done from this way, otherwise TPH linking gets fucked up
                foreach (Article article in Articles.Values)
                {
                    var todos = data.Todos.Where(t => t.ArticleTitle == article.Title).ToList();
                    if (todos.Count != 0)
                    {
                        if (article.Items == null)
                            article.Items = new List<Item>();

                        var TodoList = article.Items.ToList();
                        TodoList.AddRange(todos.Select(t => new Todo
                        {
                            Name = t.Name,
                            Done = t.Done
                        }));
                        article.Items = TodoList;
                    }
                        

                    var notes = data.Notes.Where(n => n.ArticleTitle == article.Title).ToList();
                    if (notes.Count != 0)
                    {
                        if (article.Items == null)
                            article.Items = new List<Item>();

                        var NoteList = article.Items.ToList();
                        NoteList.AddRange(notes.Select(n => new Note
                        {
                            Name = n.Name,
                            Description = n.Description
                        }));
                        article.Items = NoteList;
                    }
                }

                return context.SaveChanges();
            }

            return 0;
        }
    }
}
