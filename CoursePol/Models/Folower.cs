using System.ComponentModel.DataAnnotations;

namespace CoursePol.Models
{
    public class Folower
    {
        [Key]
        public int ID { get; set; }
        public int CourseID { get; set; }
        public string UserID { get; set; }
    }
}
