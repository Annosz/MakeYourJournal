using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required]
        public int Number { get; set; }

        public IssueDetails IssueDetails { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
