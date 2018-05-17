using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Entities;
using System;
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


                var Issues = data.Issues.ToDictionary(i => i.AllTime, i => i);
                var Articles = data.Articles.ToDictionary(a => a.Title, a => new Article
                {
                    Title = a.Title,
                    Topic = a.Topic,
                    Issue = Issues[a.IssueAllTimeNumber]
                });

                context.Issues.AddRange(Issues.Values);
                context.Articles.AddRange(Articles.Values);
                context.Todos.AddRange(data.Todos.Select(t => new Todo
                {
                    Name = t.Name,
                    Done = t.Done,
                    Article = Articles[t.ArticleTitle]
                }));
                context.Notes.AddRange(data.Notes.Select(n => new Note
                {
                    Name = n.Name,
                    Description = n.Description,
                    Article = Articles[n.ArticleTitle]
                }));

                return context.SaveChanges();
            }

            return 0;
        }
    }
}
