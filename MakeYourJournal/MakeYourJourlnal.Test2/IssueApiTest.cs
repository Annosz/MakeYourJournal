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
using Xunit;

namespace MakeYourJourlnal.Test2
{
    public class IssueApiTest : IDisposable
    {
        DbContextOptions<JournalDbContext> DbOptions;

        
        public IssueApiTest()
        {
            DbOptions = new DbContextOptionsBuilder<JournalDbContext>()
                .UseInMemoryDatabase(databaseName: "Issue_Test_Db")
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
                var controller = new IssueController(new IssueService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();

                //Assert
                var okObjectResult = result as OkObjectResult;
                Assert.NotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<IssueModel>;
                Assert.NotNull(model);
                
                Assert.Single(model);
                Assert.Equal("Nyári szám", model.Single().Name);
            }
        }

        [Fact]
        public void AddWithoutDetails()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new IssueController(new IssueService(ctx), MapperConfig.Configure());

                //Act
                controller.Post(new IssueModel()
                {
                    AllTime = 500
                });
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var iss = ctx.Issues.ToList();
                Assert.Equal(2, ctx.Issues.Count());
            }
        }

        [Fact]
        public void AddWithDetails()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new IssueController(new IssueService(ctx), MapperConfig.Configure());

                //Act
                controller.Post(new IssueModel()
                {
                    Volume = 46,
                    Number = 0,
                    Name = "Gólyaszám"
                });
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                Assert.Equal(2, ctx.Issues.Count());
                Assert.Equal(2, ctx.IssueDetails.Count());
                Assert.Equal("Gólyaszám", ctx.IssueDetails.Where(i => i.Volume == 46).Single().Name);
            }
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new IssueController(new IssueService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as IEnumerable<IssueModel>;

                controller.Delete(model.Single().Id);
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                Assert.Equal(0, ctx.Issues.Count());
            }
        }

        [Fact]
        public void Put()
        {
            //Arrange
            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new IssueController(new IssueService(ctx), MapperConfig.Configure());

                //Act
                var result = controller.Get();
                var okObjectResult = result as OkObjectResult;
                var model = okObjectResult.Value as IEnumerable<IssueModel>;

                controller.Put(model.Single().Id, new IssueModel
                {
                    Id = 6,
                    AllTime = 474,
                    Volume = 45,
                    Number = 3,
                    Name = "Téli szám",
                    CopyNumber = 800,
                    ExpectedPageCount = 36
                });
            }

            //Assert
            using (var ctx = new JournalDbContext(DbOptions))
            {
                Assert.Equal(1, ctx.Issues.Count());
                Assert.Equal("Téli szám", ctx.Issues.Include(i => i.IssueDetails).Single().IssueDetails.Name);
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
