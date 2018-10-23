using CoursePol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    public class EFFolowerRepository : IFolower
    {
        private ApplicationDbContext context;

        public EFFolowerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Folower> Folowers => context.Folowers;

        public Folower DeletFolower(int folowerID)
        {
            Folower dbEntry = context.Folowers
              .FirstOrDefault(c => c.ID == folowerID);
            if (dbEntry != null)
            {
                context.Folowers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveFolower(Folower folower)
        {
            if (folower.ID == 0)
            {
                context.Folowers.Add(folower);
            }
            else
            {
                //context.Profiles.AddRange(profile);
                Folower dbEntry = context.Folowers
                    .FirstOrDefault(c => c.ID == folower.ID);

                if (dbEntry != null)
                {
                    dbEntry.CourseID = folower.CourseID;
                    dbEntry.UserID = folower.UserID;

                }
            }
            context.SaveChanges();

        }
    }
}
