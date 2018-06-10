using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public class Address
    {
        [Display(Name = "Zip code")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Give a valid Hungarian zip code.")]
        public int ZipCode { get; set; }
        
        [MaxLength(150, ErrorMessage = "Give a settlemant name with a maximum length of 150 characters.")]
        public string Settlement { get; set; }

        [Display(Name = "Street address")]
        [MaxLength(300, ErrorMessage = "Give a street address name with a maximum length of 300 characters.")]
        public string StreetAddress { get; set; }
    }
}
