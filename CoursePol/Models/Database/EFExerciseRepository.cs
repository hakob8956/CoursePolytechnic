using CoursePol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    public class EFExerciseRepository:IExercise
    {
        private ApplicationDbContext context;

        public EFExerciseRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Exercise> Exercises => context.Exercises;

        public Exercise DeleteExercise(int exerciseID)
        {
            Exercise dbEntry = context.Exercises
              .FirstOrDefault(c => c.ID == exerciseID);
            if (dbEntry != null)
            {
                context.Exercises.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveExercise(Exercise exercise)
        {
            if (exercise.ID == 0)
            {
                context.Exercises.Add(exercise);
            }
            else
            {
                //context.Profiles.AddRange(profile);
                Exercise dbEntry = context.Exercises
                    .FirstOrDefault(c => c.ID == exercise.ID);

                if (dbEntry != null)
                {
                    dbEntry.Text = exercise.Text;
                    dbEntry.Title = exercise.Title;
                    dbEntry.Points = exercise.Points;
                   
                }
            }
            context.SaveChanges();
        }
    }
}
