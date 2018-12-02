using System.ComponentModel.DataAnnotations;

namespace CoursePol.Models
{
    public class Exercise
    {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        public int NumberSolution { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
    }
}
