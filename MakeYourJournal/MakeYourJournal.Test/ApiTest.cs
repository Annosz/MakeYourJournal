using MakeYourJournal.ANG.Controllers;
using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Mapping;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MakeYourJournal.DAL.Dtos;

namespace MakeYourJournal.Test
{
    [TestClass]
    public class ApiTest
    {
        DbContextOptions<JournalDbContext> DbOptions = new DbContextOptionsBuilder<JournalDbContext>()
                .UseInMemoryDatabase(databaseName: "Api_Test_Db")
                .Options;


        [TestInitialize]
        public void Initialize()
        {
            using (var ctx = new JournalDbContext(DbOptions))
            {
                ctx.Issues.Add(new DAL.Entities.Issue
                {
                    AllTime = 474,
                    IssueDetails = new DAL.Entities.IssueDetails
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

        [TestMethod]


        [TestMethod]

    }
}
