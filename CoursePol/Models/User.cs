using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace CoursePol.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string DateBirthday { get; set; }
        public string DateRegister { get; set; }
        public string Institute { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public string Group { get; set; }

    }
}
