using CoursePol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    public class EFCourseRepository:ICourse
    {
        private ApplicationDbContext context;

        public EFCourseRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Course> Courses => context.Courses;

        public Course DeleteCourse(int courseID)
        {
            Course dbEntry = context.Courses
               .FirstOrDefault(c=>c.CourseID == courseID);
            if (dbEntry != null)
            {
                context.Courses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCourse(Course course)
        {
            if (course.CourseID == 0)
            {
                context.Courses.Add(course);
            }
            else
            {
                Course dbEntry = context.Courses
                    .FirstOrDefault(c => c.CourseID == course.CourseID);
                if (dbEntry != null)
                {
                    dbEntry.UserID = course.UserID;
                    dbEntry.Title = course.Title;
                    dbEntry.Description = course.Description;
                    dbEntry.Category = course.Category;
                    dbEntry.Text = course.Text;
                    dbEntry.Author = course.Author;
                    dbEntry.DateTime = course.DateTime;
                    dbEntry.ImageData = course.ImageData;
                    dbEntry.ImageMimeType = course.ImageMimeType;
                    dbEntry.Allow = course.Allow;
                }
            }
            context.SaveChanges();
        }
    }
}
