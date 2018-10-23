using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    interface IExercise
    {
        IEnumerable<Exercise> Exercises { get; }
        void SaveExercise(Exercise exercise);
        Exercise DeleteExercise(int exerciseID);
    }
}
