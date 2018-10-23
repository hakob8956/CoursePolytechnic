using System;
using System.Collections.Generic;
using CoursePol.Models;


namespace CoursePol.Models
{
    public interface ICourse
    {
        IEnumerable<Course> Courses { get; }
        void SaveCourse(Course course);
        Course DeleteCourse(int courseID);
    }
}
