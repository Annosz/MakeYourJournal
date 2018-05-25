using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public class ArticleService : IArticleService
    {
        public ArticleService(JournalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public JournalDbContext DbContext { get; }

        public IEnumerable<Article> GetArticles()
        {
            return DbContext.Articles.Include(a => a.Items).ToList();
        }

        public IEnumerable<Article> GetArticlesByIssueId(int issueId)
        {
            var ret = DbContext.Articles.Where(a => a.IssueId == issueId).Include(a => a.Items).ToList();
            return ret;
        }

        public Article GetArticle(int articleId)
        {
            return DbContext.Articles.Include(a => a.Items).FirstOrDefault(a => a.Id == articleId)
                ?? throw new EntityNotFoundException("Article not found");
        }

        public Article AddArticle(Article Article)
        {
            DbContext.Articles.Add(Article);

            DbContext.SaveChanges();

            return Article;
        }

        public Article UpdateArticle(int ArticleId, Article Article)
        {
            Article.Id = ArticleId;
            var entry = DbContext.Attach(Article);
            entry.State = EntityState.Modified;

            try
            {
                DbContext.SaveChanges();
                return Article;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Article not found");
            }
        }

        public void DeleteArticle(int ArticleId)
        {
            DbContext.Articles.Remove(new Article { Id = ArticleId });

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Article not found");
            }
        }
    }
}
