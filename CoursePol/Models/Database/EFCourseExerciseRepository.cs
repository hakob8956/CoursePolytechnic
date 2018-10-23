using CoursePol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    public class EFCourseExerciseRepository:ICourseExercise
    {
        private ApplicationDbContext context;

        public EFCourseExerciseRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<CourseExercise> CourseExercises => context.CourseExercises;

     

        public CourseExercise DeleteCourseExercise(int courseExerciseID)
        {
            CourseExercise dbEntry = context.CourseExercises
              .FirstOrDefault(c => c.ID == courseExerciseID);
            if (dbEntry != null)
            {
                context.CourseExercises.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCourseExercise(CourseExercise courseExercise)
        {
            if (courseExercise.ID == 0)
            {
                context.CourseExercises.Add(courseExercise);
            }
            else
            {
                //context.Profiles.AddRange(profile);
                CourseExercise dbEntry = context.CourseExercises
                    .FirstOrDefault(c=>c.CourseID==courseExercise.ID);

                if (dbEntry != null)
                {
                    dbEntry.ExercisesID = courseExercise.ExercisesID;
                    dbEntry.CourseID = courseExercise.CourseID;
                    dbEntry.UserID = courseExercise.UserID;
                    dbEntry.Completed = courseExercise.Completed;
                }
            }
            context.SaveChanges();
        }
    }
}
