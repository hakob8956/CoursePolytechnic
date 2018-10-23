using CoursePol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    public class EFUserExercisesComplete:IUserExercisesComplete
    { 
        private ApplicationDbContext context;

        public EFUserExercisesComplete(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<UserExercisesComplete> Completes => context.UserExercisesCompletes;

        public UserExercisesComplete DeleteComplete(int completeID)
        {
            UserExercisesComplete dbEntry = context.UserExercisesCompletes
              .FirstOrDefault(c => c.ID == completeID);
            if (dbEntry != null)
            {
                context.UserExercisesCompletes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveComplete(UserExercisesComplete complete)
        {
            if (complete.ID == 0)
            {
                context.UserExercisesCompletes.Add(complete);
            }
            else
            {
                //context.Profiles.AddRange(profile);
                UserExercisesComplete dbEntry = context.UserExercisesCompletes
                    .FirstOrDefault(c => c.ID == complete.ID);

                if (dbEntry != null)
                {
                    dbEntry.CourseID = complete.CourseID;
                    dbEntry.UserID = complete.UserID;
                    dbEntry.Allow = complete.Allow;

                }
            }
            context.SaveChanges();
        }
    }
}
