using MakeYourJournal.DAL.Entities;
using System.Collections.Generic;

namespace MakeYourJournal.DAL.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();
        IEnumerable<Article> GetArticlesByIssueId(int issueId);
        Article GetArticle(int articleId);
        Article AddArticle(Article Article);
        Article UpdateArticle(int ArticleId, Article Article);
        void DeleteArticle(int ArticleId);
    }
}