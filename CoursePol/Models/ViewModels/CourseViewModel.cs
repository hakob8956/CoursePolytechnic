using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models.ViewModels
{
    public class CourseViewModel
    {
        public Exercise CourseExercise { get; set; }
        public IEnumerable<Exercise> CourseExercises { get; set; }
    }
}
