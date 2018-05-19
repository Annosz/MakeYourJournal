using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Dtos
{
    public class TodoModel
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public string Name { get; set; }
        public string ArticleTitle { get; set; }
        public int ArticleId { get; set; }
    }
}
