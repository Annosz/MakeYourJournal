using MakeYourJournal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Services
{
    public interface IIssueService
    {
        IEnumerable<Issue> GetIssues();
        Issue GetIssue(int Id);
        Issue AddIssue(Issue Issue);
        Issue UpdateIssue(int IssueId, Issue Issue);
        void DeleteIssue(int IssueId);
    }
}
