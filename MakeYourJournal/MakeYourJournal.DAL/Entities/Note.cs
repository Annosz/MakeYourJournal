using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public class Note: Item
    {
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
