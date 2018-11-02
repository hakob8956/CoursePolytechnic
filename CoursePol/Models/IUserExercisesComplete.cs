using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    interface IUserExercisesComplete
    {
        IEnumerable<UserExercisesComplete> Completes{ get; }
        void SaveComplete(UserExercisesComplete complete);
        UserExercisesComplete DeleteComplete(int completeID);
    }
}
