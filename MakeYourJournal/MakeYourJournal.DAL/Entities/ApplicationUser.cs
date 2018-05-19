using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL.Entities
{
    public class ApplicationUser : IdentityUser<long> { }
}
