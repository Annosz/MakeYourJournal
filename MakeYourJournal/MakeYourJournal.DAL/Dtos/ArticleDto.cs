using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }

        public int IssueAllTimeNumber { get; set; }
    }
}
