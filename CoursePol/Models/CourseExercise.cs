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
        public string Text { get; set; }
        public int Completed { get; set; } = 0;//1,-1,0 :--  1 - complete, -1 - no complete, 0 - none



    }
}
