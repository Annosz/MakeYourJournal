using MakeYourJournal.ANG.Controllers;
using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Mapping;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MakeYourJournal.Tests
{
    public class ArticleApiTest : IDisposable
    {
        DbContextOptions<JournalDbContext> DbOptions;
        
        public ArticleApiTest()
        {
            DbOptions = new DbContextOptionsBuilder<JournalDbContext>()
                .UseInMemoryDatabase(databaseName: "Article_Test_Db")
                .Options;

            using (var ctx = new JournalDbContext(DbOptions))
            {
                ctx.Issues.Add(new Issue
                {
                    Id = 6,
                    AllTime = 474,
                    IssueDetails = new IssueDetails
                    {
                        Volume = 45,
                        Number = 6,
                        Name = "Nyári szám",
                        CopyNumber = 800,
                        ExpectedPageCount = 36
                    },
                    Articles = new List<Article> ()
                    {
                        new Article() { Title = "Az 1%" },
                        new Article() { Title = "Utazd be Európát!"}
                    }
                });
                ctx.SaveChanges();
            }
        }

        [Fact]
        public void Get()
        {
            using (var ctx = new JournalDbContext(DbOptions))
            {
                //Arrange
                var controller = new ArticleController(new ArticleService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();

                //Assert
                var okObjectResult = result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<ArticleModel>;
                Assert.NotNull(model);

                Assert.Equal(2, model.Count());
            }
        }

        [Fact]
        public void GetByIssueId()
        {
            using (var ctx = new JournalDbContext(DbOptions))
            {
                //Arrange
                var issueController = new IssueController(new IssueService(ctx), MapperConfig.Configure());
                var controller = new ArticleController(new ArticleService(ctx), MapperConfig.Configure());

                //Act
                var issueResult = issueController.Get();
                var issueOkObjectResult = issueResult as OkObjectResult;
                var issueModel = issueOkObjectResult.Value as IEnumerable<IssueModel>;

                var result = controller.GetByIssue(issueModel.Single().AllTime);

                //Assert
                var okObjectResult = result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<ArticleModel>;
                Assert.NotNull(model);

                Assert.Equal(2, model.Count());
            }
        }

        [Fact]
        public void Add()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                //Arrange
                var issueController = new IssueController(new IssueService(ctx), MapperConfig.Configure());
                var controller = new ArticleController(new ArticleService(ctx), MapperConfig.Configure());

                //Act
                var issueResult = issueController.Get();
                var issueOkObjectResult = issueResult as OkObjectResult;
                var issueModel = issueOkObjectResult.Value as IEnumerable<IssueModel>;

                //Act
                controller.Post(new ArticleModel()
                {
                    IssueId = issueModel.Single().Id,
                    Title = "Receptek"
                });
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var iss = ctx.Articles.ToList();
                Assert.Equal(3, ctx.Articles.Count());
            }
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new ArticleController(new ArticleService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as IEnumerable<ArticleModel>;

                controller.Delete(model.First().Id);
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                Assert.Equal(1, ctx.Articles.Count());
            }
        }

        [Fact]
        public void Put()
        {
            int Id = 0;

            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new ArticleController(new ArticleService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as IEnumerable<ArticleModel>;

                Id = model.First().Id;

                controller.Put(model.First().Id, new ArticleModel
                {
                    Id = model.First().Id,
                    Title = "Sudoku"
                });
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                Assert.Equal(2, ctx.Articles.Count());
                Assert.Equal("Sudoku", ctx.Articles.Where(a => a.Id == Id).Single().Title);
            }
        }

        public void Dispose()
        {
            using (var ctx = new JournalDbContext(DbOptions))
            {
                ctx.Database.EnsureDeleted();
            }
        }
    }
}
