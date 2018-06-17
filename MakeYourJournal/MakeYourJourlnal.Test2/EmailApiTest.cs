using MakeYourJournal.ANG.Controllers;
using MakeYourJournal.ANG.Services;
using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Mapping;
using MakeYourJournal.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakeYourJournal.Tests
{
    public class EmailApiTest : IDisposable
    {
        DbContextOptions<JournalDbContext> DbOptions;
        
        public EmailApiTest()
        {
            DbOptions = new DbContextOptionsBuilder<JournalDbContext>()
                .UseInMemoryDatabase(databaseName: "Email_Test_Db")
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
        public void DeadlineReminder()
        {
            Mock<EmailSender> sender = new Mock<EmailSender>();
            sender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            using (var ctx = new JournalDbContext(DbOptions))
            {
                var controller = new EmailController(sender.Object, new IssueService(ctx), MapperConfig.Configure());

                controller.SendDeadlineReminder(ctx.Issues.SingleAsync().Id);
            }

            sender.Verify(mock => mock.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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
