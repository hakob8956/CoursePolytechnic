using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoursePol.Models.ViewModels;
using CoursePol.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoursePol.Controllers
{
    public class HomeController : Controller
    {
        private ICourse _course;

        public HomeController(ICourse course)
        {
            _course = course;           
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_course.Courses);
        }
        public IActionResult Course()
        {
            return View();
        }
    }
}
