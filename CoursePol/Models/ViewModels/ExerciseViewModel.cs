using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models.ViewModels
{
    public class ExerciseViewModel
    {
        public int ExerciseID { get; set; }
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public string Text { get; set; }
    }
}
