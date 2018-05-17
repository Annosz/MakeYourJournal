using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public abstract class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public Article Article { get; set; }
        public int ArticleId { get; set; }
    }
}
