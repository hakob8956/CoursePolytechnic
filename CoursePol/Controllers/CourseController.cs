using CoursePol.Models;
using CoursePol.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PascalChecker;
using SmartBreadcrumbs;
using System;
using System.Diagnostics;
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
        private readonly ICourseExercise _courseExerciseComplete;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CourseController(UserManager<User> userManager, IExercise exercise, ICourse course, IFolower folower, ICourseExercise courseExerciseComplete)
        {
            _exercise = exercise;
            _userManager = userManager;
            _course = course;
            _folower = folower;
            _courseExerciseComplete = courseExerciseComplete;
        }
        [Breadcrumb("Courses", CacheTitle = true, FromAction = "Home.Index")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Courses";
            var Courses = _course.Courses.ToList();
            return View(Courses);


        }
        [Breadcrumb("ViewData.Title", CacheTitle = true, FromAction = "Course.Index")]
        public IActionResult CourseDetails(int? courseID)
        {
            ViewData["Title"] = "AboutCourse";
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
        [Breadcrumb("ViewData.Title", CacheTitle = true, FromAction = "Course.CourseDetails")]
        [Authorize]
        public IActionResult Course(int? courseID)
        {

            if (courseID == null)
            {
                return NotFound();
            }
            var Course = _course.Courses.FirstOrDefault(c => c.CourseID == courseID);//for ViewData
            if (Course != null)
            {
                var CourseExercise = _exercise.Exercises.Where(c => c.CourseID == Course.CourseID);
                if (CourseExercise.Count() != 0)
                {
                    ViewData["Title"] = Course.Title;
                    return View(CourseExercise);
                }
            }
            return NotFound();

        }

        [Breadcrumb("ViewData.Title", CacheTitle = true, FromAction = "Course.Course")]
        [Authorize]
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
                if (exercise != null && user != null)
                {
                    ExerciseViewModel model = new ExerciseViewModel()
                    {
                        Exercise = exercise,
                        UserID = user.Id
                    };
                    ViewData["Title"] = model.Exercise.Title;
                    return View(model);
                }


            }
            return NotFound();
        }
        //TODO ADD FOLOWER
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
        public async Task<JsonResult> Course([FromBody]CourseExercise model)
        {
            //TODO FIX PATH PASCAL         
            var user = await GetCurrentUserAsync();
            var CurrentExercise = _exercise.Exercises.FirstOrDefault(i => i.ID == model.ExercisesID);

            if (user != null && CurrentExercise != null)
            {

                FileInfo folder1 = new FileInfo($"D:/Programming/CoursePol/CoursePol/wwwroot/pascalFile/User[{user.Id}]");
                if (!folder1.Exists)
                {
                    Directory.CreateDirectory(folder1.FullName);

                }
                FileInfo folder2 = new FileInfo(folder1.FullName + $"/exercise[{CurrentExercise.NumberSolution}]");
                if (!folder2.Exists)
                {
                    Directory.CreateDirectory(folder2.FullName);

                }
                string path = $"{folder2.FullName}/exercise[{CurrentExercise.NumberSolution}].pas";
                FileInfo fi1 = new FileInfo(path);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(model.Text);
                }
                bool completed = Pascal.BuildOutput(CurrentExercise.NumberSolution, folder2.FullName, Path.GetFileNameWithoutExtension(fi1.Name));
                string output = completed ? $"Output:True" : "Output:False";
                var courseExercise = _courseExerciseComplete.CourseExercises.FirstOrDefault(l => l.ExercisesID == model.ExercisesID && user.Id == l.UserID && l.CourseID == model.CourseID);
                if (courseExercise == null)
                {
                    CourseExercise courseExerciseNewModel = new CourseExercise()
                    {
                        CourseID = model.CourseID,
                        UserID = user.Id,
                        Text = model.Text,
                        ExercisesID = model.ExercisesID,
                        Completed = completed ? 1 : -1
                    };
                    _courseExerciseComplete.SaveCourseExercise(courseExerciseNewModel);
                }
                else
                {
                    courseExercise.Completed = completed ? 1 : -1;
                    courseExercise.Text = model.Text;
                    _courseExerciseComplete.SaveCourseExercise(courseExercise);
                }


                return Json(output);
            }

            return Json("Error");

        }



    }

}