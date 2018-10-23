using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    interface ICourseExercise
    {
        IEnumerable<CourseExercise> CourseExercises { get; }
        void SaveCourseExercise(CourseExercise courseExercise);
        CourseExercise DeleteCourseExercise(int courseExerciseID);
    }
}
