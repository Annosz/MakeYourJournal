using AutoMapper;
using MakeYourJournal.DAL.Dtos;
using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public class IssueService : IIssueService
    {
        public IssueService(JournalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public JournalDbContext DbContext { get; }

        public IEnumerable<Issue> GetIssues()
        {
            return DbContext.Issues.Include(i => i.Articles).ToList();
        }

        public Issue GetIssue(int Id)
        {
            return DbContext.Issues.Include(i => i.Articles).FirstOrDefault(i => i.Id == Id)
                ?? throw new EntityNotFoundException("Issue not found");
        }

        public Issue AddIssue(Issue Issue)
        {
            DbContext.Issues.Add(Issue);

            DbContext.SaveChanges();

            return Issue;
        }

        public Issue UpdateIssue(int IssueId, Issue Issue)
        {
            Issue.Id = IssueId;
            var entry = DbContext.Attach(Issue);
            entry.State = EntityState.Modified;

            try
            {
                DbContext.SaveChanges();
                return Issue;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Issue not found");
            }
        }

        public void DeleteIssue(int IssueId)
        {
            DbContext.Issues.Remove(new Issue { Id = IssueId });

            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new EntityNotFoundException("Issue not found");
            }
        }
    }
}
