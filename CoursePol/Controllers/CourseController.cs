﻿using CoursePol.Models;
using CoursePol.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PascalChecker;



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
        public async Task<JsonResult> Course([FromBody]Exercise model)
        {           
            //TODO FIX PATH PASCAL         
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                FileInfo folder1 = new FileInfo($"D:/Programming/CoursePol/CoursePol/wwwroot/pascalFile/User[{user.Id}]");
                if (!folder1.Exists)
                {
                    Directory.CreateDirectory(folder1.FullName);
                  
                }
                FileInfo folder2 = new FileInfo(folder1.FullName + $"/exercise[{model.ID}]");
                if (!folder2.Exists)
                {
                    Directory.CreateDirectory(folder2.FullName);

                }
                string path = $"{folder2.FullName}/exercise[{model.ID}].pas";
                FileInfo fi1 = new FileInfo(path);

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(model.Text);
                }
                bool a = Pascal.BuildOutput(2,folder2.FullName, Path.GetFileNameWithoutExtension(fi1.Name));//TODO first input-->it's id exercise (test)
                string output= a ? $"Output:True" : "Output:False";
                return Json(output);
            }

            return Json("Error");

        }
       


    } 

}