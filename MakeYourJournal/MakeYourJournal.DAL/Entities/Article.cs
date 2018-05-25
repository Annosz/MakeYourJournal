using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Topic { get; set; }

        [Required]
        public Issue Issue { get; set; }
        public int IssueId { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
