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

        public IEnumerable<Article> GetArticlesByAllTimeNumber(int allTime)
        {
            var ret = DbContext.Articles.Where(a => a.IssueAllTime == allTime).Include(a => a.Items).ToList();
            return ret;
        }

        public IEnumerable<Article> GetArticlesByIssueVolumeAndNumber(int volume, int number)
        {
            var ret = DbContext.Articles.Include(a => a.Issue).ThenInclude(i => i.IssueDetails)
                .Where(a => a.Issue.IssueDetails.Volume == volume && a.Issue.IssueDetails.Number == number)
                .Include(a => a.Items).ToList();
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

            return GetArticle(Article.Id);
        }

        public Article UpdateArticle(int ArticleId, Article Article)
        {
            Article.Id = ArticleId;
            DeleteArticle(ArticleId);
            return AddArticle(Article);
        }

        public void DeleteArticle(int ArticleId)
        {
            DbContext.Remove(DbContext.Articles.Where(a => a.Id == ArticleId).Single());

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
