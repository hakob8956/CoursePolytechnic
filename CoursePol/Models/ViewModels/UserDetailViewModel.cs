
using System.Collections.Generic;

namespace CoursePol.Models.ViewModels
{
    public class UserDetailViewModel
    {
        //public string userID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Folower> Folower { get; set; }
        public IEnumerable<string> CurrentFolders { get; set; }
        


    }
}
