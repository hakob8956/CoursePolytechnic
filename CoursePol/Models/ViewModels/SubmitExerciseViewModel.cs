using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models.ViewModels
{
    public class SubmitExerciseViewModel
    {
        public int ExerciseID { get; set; }
        public int CourseID { get; set; }
        public string Text { get; set; }

    }
}
