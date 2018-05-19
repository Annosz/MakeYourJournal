using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Dtos
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public int IssueId { get; set; }

        public IEnumerable<TodoModel> Todos { get; set; }
        public IEnumerable<NoteModel> Notes { get; set; }
    }
}
