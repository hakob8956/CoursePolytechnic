using CoursePol.Models;
using CoursePol.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CoursePol.Controllers
{
    public class CourseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IExercise _exercise;
        private readonly ICourse _course;
        private readonly IFolower _folower;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CourseController(UserManager<User> userManager, IExercise exercise, ICourse course, IFolower folower)
        {
            _exercise = exercise;
            _userManager = userManager;
            _course = course;
            _folower = folower;
        }

        public IActionResult Index()
        {
            var Courses = _course.Courses.ToList();
            return View(Courses);


        }
        public IActionResult CourseDetails(int? courseID)
        {
            if (courseID == null)
            {
                return NotFound();
            }
            var Course = _course.Courses.FirstOrDefault(c => c.CourseID == courseID);

            if (Course != null)
            {
                return View(Course);
            }
            return NotFound();

        }
        public IActionResult Course(int? courseID)
        {
            if (courseID == null)
            {
                return NotFound();
            }
            var CourseExercise = _exercise.Exercises.Where(c => c.CourseID == courseID);

            if (CourseExercise.Count() != 0)
            {
                return View(CourseExercise);
            }
            return NotFound();

        }
        public async Task<IActionResult> Exercise(string _exerciseID = null)
        {
            int exerciseID;
            if (_exerciseID != null)
            {
                try
                {
                    exerciseID = Convert.ToInt32(_exerciseID);
                }
                catch (Exception)
                {
                    return NotFound();
                }

                var user = await GetCurrentUserAsync();

                Exercise exercise = _exercise.Exercises.FirstOrDefault(e => e.ID == exerciseID);
                if (exercise != null)
                {
                    ExerciseViewModel model = new ExerciseViewModel()
                    {
                        ExerciseID = exercise.ID,
                        Text = exercise.Text,
                        CourseID = exercise.CourseID,
                        UserID = user.Id
                    };
                    return View(model);
                }


            }
            return NotFound();
        }

        public async Task<IActionResult> AddFolower(int? courseID)
        {
            var user = await GetCurrentUserAsync();

            if (courseID != null && user != null)
            {
                Course course = _course.Courses.FirstOrDefault(c => c.CourseID == courseID);
                if (course != null)
                {
                    if (_folower.Folowers.FirstOrDefault(f => f.UserID == user.Id && f.CourseID == course.CourseID) == null)
                    {
                        _folower.SaveFolower(new Folower()
                        {
                            CourseID = course.CourseID,
                            UserID = user.Id
                        });
                    }
                    return View("Course", courseID);
                }
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> Course(ExerciseViewModel model)
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                FileInfo folder = new FileInfo($"D:/Programming/CoursePol/CoursePol/wwwroot/pascalFile/User{model.UserID}");
                if (!folder.Exists)
                {
                    Directory.CreateDirectory($"D:/Programming/CoursePol/CoursePol/wwwroot/pascalFile/User{model.UserID}");
                }
                string path = $"D:/Programming/CoursePol/CoursePol/wwwroot/pascalFile/User{model.UserID}/exercise{model.ExerciseID}.txt";
                FileInfo fi1 = new FileInfo(path);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(model.Text);
                }

            }

            return View(model);

        }


    }

}