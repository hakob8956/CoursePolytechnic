using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace CoursePol.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public string Date { get; set; }
        public string Facul { get; set; }
        public string Group { get; set; }
        public int NumberCourse { get; set; }
        

    }
}
