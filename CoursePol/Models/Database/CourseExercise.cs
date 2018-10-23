using System.ComponentModel.DataAnnotations;

namespace CoursePol.Models
{
    public class CourseExercise
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public int ExercisesID { get; set; }
        public int Completed { get; set; }//1,-1,0
    }
}
